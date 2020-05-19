﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Hydra
{
    public class Fireball : Enemy
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        private int currentFrame;
        private int totalFrames;


        public Fireball(Texture2D enemyTexture, int startingX, int startingY, int rows, int columns) : base(enemyTexture, startingX, startingY, rows, columns)
        {
            Texture = enemyTexture;
            Position.X = startingX;
            Position.Y = startingY;
            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            totalFrames = Rows * Columns;
        }

        //public override void Update()
        //{
        //    int width = Texture.Width / Columns;
        //    int height = Texture.Height / Rows;
        //    BoundingBox = new Rectangle((int)Position.X, (int)Position.Y, width, height);
        //}

        public void Update(int WindowWidth, int WindowHeight)
        {
            Vector2 velocity = new Vector2(-3.0f, 0);
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