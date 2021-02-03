using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameProject1
{
    public class InputManager
    {
        KeyboardState priorKeyboardState;
        KeyboardState currentKeyboardState;

        MouseState priorMouseState;
        MouseState currentMouseState;

        GamePadState priorGamePadState;
        GamePadState currentGamePadState;
        /// <summary>
        /// Current direction
        /// </summary>
        public Vector2 Direction { get; private set; }

        /// <summary>
        /// If the user has requested the game end
        /// </summary>
        public bool Exit { get; private set; } = false;
        public void Update(GameTime gameTime)
        {
            #region Updating Input State
            priorKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            priorMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();

            priorGamePadState = currentGamePadState;
            currentGamePadState = GamePad.GetState(PlayerIndex.One);
            #endregion

            #region Direction Input
            //Get position from GamePad, modified this slightly to flip the analog stick directions to what I am use to
            Direction = new Vector2(currentGamePadState.ThumbSticks.Right.X, currentGamePadState.ThumbSticks.Right.Y * -1) * 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;

            //Get position from Keyboard
            if (currentKeyboardState.IsKeyDown(Keys.Left) || currentKeyboardState.IsKeyDown(Keys.A))
            {
                Direction += new Vector2(-100 * (float)gameTime.ElapsedGameTime.TotalSeconds, 0);
            }
            if (currentKeyboardState.IsKeyDown(Keys.Right) || currentKeyboardState.IsKeyDown(Keys.D))
            {
                Direction += new Vector2(100 * (float)gameTime.ElapsedGameTime.TotalSeconds, 0);
            }
            if (currentKeyboardState.IsKeyDown(Keys.Up) || currentKeyboardState.IsKeyDown(Keys.W))
            {
                Direction += new Vector2(0, -100 * (float)gameTime.ElapsedGameTime.TotalSeconds);
            }
            if (currentKeyboardState.IsKeyDown(Keys.Down) || currentKeyboardState.IsKeyDown(Keys.S))
            {
                Direction += new Vector2(0, 100 * (float)gameTime.ElapsedGameTime.TotalSeconds);
            }
            #endregion

            #region Exit Input
            if (currentGamePadState.Buttons.Back == ButtonState.Pressed || currentKeyboardState.IsKeyDown(Keys.Escape))
                Exit = true;
            #endregion
        }
    }
}
