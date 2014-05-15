using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AR_Geometric.GameObjects.Core
{
    public class StringManager 
    {
        private string strToPrint;
        public string StrToPrint
        {
            get { return strToPrint; }
            set { strToPrint = value; }
        }

        private SpriteBatch spriteBatch;
        private SpriteFont font;

        public StringManager(Game game, string toPrint)
        {
            spriteBatch = (SpriteBatch)game.Services.GetService(typeof(SpriteBatch));
            font = ContentSingleton.Singleton.Content.Load<SpriteFont>("SpriteFont");
            strToPrint = toPrint;
        }

        public void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.DrawString(font, strToPrint, new Vector2(15, 15), Color.White);

            spriteBatch.End();
         }

    }
}
