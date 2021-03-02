using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Collisions.Collisions
{
    /// <summary>
    /// A struct representing a circle bound
    /// </summary>
    public struct BoundingCircle
    {
        /// <summary>
        /// Center Point for the Bounding Circle
        /// </summary>
        public Vector2 Center;

        /// <summary>
        /// Radius for the Bounding Circle
        /// </summary>
        public float Radius;

        public BoundingCircle(Vector2 centerVal, float radiusVal)
        {
            Center = centerVal;

            Radius = radiusVal;
        }

        /// <summary>
        /// Test for a collision between this and another BoundingCircle
        /// </summary>
        /// <param name="other">The other bounding Circle</param>
        /// <returns>true for a collision</returns>
        public bool CollidesWith(BoundingCircle other)
        {
            return CollisionHelper.Collides(this, other);
        }

        /// <summary>
        /// Test for a collision between this and another BoundingRectangle
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool CollidesWith(BoundingRectangle other)
        {
            return CollisionHelper.Collides(this,other);
        }
    }
}
