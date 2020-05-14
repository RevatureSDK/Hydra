using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Hydra
{
    public class Enemy : Object2D
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        private int currentFrame;
        private int totalFrames;

        public Enemy(Texture2D textureFireball, int startingX, int startingY, int rows, int columns)
        {
            Texture = textureFireball;
            Position.X = startingX;
            Position.Y = startingY;
            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            totalFrames = Rows * Columns;
        }

        public override void Update()
        {
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            BoundingBox = new Rectangle((int)Position.X, (int)Position.Y, width, height);
        }

        public void Update(int WindowWidth, int WindowHeight)
        {
            Vector2 velocity = new Vector2(0, 3.0f);
            Position += velocity;
            this.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            int row = (int)((float)currentFrame / (float)Columns);
            int column = currentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)Position.X, (int)Position.Y, width, height);

            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
        }
    }
}