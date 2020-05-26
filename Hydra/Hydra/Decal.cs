using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hydra
{
    public class Decal
    {
        private Texture2D Texture;
        private Vector2 Position;

        public Decal(Texture2D Texture, Vector2 Position)
        {
            this.Texture = Texture;
            this.Position = Position;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }

        public void Dispose()
        {
            Texture.Dispose();
        }
    }
}