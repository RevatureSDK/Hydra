using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Hydra
{
    public class Background
    {
        public Rectangle Size;
        public float Scale = 1.0f;  //Used to size the Sprite up or down from the original image
        public Vector2 Position = new Vector2(0, 0);
        private Texture2D mSpriteTexture;

        public void LoadContent(ContentManager theContentManager, string AssetName)
        {
            mSpriteTexture = theContentManager.Load<Texture2D>(AssetName);
            Size = new Rectangle(0, 0, (int)(mSpriteTexture.Width * Scale), (int)(mSpriteTexture.Height * Scale));
        }

        public void Draw(SpriteBatch theSpriteBatch)
        {
            theSpriteBatch.Draw(mSpriteTexture, Position,
                new Rectangle(0, 0, mSpriteTexture.Width, mSpriteTexture.Height), Color.White,
                0.0f, Vector2.Zero, Scale, SpriteEffects.None, 0);
        }
    }
}
