using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using CollisionExample.Collisions;

namespace GameProject1
{
    /// <summary>
    /// Enum of the possible bush directions
    /// </summary>
    public enum Direction
    {
        Down = 0,
        Right = 1,
        Up = 2,
        Left = 3
    }
    public class Bush
    {
        private Texture2D texture;

        private double directionTimer;

        private BoundingRectangle bounds;

        /// <summary>
        /// The current direction of the bush obstacle
        /// </summary>
        public Direction Direction;

        /// <summary>
        /// The current position of the bush
        /// </summary>
        public Vector2 Position;

        /// <summary>
        /// Determines if the bush should be vertical or horizontal on screen
        /// </summary>
        public bool Is_Vertical;

        /// <summary>
        /// The bounds of the bush
        /// </summary>
        public BoundingRectangle Bounds => bounds;

        /// <summary>
        /// Bush constructor 
        /// </summary>
        /// <param name="position">Position on screen</param>
        /// <param name="dir">direction to go</param>
        /// <param name="is_vert">Is it a vertical or horizontal bush</param>
        public Bush(Vector2 position, Direction dir, bool is_vert)
        {
            this.Position = position;
            this.Direction = dir;
            this.Is_Vertical = is_vert;
            if (is_vert)
            {
                this.bounds = new BoundingRectangle(Position, 38, 110);
            }
            else
            {
                this.bounds = new BoundingRectangle(Position, 127, 51);
            }
        }

        /// <summary>
        /// Loads the bush texture
        /// </summary>
        /// <param name="content">Content manager of game</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("trees");
        }

        /// <summary>
        /// Updates the bush direction/position/bounds on screen
        /// </summary>
        /// <param name="gameTime">Current gametime</param>
        public void Update(GameTime gameTime)
        {
            directionTimer += gameTime.ElapsedGameTime.TotalSeconds;

            if(directionTimer > 2.75)
            {
                if(Is_Vertical)
                {
                    switch(Direction)
                    {
                        case Direction.Down:
                            Direction = Direction.Up;
                            break;
                        case Direction.Up:
                            Direction = Direction.Down;
                            break;
                    }
                }
                else
                {
                    switch (Direction)
                    {
                        case Direction.Left:
                            Direction = Direction.Right;
                            break;
                        case Direction.Right:
                            Direction = Direction.Left;
                            break;
                    }
                }
                directionTimer -= 2.75;
            }

            switch(Direction)
            {
                case Direction.Up:
                    Position += new Vector2(0, -1) * 150 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    break;
                case Direction.Down:
                    Position += new Vector2(0, 1) * 150 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    break;
                case Direction.Left:
                    Position += new Vector2(-1, 0) * 150 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    break;
                case Direction.Right:
                    Position += new Vector2(1, 0) * 150 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    break;
            }
            bounds.X = Position.X;
            bounds.Y = Position.Y;
        }

        /// <summary>
        /// Draws the bush on screen
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if(Is_Vertical)
            {
                Rectangle vertical_bush = new Rectangle(301, 81, 38, 110);
                spriteBatch.Draw(texture, Position, vertical_bush, Color.White);
            }
            else
            {
                Rectangle horizontal_bush = new Rectangle(289, 12, 127, 51);
                spriteBatch.Draw(texture, Position, horizontal_bush, Color.White);
            }
        }
    }
}
