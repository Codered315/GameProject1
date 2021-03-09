using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace GameProject1
{
    public enum TreeType
    {
        tall_green_tree,
        normal_green_tree,
        normal_brown_tree,
        pink_tree,
        stump,
        dead_tree
    }
    public class Tree
    {
        private Texture2D texture;

        private Rectangle sourceRectangle;

        private Vector2 position;

        private TreeType tree;

        public Vector2 Position;

        public Tree(Vector2 pos, TreeType tree)
        {
            this.tree = tree;
            this.position = pos;
            switch(tree)
            {
                case TreeType.tall_green_tree:
                {
                    this.sourceRectangle = new Rectangle(0, 0, 64, 153);
                    break;
                }
                case TreeType.normal_green_tree:
                {
                    this.sourceRectangle = new Rectangle(127, 286, 98, 130);
                    break;
                }
                case TreeType.normal_brown_tree:
                {
                    this.sourceRectangle = new Rectangle(225, 292, 94, 117);
                    break;
                }
                case TreeType.pink_tree:
                {
                    this.sourceRectangle = new Rectangle(189, 0, 98, 131);
                    break;
                }
                case TreeType.stump:
                {
                    this.sourceRectangle = new Rectangle(355, 71, 56, 48);
                    break;
                }
                case TreeType.dead_tree:
                {
                    this.sourceRectangle = new Rectangle(4, 193, 90, 95);
                    break;
                }
                default: break;

            }
        }

        /// <summary>
        /// Loads the tree texture tile
        /// </summary>
        /// <param name="content">Content manager of game</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("trees");
        }

        /// <summary>
        /// Updates the tree (not needed? the position isnt changing but I will keep this as a placeholder)
        /// </summary>
        /// <param name="gameTime">Current gametime</param>
        public void Update(GameTime gameTime)
        {
            return;
        }

        /// <summary>
        /// Draws the tree on screen
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, this.position, this.sourceRectangle, Color.White);
        }
    }
}
