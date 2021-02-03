using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace CollisionExample.Collisions
{
    /// <summary>
    /// A struct representing a circular bounds
    /// </summary>
    public struct BoundingCircle
    {
        /// <summary>
        /// The center of the bounding circle
        /// </summary>
        public Vector2 Center;

        /// <summary>
        /// The radius of the bounding circle
        /// </summary>
        public float Radius;

        /// <summary>
        /// Constructs a new circle
        /// </summary>
        /// <param name="center">The center of circle</param>
        /// <param name="radius">The radius</param>
        public BoundingCircle(Vector2 center, float radius)
        {
            Center = center;
            Radius = radius;
        }

        /// <summary>
        /// Tests for collision between this and another bounding circle
        /// </summary>
        /// <param name="other">The other circle</param>
        /// <returns>True if we have a collision false otherwise</returns>
        public bool CollidesWith(BoundingCircle other)
        {
            return CollisionHelper.Collides(this, other);
        }

        /// <summary>
        /// Tests for collision between this and building rectangle
        /// </summary>
        /// <param name="other">The rectangle</param>
        /// <returns>True if we have a collision false otherwise</returns>
        public bool CollidesWith(BoundingRectangle other)
        {
            return CollisionHelper.Collides(this, other);
        }

    }
}
