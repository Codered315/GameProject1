using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CollisionExample.Collisions;

namespace GameProject1
{
    public class Disc
    {
        private Game game;

        private Texture2D texture;

        private InputManager inputManager;

        private BoundingRectangle bounds = new BoundingRectangle(new Vector2(250 +  5,250 + 25), 53, 12);

        private Vector2 position = new Vector2(250, 250);

        /// <summary>
        /// The color of the sprite
        /// </summary>
        public Color Color { get; set; } = Color.White;

        /// <summary>
        /// The bounding rectangle for the disc
        /// </summary>
        public BoundingRectangle Bounds => bounds;

        /// <summary>
        /// The position of the disc
        /// </summary>
        public Vector2 Position { get => position; set => position = value; }

        /// <summary>
        /// Constructor for the Disc instance 
        /// </summary>
        /// <param name="game"></param>
        public Disc(Game game)
        {
            this.game = game;
        }

        /// <summary>
        /// Loads the disc content
        /// </summary>
        public void LoadContent()
        {
            texture = game.Content.Load<Texture2D>("DiscPixel");
            inputManager = new InputManager();
        }

        public void Update(GameTime gameTime, Vector2 windDir)
        {
            inputManager.Update(gameTime);
            position += Vector2.Add(inputManager.Direction, windDir);

            //Keeps the disc on screen
            var viewport = game.GraphicsDevice.Viewport;
            if (position.Y < 0) position.Y = 0;
            if (position.Y > viewport.Height - 40) position.Y = viewport.Height - 40;
            if (position.X < 0) position.X = 0;
            if (position.X > viewport.Width - 59) position.X = viewport.Width - 59;

            bounds.X = position.X + 5;
            bounds.Y = position.Y + 25;
        }

        /// <summary>
        /// Draws the Disc sprite on screen at the given position
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            if (texture is null) throw new InvalidOperationException("Texture must be loaded to render");
            spriteBatch.Draw(texture, position, Color);
        }
    }
}
