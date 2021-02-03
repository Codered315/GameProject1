using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameProject1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Disc disc;
        private Texture2D basket;
        private Texture2D trees_tile;

        private Bush[] bushes;

        private InputManager inputManager;

        private SpriteFont font;

        private bool is_collided = false;

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
            inputManager = new InputManager();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            basket = Content.Load<Texture2D>("DiscBasket");
            trees_tile = Content.Load<Texture2D>("trees");
            foreach (Bush bush in bushes) bush.LoadContent(Content);
            disc.LoadContent();
            font = Content.Load<SpriteFont>("GoudyStout");
        }

        protected override void Update(GameTime gameTime)
        {
            inputManager.Update(gameTime);

            if (inputManager.Exit) Exit();

            disc.Update(gameTime);
            disc.Color = Color.White;

            is_collided = false;
            foreach (Bush bush in bushes)
            {
                bush.Update(gameTime);
                if(bush.Bounds.CollidesWith(disc.Bounds))
                {
                    disc.Color = Color.Red;
                    is_collided = true;
                }
            }
            // TODO: Add your update logic here
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.ForestGreen);

            //Get the width and height of viewport for spawning sprites
            int width = GraphicsDevice.Viewport.Width;
            int height = GraphicsDevice.Viewport.Height;

            //These are source rectangles for each sprite on the tree sheet
            Rectangle tall_green_tree = new Rectangle(0, 0, 64, 153);
            Rectangle normal_green_tree = new Rectangle(127, 286, 98, 130);
            Rectangle normal_brown_tree = new Rectangle(225, 292, 94, 117);
            Rectangle pink_tree = new Rectangle(189, 0, 98, 131);
            Rectangle stump = new Rectangle(355, 71, 56, 48);
            Rectangle dead_tree = new Rectangle(4, 193, 90, 95);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            disc.Draw(_spriteBatch);
            foreach (Bush bush in bushes) bush.Draw(gameTime, _spriteBatch);
            _spriteBatch.Draw(basket, new Vector2(1750, height/2), Color.White);

            _spriteBatch.Draw(trees_tile, new Vector2(width / 2, height / 2), tall_green_tree, Color.White);
            _spriteBatch.Draw(trees_tile, new Vector2(200, 25), tall_green_tree, Color.White);
            _spriteBatch.Draw(trees_tile, new Vector2(700, 700), tall_green_tree, Color.White);
            _spriteBatch.Draw(trees_tile, new Vector2(960, 200), stump, Color.White);
            _spriteBatch.Draw(trees_tile, new Vector2(375, 175), stump, Color.White);
            _spriteBatch.Draw(trees_tile, new Vector2(1150, 150), normal_green_tree, Color.White);
            _spriteBatch.Draw(trees_tile, new Vector2(200, 400), normal_green_tree, Color.White);
            _spriteBatch.Draw(trees_tile, new Vector2(1000, 800), pink_tree, Color.White);
            _spriteBatch.Draw(trees_tile, new Vector2(600, 400), pink_tree, Color.White);
            _spriteBatch.Draw(trees_tile, new Vector2(1800, 50), pink_tree, Color.White);
            _spriteBatch.Draw(trees_tile, new Vector2(1600, 700), normal_brown_tree, Color.White);
            _spriteBatch.Draw(trees_tile, new Vector2(150, 850), normal_brown_tree, Color.White);
            _spriteBatch.Draw(trees_tile, new Vector2(575, 700), stump, Color.White);
            _spriteBatch.Draw(trees_tile, new Vector2(1500, 225), dead_tree, Color.White);
            _spriteBatch.Draw(trees_tile, new Vector2(75, 700), dead_tree, Color.White);
            _spriteBatch.Draw(trees_tile, new Vector2(1250, 625), normal_green_tree, Color.White);

            if (is_collided) _spriteBatch.DrawString(font, "BONK!", new Vector2(800, 500), Color.Red);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
