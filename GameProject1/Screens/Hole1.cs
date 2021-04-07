using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameProject1.StateManagement;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using GameProject1.ParticleSystems;

namespace GameProject1.Screens
{
    public class Hole1 : GameScreen
    {
        private ContentManager _content;
        private SpriteFont font;
        private SpriteFont high_score_font;
        private Texture2D windArrow;

        private Disc disc;
        private DiscBasket basket;

        private Bush[] bushes;
        private Tree[] trees;

        private bool is_hit_by_bush = false;
        private bool is_in_basket = false;

        private float elapsed_text_time = 0f;
        private bool input_enabled = true;
        private float current_round_time = 0f;
        private float high_score_time = 10000f;

        private SoundEffect sfxBushHit;
        private SoundEffect sfxChains;
        private Song backgroundMusic;

        private Random rand = new Random();
        private float windAngle;
        private Vector2 windDirection;

        private readonly InputAction _pauseAction;
        private float _pauseAlpha;

        WindParticleSystem windParticleSystem;
        ExplosionParticleSystem basketParticleSystem;
        ExplosionParticleSystem bushParticleSystem;

        // <summary>
        /// Current position
        /// </summary>
        public Vector2 Direction { get; private set; }

        public Hole1()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

            _pauseAction = new InputAction(
                new[] { Buttons.Start, Buttons.Back },
                new[] { Keys.Back, Keys.Escape }, true);
        }

        public override void Activate()
        {
            if(_content == null) _content = _content = new ContentManager(ScreenManager.Game.Services, "Content");

            font = _content.Load<SpriteFont>("GoudyStout");
            high_score_font = _content.Load<SpriteFont>("Score");

            disc = new Disc();
            basket = new DiscBasket(new Vector2(1750, 540));

            //Generate random wind for the start
            windAngle = (float)(rand.NextDouble() * 2 * Math.PI);
            windDirection = new Vector2((float)Math.Cos(windAngle), (float)Math.Sin(windAngle));

            bushes = new Bush[]
            {
                new Bush(new Vector2(50,0), GameProject1.Direction.Down, true),
                new Bush(new Vector2(1200,1000), GameProject1.Direction.Right, false),
                new Bush(new Vector2(1700,400), GameProject1.Direction.Left, false),
                new Bush(new Vector2(350,700), GameProject1.Direction.Up, true),
                new Bush(new Vector2(250,1000), GameProject1.Direction.Right, false),
                new Bush(new Vector2(600,125), GameProject1.Direction.Right, false),
                new Bush(new Vector2(725,450), GameProject1.Direction.Up, true),
                new Bush(new Vector2(1125,350), GameProject1.Direction.Down, true)
            };
            trees = new Tree[]
            {
                 new Tree(new Vector2(960, 540), TreeType.tall_green_tree),
                 new Tree(new Vector2(200, 25), TreeType.tall_green_tree),
                 new Tree(new Vector2(700, 700), TreeType.tall_green_tree),
                 new Tree(new Vector2(960, 200), TreeType.stump),
                 new Tree(new Vector2(375, 175), TreeType.stump),
                 new Tree(new Vector2(1150, 150), TreeType.normal_green_tree),
                 new Tree(new Vector2(200, 400), TreeType.normal_green_tree),
                 new Tree(new Vector2(1000, 800), TreeType.pink_tree),
                 new Tree(new Vector2(600, 400), TreeType.pink_tree),
                 new Tree(new Vector2(1800, 50), TreeType.pink_tree),
                 new Tree(new Vector2(1600, 700), TreeType.normal_brown_tree),
                 new Tree(new Vector2(150, 850), TreeType.normal_brown_tree),
                 new Tree(new Vector2(575, 700), TreeType.stump),
                 new Tree(new Vector2(1500, 225), TreeType.dead_tree),
                 new Tree(new Vector2(75, 700), TreeType.dead_tree),
                 new Tree(new Vector2(1250, 625), TreeType.normal_green_tree)
            };
            basket.LoadContent(_content);
            windArrow = _content.Load<Texture2D>("windArrow");
            foreach (Bush bush in bushes) bush.LoadContent(_content);
            foreach (Tree tree in trees) tree.LoadContent(_content);
            disc.LoadContent(_content);
            font = _content.Load<SpriteFont>("GoudyStout");
            high_score_font = _content.Load<SpriteFont>("Score");

            //Particle engine stuff
            Rectangle source = windAngle < Math.PI ? new Rectangle(-960, -20, 1920 * 2, 10) : new Rectangle(-960, 1100, 1920 * 2, 10);

            windParticleSystem = new WindParticleSystem(ScreenManager.Game, source, windDirection);
            ScreenManager.Game.Components.Add(windParticleSystem);

            Color[] fireworkColors = new Color[]
            {
                Color.Fuchsia,
                Color.Red,
                Color.Crimson,
                Color.CadetBlue,
                Color.Aqua,
                Color.HotPink,
                Color.LimeGreen
            };

            Color[] bushHitColors = new Color[]
            {
                Color.Brown,
                Color.DarkGreen,
                Color.Tan,
                Color.SandyBrown,
                Color.DarkRed,
                Color.LightGreen
            };
            basketParticleSystem = new ExplosionParticleSystem(ScreenManager.Game, 20, fireworkColors, true);
            ScreenManager.Game.Components.Add(basketParticleSystem);

            bushParticleSystem = new ExplosionParticleSystem(ScreenManager.Game, 20, bushHitColors, false);
            ScreenManager.Game.Components.Add(bushParticleSystem);

            //Music/Sound effects
            sfxBushHit = _content.Load<SoundEffect>("bush_hit");
            sfxChains = _content.Load<SoundEffect>("DG-Putt");
            backgroundMusic = _content.Load<Song>("background_music");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(backgroundMusic);
            base.Activate();
        }

        public override void Deactivate()
        {
            base.Deactivate();
        }

        public override void Unload()
        {
            _content.Unload();
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
            if (coveredByOtherScreen)
                _pauseAlpha = Math.Min(_pauseAlpha + 1f / 32, 1);
            else
                _pauseAlpha = Math.Max(_pauseAlpha - 1f / 32, 0);

        }

        public override void HandleInput(GameTime gameTime, InputState input)
        {
            base.HandleInput(gameTime, input);
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            // Look up inputs for the active player profile.
            int playerIndex = (int)ControllingPlayer.Value;

            var currentKeyboardState = input.CurrentKeyboardStates[playerIndex];
            var currentGamePadState = input.CurrentGamePadStates[playerIndex];

            bool gamePadDisconnected = !currentGamePadState.IsConnected && input.GamePadWasConnected[playerIndex];

            float boost = 1.0f;
            PlayerIndex player;
            if (_pauseAction.Occurred(input, ControllingPlayer, out player) || gamePadDisconnected)
            {
                ScreenManager.AddScreen(new PauseMenuScreen(), ControllingPlayer);
            }
            else
            {
                #region Direction Input
                //Get position from GamePad, modified this slightly to flip the analog stick directions to what I am use to
                Direction = new Vector2(currentGamePadState.ThumbSticks.Left.X, currentGamePadState.ThumbSticks.Left.Y * -1) * 250 * (currentGamePadState.Triggers.Right + 1) * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if(currentKeyboardState.IsKeyDown(Keys.LeftShift) || currentKeyboardState.IsKeyDown(Keys.LeftShift))
                {
                    boost = 2.0f;
                }
                    //Get position from Keyboard
                    if (currentKeyboardState.IsKeyDown(Keys.Left) || currentKeyboardState.IsKeyDown(Keys.A))
                {
                    Direction += new Vector2(-200 * (float)gameTime.ElapsedGameTime.TotalSeconds * boost, 0);
                }
                if (currentKeyboardState.IsKeyDown(Keys.Right) || currentKeyboardState.IsKeyDown(Keys.D))
                {
                    Direction += new Vector2(200 * (float)gameTime.ElapsedGameTime.TotalSeconds * boost, 0);
                }
                if (currentKeyboardState.IsKeyDown(Keys.Up) || currentKeyboardState.IsKeyDown(Keys.W))
                {
                    Direction += new Vector2(0, -200 * (float)gameTime.ElapsedGameTime.TotalSeconds * boost);
                }
                if (currentKeyboardState.IsKeyDown(Keys.Down) || currentKeyboardState.IsKeyDown(Keys.S))
                {
                    Direction += new Vector2(0, 200 * (float)gameTime.ElapsedGameTime.TotalSeconds * boost);
                }
                #endregion

                if (input_enabled) disc.Update(gameTime, windDirection, Direction);

                current_round_time += (float)gameTime.ElapsedGameTime.TotalSeconds;
                disc.Color = Color.White;

                foreach (Bush bush in bushes)
                {
                    bush.Update(gameTime);
                    if (bush.Bounds.CollidesWith(disc.Bounds) && !is_hit_by_bush) //If you don't add the is_hit check the sound effect will murder your ears :)
                    {
                        sfxBushHit.Play();
                        disc.Color = Color.Red;
                        is_hit_by_bush = true;
                    }
                }
                if (disc.Bounds.CollidesWith(basket.Bounds) && !is_in_basket)
                {
                    sfxChains.Play();
                    disc.Color = Color.Green;
                    is_in_basket = true;
                }

            }

        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.GraphicsDevice.Clear(Color.ForestGreen);
            
            var _spriteBatch = ScreenManager.SpriteBatch;
            //If it isn't shaking make it the identity matrix
            Matrix shakeTransform = Matrix.Identity;

            if (is_hit_by_bush)
            {
                shakeTransform = Matrix.CreateTranslation(20 * MathF.Sin(elapsed_text_time * 1000), 20 * MathF.Cos(elapsed_text_time * 1000), 0);
            }

            // TODO: Add your drawing code here
            _spriteBatch.Begin(transformMatrix: shakeTransform);

            //Draw class objects
            disc.Draw(_spriteBatch);
            foreach (Bush bush in bushes) bush.Draw(gameTime, _spriteBatch);
            foreach (Tree tree in trees) tree.Draw(gameTime, _spriteBatch);

            //Had to rotate by another 90 degrees (radians) because arrow sprite was draw straight north in the sprite (rather then where the unit circle starts)
            _spriteBatch.Draw(windArrow, new Vector2(1375, 64), null, Color.White, windAngle + (float)(Math.PI / 2.0), new Vector2(16, 16), 1f, SpriteEffects.None, 1);
            _spriteBatch.DrawString(high_score_font, "WIND", new Vector2(1325, 120), Color.Red);

            #region High Score Drawing
            _spriteBatch.DrawString(high_score_font, current_round_time.ToString("n3") + " seconds", new Vector2(2, 2), Color.Gold);
            _spriteBatch.DrawString(high_score_font, "High Score: " + high_score_time.ToString("n3") + " seconds", new Vector2(1500, 2), Color.Gold);
            #endregion

            //Obstacle/winning logic, disables input either way and the resets player
            if (is_hit_by_bush)
            {
                if (elapsed_text_time < 1.5)
                {
                    input_enabled = false;
                    _spriteBatch.DrawString(font, "BONK!", new Vector2(800, 500), Color.Red);
                    elapsed_text_time += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    GamePad.SetVibration(0, 0.5f, 0.5f);
                    bushParticleSystem.PlaceExplosion(disc.Position);
                }
                else
                {
                    elapsed_text_time = 0f;
                    disc.Position = new Vector2(250, 250);
                    input_enabled = true;
                    is_hit_by_bush = false;
                    GamePad.SetVibration(0, 0, 0);
                    current_round_time = 0.0f;
                }
            }
            if (is_in_basket)
            {
                basket.DrawWin(gameTime, _spriteBatch);
                if (elapsed_text_time < 1.5)
                {
                    input_enabled = false;
                    _spriteBatch.DrawString(font, "CHAINS!", new Vector2(800, 500), Color.Orange);
                    elapsed_text_time += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    GamePad.SetVibration(0, 0.5f, 0.5f);
                    if (current_round_time < high_score_time)
                    {
                        high_score_time = current_round_time;
                    }
                    basketParticleSystem.PlaceExplosion(new Vector2(basket.Bounds.X + basket.Bounds.Width/2, basket.Bounds.Y + basket.Bounds.Height / 2));
                }
                else
                {
                    elapsed_text_time = 0f;
                    disc.Position = new Vector2(250, 250);
                    input_enabled = true;
                    is_in_basket = false;
                    GamePad.SetVibration(0, 0, 0);
                    current_round_time = 0.0f;

                    //generate a new wind each time the player wins
                    windAngle = (float)(rand.NextDouble() * 2 * Math.PI);
                    windDirection = new Vector2((float)Math.Cos(windAngle), (float)Math.Sin(windAngle));

                    //Update particle enginer for new wind
                    windParticleSystem.Source = GetWindSourceRect();
                    windParticleSystem.WindDirection = windDirection;
                }
            }
            else
            {
                basket.Draw(_spriteBatch); //Just draw the normal basket
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        //This method returns a source rectangle for where wind particles should spawn 
        //This was a fun refresher of the unit circle lol
        private Rectangle GetWindSourceRect()
        {
            const double PI = Math.PI;

            if(windAngle > PI / 4 && windAngle < 3 * PI / 4)
            {
                //wind is coming from the top
                return new Rectangle(-960, -20, 1920 * 2, 10);
            }
            else if(windAngle > 3 * PI / 4 && windAngle < 5 * PI / 4)
            {
                //wind is coming from the left
                return new Rectangle(1940, -540, 10, 1080 * 2);
            }
            else if(windAngle > 5 * PI / 4 && windAngle < 7 * PI / 4)
            {
                //wind is coming from the bottom
                return new Rectangle(-960, 1100, 1920 * 2, 10);
            }
            else
            {
                //wind is coming from the right
                return new Rectangle(-20, -540, 10, 1080 * 2);
            }
        }
    }
}
