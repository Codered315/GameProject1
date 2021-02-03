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
        /// <summary>
        /// The game this disc is apart of
        /// </summary>
        private Game game;

        /// <summary>
        /// The texture to be applied to the disc
        /// </summary>
        private Texture2D texture;

        private InputManager inputManager;

        private BoundingRectangle bounds = new BoundingRectangle(new Vector2(250 +  5,250 + 25), 53, 12);

        /// <summary>
        /// The position of the disc on the screen
        /// </summary>
        private Vector2 position = new Vector2(250, 250);

        /// <summary>
        /// The color of the sprite
        /// </summary>
        public Color Color { get; set; } = Color.White;

        public BoundingRectangle Bounds => bounds;

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

        public void Update(GameTime gameTime)
        {
            inputManager.Update(gameTime);
            position += inputManager.Direction;

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
