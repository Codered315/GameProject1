using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using CollisionExample.Collisions;

namespace GameProject1
{
    public class DiscBasket
    {
        private Texture2D texture;

        private Vector2 position;

        private BoundingRectangle bounds = new BoundingRectangle(new Vector2(1750 + 18, 540 + 14), 90, 50); //+ 18 and + 14 is to move hit box down to chains

        public BoundingRectangle Bounds => bounds;

        /// <summary>
        /// Creation of the disc basket (goal)
        /// </summary>
        /// <param name="pos">The position of the basket</param>
        public DiscBasket(Vector2 pos)
        {
            this.position = pos;
        }

        /// <summary>
        /// Loads the disc basket content
        /// </summary>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("DiscBasket");
        }

        /// <summary>
        /// Draws the Disc Basket sprite on screen at the given position
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            if (texture is null) throw new InvalidOperationException("Texture must be loaded to render");
            spriteBatch.Draw(texture, position, Color.White);
        }


    }
}
