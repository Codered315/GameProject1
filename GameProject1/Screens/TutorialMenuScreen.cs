using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Text;

namespace GameProject1.Screens
{
    public class TutorialMenuScreen : MenuScreen
    {
        private readonly MenuEntry _backMenuEntry;
        private ContentManager _content;
        private SpriteFont font;

        public TutorialMenuScreen() : base("Tutorial")
        {
            _backMenuEntry = new MenuEntry("Back");

            _backMenuEntry.Selected += OnCancel;
            _backMenuEntry.Position = new Vector2(960, 1000);

            MenuEntries.Add(_backMenuEntry);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            var _spriteBatch = ScreenManager.SpriteBatch;

            #region Loading Images for Tutorial
            if (_content == null)
                _content = new ContentManager(ScreenManager.Game.Services, "Content");

            font = _content.Load<SpriteFont>("Score");

            Texture2D bush   = _content.Load<Texture2D>("trees");
            Texture2D basket = _content.Load <Texture2D>("DiscBasket");
            Texture2D cancel = _content.Load<Texture2D>("cancel");
            Texture2D check  = _content.Load<Texture2D>("check");
            Texture2D arrows = _content.Load <Texture2D>("arrowKeys");
            Texture2D shift  = _content.Load<Texture2D>("shift");
            Texture2D disc   = _content.Load<Texture2D>("DiscPixel");
            Texture2D windArrow = _content.Load<Texture2D>("WindArrow");
            Texture2D wind = _content.Load<Texture2D>("wind");
            #endregion

            _spriteBatch.Begin();
            Rectangle vertical_bush = new Rectangle(301, 81, 38, 110);
            _spriteBatch.Draw(arrows, new Vector2(80, 200), Color.White);
            _spriteBatch.DrawString(font, "Disc Movement (or WASD Keys)", new Vector2(350, 270), Color.Black);
            _spriteBatch.Draw(shift, new Vector2(80, 370), new Rectangle(1, 25, 157, 92), Color.White);
            _spriteBatch.DrawString(font, "Disc Boost", new Vector2(350, 390), Color.Black);
            _spriteBatch.Draw(basket, new Vector2(80, 500), new Rectangle(0, 0, 128, 128), Color.White);
            _spriteBatch.Draw(check, new Vector2(250, 500), Color.White);
            _spriteBatch.Draw(bush, new Vector2(100, 750), vertical_bush, Color.White, 0.0f, new Vector2(0,0), 1.5f, SpriteEffects.None, 0);
            _spriteBatch.Draw(cancel, new Vector2(250, 720), Color.White);

            _spriteBatch.Draw(disc, new Vector2(1000, 300), null, Color.White, 0.0f, new Vector2(0,0), 3.0f, SpriteEffects.None, 0);
            _spriteBatch.Draw(windArrow, new Vector2(1350, 330), null, Color.White, (float)(Math.PI / 2.0), new Vector2(16, 16), 2.2f, SpriteEffects.None, 1);
            _spriteBatch.Draw(basket, new Vector2(1400, 300), new Rectangle(0, 0, 128, 128), Color.White);
            _spriteBatch.DrawString(font, "Put the Disc in the Basket in the Quickest Time Possible!", new Vector2(920, 450), Color.Black);

            _spriteBatch.Draw(wind, new Vector2(1100, 600), Color.White);
            _spriteBatch.DrawString(font, "Beware of the Kansas Wind!", new Vector2(1050, 800), Color.Black);

            _spriteBatch.End();
        }
    }
}
