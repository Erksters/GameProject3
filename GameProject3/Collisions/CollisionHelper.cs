using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Collisions.Collisions
{
    public static class CollisionHelper
    {
        /// <summary>
        /// Detects collision between two BoundingCircles
        /// </summary>
        /// <param name="a">The first BoudningCircle</param>
        /// <param name="b">The second BoudningCircle</param>
        /// <returns></returns>
        public static bool Collides(BoundingCircle a, BoundingCircle b)
        {
            return Math.Pow(a.Radius + b.Radius , 2) >= 
                Math.Pow(a.Center.X - b.Center.X, 2) + 
                Math.Pow(a.Center.Y - b.Center.Y, 2);
        }

        /// <summary>
        /// Detects collision between two BoundingRectangles
        /// </summary>
        /// <param name="a">The first Bounding Rectangle</param>
        /// <param name="b">The Second Bounding Rectangle</param>
        /// <returns></returns>
        public static bool Collides(BoundingRectangle a, BoundingRectangle b)
        {
            return !(a.Right < b.Left || a.Left > b.Right
                || a.Top > b.Bottom || a.Bottom < b.Top
                );
        }

        /// <summary>
        /// Detects a collision between a rectangle and a circle
        /// </summary>
        /// <param name="c"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static bool Collides(BoundingCircle c, BoundingRectangle r)
        {
            float NearestX = MathHelper.Clamp(c.Center.X, r.Left, r.Right);
            float NearestY = MathHelper.Clamp(c.Center.Y, r.Top, r.Bottom);

            return Math.Pow(c.Radius, 2) >=
                Math.Pow(c.Center.X - NearestX, 2) +
                Math.Pow(c.Center.Y - NearestY, 2);
        }

        public static bool Collides(BoundingRectangle r, BoundingCircle c) => Collides(c, r);

    }
}
