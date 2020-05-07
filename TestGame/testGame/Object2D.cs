using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testGame
{
    public class Object2D
    {
        public int Health = 100;
        public Vector2 Position;
        public Texture2D Texture;
        public Rectangle BoundingBox;

        public bool Colliding(Object2D obj)
        {
            bool col = false;

            if (BoundingBox.Intersects(obj.BoundingBox))
            {
                col = true;
            }

            return col;
        }

        public virtual void Init(Vector2 pos, Texture2D text)
        {
            Position = pos;
            Texture = text;
        }

        public virtual void Update()
        {
            BoundingBox = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }
    }
}