using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Hydra
{
    public class Tile : Object2D
    {
        public Tile(Texture2D texture, int positionx, int positiony)
        {
            Texture = texture;
            Position = new Vector2(positionx, positiony);
            this.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(Texture, Position, Color.White);
            spriteBatch.End();
        }
    }
}