using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hydra
{
    class MovableObject : Object2D
    {
        bool inAir = true;

        public MovableObject(Texture2D texture, int positionx, int positiony)
        {
            Texture = texture;
            Position = new Vector2(positionx, positiony);
            this.Update();
        }

        public Boolean CheckCollision(List<Object2D> objects)
        {
            foreach (var obj in objects)
            {
                if (obj != null)
                {
                    if (Velocity.Y >= 0)
                    {
                        if (this.IsTouchingBottom(obj))
                        {
                            Velocity.Y = 0;
                        }
                        else
                        {
                            Velocity.Y += 0.5f;
                        }
                    }


                    if ((Velocity.X > 0 && this.IsTouchingLeft(obj)) ||
                    (Velocity.X < 0 && this.IsTouchingRight(obj)))
                    {
                        Velocity.X = 0;
                        return true;
                    }
                }

            }
            return false;
        }

        public void Update(float velocityx, List<Object2D> objects)
        {
            Velocity.X = velocityx;

            CheckCollision(objects);

            //if (inAir == true)
            //{
            //    Velocity.Y += 0.5f;
            //}
            //else
            //{
            //    Velocity.Y = 0;
            //}

            Position += Velocity;
            this.Update();
        }
    }
}
