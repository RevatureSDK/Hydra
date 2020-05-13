using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hydra
{
    public class Object2D
    {
        public Vector2 Position;
        public Texture2D Texture;
        public Rectangle BoundingBox;
        public Vector2 Velocity;
        public float Speed;
        public TileCollision Collision;

        public enum TileCollision
        {
            /// <summary>
            /// A passable tile is one which does not hinder player motion at all.
            /// </summary>
            Passable = 0,

            /// <summary>
            /// An impassable tile is one which does not allow the player to move through
            /// it at all. It is completely solid.
            /// </summary>
            Impassable = 1,

            /// <summary>
            /// A platform tile is one which behaves like a passable tile except when the
            /// player is above it. A player can jump up through a platform as well as move
            /// past it to the left and right, but can not fall down through the top of it.
            /// </summary>
            Platform = 2,
        }

        public bool Colliding(Object2D obj)
        {
            bool col = false;

            if (BoundingBox.Intersects(obj.BoundingBox))
            {
                col = true;
            }

            return col;
        }

        protected bool IsTouchingLeft(Object2D obj)
        {
            return this.BoundingBox.Right + this.Velocity.X > obj.BoundingBox.Left &&
                   this.BoundingBox.Left < obj.BoundingBox.Left &&
                   this.BoundingBox.Bottom > obj.BoundingBox.Top &&
                   this.BoundingBox.Top < obj.BoundingBox.Bottom;
        }

        protected bool IsTouchingRight(Object2D obj)
        {
            return this.BoundingBox.Left + this.Velocity.X < obj.BoundingBox.Right &&
                   this.BoundingBox.Right > obj.BoundingBox.Right &&
                   this.BoundingBox.Bottom > obj.BoundingBox.Top &&
                   this.BoundingBox.Top < obj.BoundingBox.Bottom;
        }

        protected bool IsTouchingBottom(Object2D obj)
        {
            return this.BoundingBox.Bottom + this.Velocity.Y >= obj.BoundingBox.Top &&
                   this.BoundingBox.Top < obj.BoundingBox.Top &&
                   this.BoundingBox.Right > obj.BoundingBox.Left &&
                   this.BoundingBox.Left < obj.BoundingBox.Right;
        }

        protected bool IsTouchingTop(Object2D obj)
        {
            return this.BoundingBox.Top + this.Velocity.Y < obj.BoundingBox.Bottom &&
                   this.BoundingBox.Bottom > obj.BoundingBox.Bottom &&
                   this.BoundingBox.Right > obj.BoundingBox.Left &&
                   this.BoundingBox.Left < obj.BoundingBox.Right;
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

        public virtual void Update(int offsetX, int offsetY, int Width, int Height)
        {
            BoundingBox = new Rectangle((int)Position.X + offsetX, (int)Position.Y + offsetY, Width, Height);
        }


        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }

        public void Dispose()
        {
            Texture.Dispose();
        }
    }
}