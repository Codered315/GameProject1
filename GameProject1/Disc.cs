using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject1
{
    public class Disc
    {
        /// <summary>
        /// The game this disc is apart of
        /// </summary>
        Game game;

        /// <summary>
        /// The texture to be applied to the disc
        /// </summary>
        Texture2D texture;

        /// <summary>
        /// The position of the disc on the screen
        /// </summary>
        public Vector2 Position { get; set;}

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
            texture = game.Content.Load<Texture2D>("disc");
        }

        /// <summary>
        /// Draws the Disc sprite on screen at the given position
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            if (texture is null) throw new InvalidOperationException("Texture must be loaded to render");
            spriteBatch.Draw(texture, Position, Color.White);
        }
    }
}
