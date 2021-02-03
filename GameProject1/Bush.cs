using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace GameProject1
{
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

        public Direction Direction;

        public Vector2 Position;

        public bool Is_Vertical;

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("trees");
        }

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
        }

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
