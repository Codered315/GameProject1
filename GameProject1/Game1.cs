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

        private InputManager inputManager;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            disc = new Disc(this) { Position = new Vector2(250, 250) };

            inputManager = new InputManager();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            basket = Content.Load<Texture2D>("DiscBasket");
            disc.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            inputManager.Update(gameTime);

            if (inputManager.Exit) Exit();

            disc.Position += inputManager.Direction;

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.ForestGreen);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            disc.Draw(_spriteBatch);
            _spriteBatch.Draw(basket, new Vector2(400, 200), Color.White);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
