using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Hydra
{
    public class Player : Object2D
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        private int currentFrame;
        private int totalFrames;
        private InputHelper inputHelper;
        private const int  STARTING_POSITIONX = 400;
        private const int  STARTING_POSITIONY = 207;
        private const int INITIAL_SPEED = 4;
        private State currentState;
        private int totalFps = 0;
        private Texture2D TextureLeft;
        private Texture2D TextureRight;
        private bool hasJumped;
        private int Acceleration;
        private float test;

        public Player(Texture2D textureLeft, Texture2D textureRight, int rows, int columns)
        {
            Texture = textureRight;
            TextureLeft = textureLeft;
            TextureRight = textureRight;
            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            totalFrames = Rows * Columns;
            inputHelper = new InputHelper();
            //currentState = Idle;
            Speed = INITIAL_SPEED;
            Position = new Vector2(STARTING_POSITIONX, STARTING_POSITIONY);
            Velocity = new Vector2(0, 0);
            hasJumped = true;
            test = 0.1f;
        }

        enum State
        {
            Idle,
            Walking,
            Jumping
        }

        public override void Update()
        {
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            BoundingBox = new Rectangle((int)Position.X + 20, (int)Position.Y, width - 40, height - 5);
        }

        public void Update(int WindowHeight, int WindowWidth, List<Object2D> objects)
        {
            this.Update();
            inputHelper.Update();
            Move(WindowHeight, WindowWidth);

            hasJumped = CheckCollision(this, objects);

            Position += Velocity;
            Velocity.X = 0;

            totalFps++;
            if(totalFps % 10 == 0)
            {
                currentFrame++;
            }
            if (currentFrame == totalFrames)
                currentFrame = 0;
        }

        private bool CheckCollision(Object2D player, List<Object2D> objects)
        {
            bool jump = false;
            bool floor = true;
            foreach (var obj in objects)
            {
                if ((Velocity.X > 0 && this.IsTouchingLeft(obj)) ||
                    (Velocity.X < 0 && this.IsTouchingRight(obj)))
                {
                    Velocity.X = 0;
                }

                if ((Velocity.Y >= 0 && this.IsTouchingBottom(obj)) ||
                    (Velocity.Y <= 0 && this.IsTouchingTop(obj)))
                {
                    if (this.IsTouchingBottom(obj))
                    {
                        floor = false;
                    }

                    Velocity.Y = 0;
                }

                if (!this.IsTouchingBottom(obj))
                {
                    jump = true;
                }
            }
            if (floor == false)
            {
                return floor;
            }
            else
                return jump;
        }

        private void Move(int WindowHeight, int WindowWidth)
        {
            if (inputHelper.IsKeyDown(Keys.Right) || inputHelper.IsKeyDown(Keys.D))
            {
                Texture = TextureRight;              
                if (Position.X <= WindowWidth - Texture.Width)
                {
                    Velocity.X = Speed;
                }
            }

            if (inputHelper.IsKeyDown(Keys.Left) || inputHelper.IsKeyDown(Keys.A))
            {
                Texture = TextureLeft;
                if (Position.X >= 0)
                {
                    Velocity.X = -Speed;
                }
            }

            if ((inputHelper.IsKeyDown(Keys.Up) || inputHelper.IsKeyDown(Keys.Space) || inputHelper.IsKeyDown(Keys.W)) && hasJumped == false)
            {
                if (Position.Y >= 0)
                {
                    Velocity.Y = -14;                
                    hasJumped = true;
                }
            }

            if (hasJumped == true)
            {
                Velocity.Y += 0.5f;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            int row = (int)((float)currentFrame / (float)Columns);
            int column = currentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)Position.X, (int)Position.Y, width, height);

            spriteBatch.Begin();
            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
            spriteBatch.End();
        }
    }
}