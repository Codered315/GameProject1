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

        private double animationTimer = 0;

        private int animationFrame = 0;

        /// <summary>
        /// BoundingRectangle of the basket (just chains)
        /// </summary>
        public BoundingRectangle Bounds { get => bounds; set => bounds = value; }


        /// <summary>
        /// Creation of the disc basket (goal)
        /// </summary>
        /// <param name="pos">The position of the basket</param>
        public DiscBasket(Vector2 pos)
        {
            this.position = pos;
            this.bounds = new BoundingRectangle(new Vector2(pos.X + 18, pos.Y + 14), 90, 50);
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
        /// <param name="spriteBatch">current sprite batch</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            animationFrame = 0; //Reset animation frame to original basket
            if (texture is null) throw new InvalidOperationException("Texture must be loaded to render");

            Rectangle source =new Rectangle(0, 0, 128, 128);

            spriteBatch.Draw(texture, position, source, Color.White);
        }

        /// <summary>
        /// Draws the basket animation when the player has won
        /// </summary>
        /// <param name="gametime">gameTime object, used to determine what animation frame we are on</param>
        /// <param name="spriteBatch">current sprite batch</param>
        public void DrawWin(GameTime gametime, SpriteBatch spriteBatch)
        {
            if (texture is null) throw new InvalidOperationException("Texture must be loaded to render");

            animationTimer += gametime.ElapsedGameTime.TotalSeconds;

            if (animationTimer > 0.4)
            {
                animationFrame++;
                if (animationFrame > 3) animationFrame = 0;

                animationTimer -= 0.4;
            }
            Rectangle? source = animationFrame == 0 ? new Rectangle(0, 0, 128, 128) : new Rectangle(0, animationFrame * 128, 128, 128);

            spriteBatch.Draw(texture, position, source, Color.White);
        }

    }
}
