using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Text;

namespace GameProject1.Screens
{
    public class EndScreen : MenuScreen
    {
        private readonly MenuEntry _restartMenuEntry;
        private readonly MenuEntry _exitMenuEntry;
        private ContentManager _content;
        private SpriteFont font;
        private float high_score;

        public EndScreen(float high_score) : base("End Screen")
        {
            this.high_score = high_score;

            _restartMenuEntry = new MenuEntry("Restart");
            _exitMenuEntry = new MenuEntry("Exit to Main");

            _restartMenuEntry.Selected += RestartGameMenuEntrySelected;
            _exitMenuEntry.Selected += ExitToMainEntrySelected;

            MenuEntries.Add(_restartMenuEntry);
            MenuEntries.Add(_exitMenuEntry);
        }

        // This uses the loading screen to transition from the game back to the first hole
        private void RestartGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, 0, new Hole1());
        }

        private void ExitToMainEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, 0, new MainMenuScreen());
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            var _spriteBatch = ScreenManager.SpriteBatch;

            if (_content == null)
                _content = new ContentManager(ScreenManager.Game.Services, "Content");

            font = _content.Load<SpriteFont>("GoudyStout");

            _spriteBatch.Begin();
            string high_score_string = "YOUR FINAL TIME: " + high_score.ToString("n3");
            Vector2 measure = font.MeasureString(high_score_string);
            _spriteBatch.DrawString(font, high_score_string, new Vector2(960 - (measure.X / 2), 540 - (measure.Y /2)), Color.Gold);
            string beat_string = "See if you can beat it!";
            Vector2 beat = font.MeasureString(high_score_string);
            _spriteBatch.DrawString(font, beat_string, new Vector2(960 - (measure.X / 2), 700 - (measure.Y / 2)), Color.Gold);
            _spriteBatch.End();
        }
    }
}
