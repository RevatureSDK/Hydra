using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Hydra
{
    public class Player
    {
        public Texture2D Texture { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        private int currentFrame;
        private int totalFrames;
        private InputHelper inputHelper;
        public Vector2 playerPosition;
        private const int  STARTING_POSITIONX = 400;
        private const int  STARTING_POSITIONY = 200;

        public Player(Texture2D texture, int rows, int columns)
        {
            Texture = texture;
            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            totalFrames = Rows * Columns;
            inputHelper = new InputHelper();
            playerPosition = new Vector2(STARTING_POSITIONX, STARTING_POSITIONY);
        }

        public void Update(int WindowHeight, int WindowWidth)
        {
            inputHelper.Update();

            if (inputHelper.IsKeyDown(Keys.Right) || inputHelper.IsKeyDown(Keys.D))
            {
                if (playerPosition.X <= WindowWidth)
                {
                    playerPosition.X += 2;
                }

            }

            if (inputHelper.IsKeyDown(Keys.Left) || inputHelper.IsKeyDown(Keys.A))
            {
                if (playerPosition.X >= 0)
                {
                    playerPosition.X -= 2;
                }
            }

            if (inputHelper.IsKeyDown(Keys.Down) || inputHelper.IsKeyDown(Keys.S))
            {
                if (playerPosition.Y <= WindowHeight)
                {
                    playerPosition.Y += 2;
                }
            }

            if (inputHelper.IsKeyDown(Keys.Up) || inputHelper.IsKeyDown(Keys.W))
            {
                if (playerPosition.Y >= 0)
                {
                    playerPosition.Y -= 2;
                }
            }

             
            currentFrame++;
            if (currentFrame == totalFrames)
                currentFrame = 0;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            int row = (int)((float)currentFrame / (float)Columns);
            int column = currentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);

            spriteBatch.Begin();
            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
            spriteBatch.End();
        }
    }
}