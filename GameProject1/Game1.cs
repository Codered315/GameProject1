using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace GameProject1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Disc disc;
        private DiscBasket basket;
        private Texture2D trees_tile;
        private Texture2D windArrow;

        private Bush[] bushes;
        private Tree[] trees;

        private InputManager inputManager;

        private SpriteFont font;
        private SpriteFont high_score_font;

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
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.ApplyChanges();
            Content.RootDirectory = "Content";
            Window.AllowUserResizing = true;
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            disc = new Disc(this);
            basket = new DiscBasket(new Vector2(1750, 540));

            //Generate random wind for the start
            windAngle = (float)(rand.NextDouble() * 2 * Math.PI);
            windDirection = new Vector2((float)Math.Cos(windAngle), (float)Math.Sin(windAngle));

            bushes = new Bush[]
            {
                new Bush(new Vector2(50,0), Direction.Down, true),
                new Bush(new Vector2(1200,1000), Direction.Right, false),
                new Bush(new Vector2(1700,400), Direction.Left, false),
                new Bush(new Vector2(350,700), Direction.Up, true),
                new Bush(new Vector2(250,1000), Direction.Right, false),
                new Bush(new Vector2(600,125), Direction.Right, false),
                new Bush(new Vector2(725,450), Direction.Up, true),
                new Bush(new Vector2(1125,350), Direction.Down, true)
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
            inputManager = new InputManager();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            basket.LoadContent(Content);
            trees_tile = Content.Load<Texture2D>("trees");
            windArrow = Content.Load<Texture2D>("windArrow");
            foreach (Bush bush in bushes) bush.LoadContent(Content);
            foreach (Tree tree in trees) tree.LoadContent(Content);
            disc.LoadContent();
            font = Content.Load<SpriteFont>("GoudyStout");
            high_score_font = Content.Load<SpriteFont>("Score");

            //Music/Sound effects
            sfxBushHit = Content.Load<SoundEffect>("bush_hit");
            sfxChains = Content.Load<SoundEffect>("DG-Putt");
            backgroundMusic = Content.Load<Song>("background_music");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(backgroundMusic);
        }

        protected override void Update(GameTime gameTime)
        {
            inputManager.Update(gameTime);
            current_round_time += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (inputManager.Exit) Exit();

            if(input_enabled) disc.Update(gameTime, windDirection);
            disc.Color = Color.White;

            foreach (Bush bush in bushes)
            {
                bush.Update(gameTime);
                if(bush.Bounds.CollidesWith(disc.Bounds) && !is_hit_by_bush) //If you don't add the is_hit check the sound effect will murder your ears :)
                {
                    sfxBushHit.Play();
                    disc.Color = Color.Red;
                    is_hit_by_bush = true;
                }
            }
            if(disc.Bounds.CollidesWith(basket.Bounds) && !is_in_basket)
            {
                sfxChains.Play();
                disc.Color = Color.Green;
                is_in_basket = true;
            }
            // TODO: Add your update logic here
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.ForestGreen);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            //Draw class objects
            disc.Draw(_spriteBatch);
            foreach (Bush bush in bushes) bush.Draw(gameTime, _spriteBatch);
            foreach (Tree tree in trees) tree.Draw(gameTime, _spriteBatch);

            //Had to rotate by another 90 degrees (radians) because arrow sprite was draw straight north in the sprite (rather then where the unit circle starts)
            _spriteBatch.Draw(windArrow, new Vector2(1375, 64), null, Color.White, windAngle + (float)(Math.PI/2.0), new Vector2(16, 16), 1f, SpriteEffects.None, 1);
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
                }
            }
            else
            {
                basket.Draw(_spriteBatch); //Just draw the normal basket
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
