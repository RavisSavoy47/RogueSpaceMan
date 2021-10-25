using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary1;

namespace MathForGames
{
    class AABBCollider : Collider
    {
        private float _width;
        private float _height;

        /// <summary>
        /// The size of thsi collider on the x axis
        /// </summary>
        public float Width
        {
            get { return _width; }
            set { _width = value; }
        }

        /// <summary>
        /// Teh size of this collider pn the y axis
        /// </summary>
        public float Height
        {
            get { return _height; }
            set { _height = value; }
        }

        /// <summary>
        /// The farthest left x position of this collider
        /// </summary>
        public float Left
        {
            get
            {
                return Owner.Position.X - Width / 2;
            }
        }

        /// <summary>
        /// The farthest right x position of this collider
        /// </summary>
        public float Right
        {
            get
            {
                return Owner.Position.X + Width / 2;
            }
        }

        /// <summary>
        /// The farthest top y position of this collider
        /// </summary>
        public float Top
        {
            get
            {
                return Owner.Position.Y - Height / 2;
            }
        }

        /// <summary>
        /// The farthest bottom y position of this collider
        /// </summary>
        public float Bottom
        {
            get
            {
                return Owner.Position.Y + Height / 2;
            }
        }
    }
}
