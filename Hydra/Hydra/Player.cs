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
        private const int  STARTING_POSITIONX = 310;
        private const int  STARTING_POSITIONY = 400;
        private const int INITIAL_SPEED = 4;
        private State currentState;
        private int totalFps = 0;
        private Texture2D TextureLI;
        private Texture2D TextureRI;
        private Texture2D TextureLW;
        private Texture2D TextureRW;
        private bool hasJumped;
        public bool reachedExit = false;
        public bool alive;
        public int lives = 1;

        public Player(int startingX, int startingY, Texture2D textureLI, Texture2D textureRI, Texture2D textureLW, Texture2D textureRW, int rows, int columns)
        {
            Texture = textureRI;
            TextureLI = textureLI;
            TextureRI = textureRI;
            TextureLW = textureLW;
            TextureRW = textureRW;
            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            totalFrames = Rows * Columns;
            inputHelper = new InputHelper();
            Speed = INITIAL_SPEED;
            Position = new Vector2(startingX, startingY);
            Velocity = new Vector2(0, 0);
            hasJumped = true;
            currentState = State.Idle;
            alive = true;
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

        public void Update(int WindowWidth, int WindowHeight, List<Object2D> objects)
        {
            this.Update();
            inputHelper.Update();
            Move(WindowHeight, WindowWidth);

            if (Velocity.X == 0)
            {
                currentState = State.Idle;
            }

            if (Velocity.X > 0 && currentState == State.Walking)
            {
                Texture = TextureRW;
            }
             else if (Velocity.X < 0 && currentState == State.Walking)
            {
                Texture = TextureLW;
            }
            
            if (currentState == State.Idle && Texture == TextureLW)
            {
                Texture = TextureLI;
            }
            else if (currentState == State.Idle && Texture == TextureRW)
            {
                Texture = TextureRI;
            }

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

                    if (obj.Texture.Name == "Flag")
                    {
                        reachedExit = true;
                    }
                    if (obj.Texture.Name == "Damage")
                    {
                        alive = false;
                    }
                }

                if ((Velocity.Y >= 0 && this.IsTouchingBottom(obj)) ||
                    (Velocity.Y <= 0 && this.IsTouchingTop(obj)))
                {
                    if (this.IsTouchingBottom(obj))
                    {
                        floor = false;
                    }

                    Velocity.Y = 0;

                    if (obj.Texture.Name == "Flag")
                    {
                        reachedExit = true;
                    }
                    if (obj.Texture.Name == "Damage")
                    {
                        alive = false;
                    }
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
            {
                return jump;
            }
        }

        private void Move(int WindowHeight, int WindowWidth)
        {
            if (inputHelper.IsKeyDown(Keys.Right) || inputHelper.IsKeyDown(Keys.D))
            {
                currentState = State.Walking;          
                if (Position.X <= WindowWidth - Texture.Width)
                {
                    Velocity.X = Speed;                  
                }
            }

            if (inputHelper.IsKeyDown(Keys.Left) || inputHelper.IsKeyDown(Keys.A))
            {
                currentState = State.Walking;
                if (Position.X >= 0)
                {
                    Velocity.X = -Speed;
                }
            }

            if (Position.Y >= WindowHeight + Texture.Height)
            {
                alive = false;
            }

            if ((inputHelper.IsKeyDown(Keys.Up) || inputHelper.IsKeyDown(Keys.Space) || inputHelper.IsKeyDown(Keys.W)) && hasJumped == false)
            {
                // currentState = State.Jumping;
                if (Position.Y >= 0)
                {
                    Velocity.Y = -12;                
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

            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
        }
    }
}