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

        public Tile(Texture2D texture, int positionx, int positiony, int offsetX, int offsetY, int width, int height)
        {
            Texture = texture;
            Position = new Vector2(positionx, positiony);
            this.Update(offsetX, offsetY, width, height);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }
    }

}