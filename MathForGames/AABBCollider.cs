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

        public float Width
        {
            get { return _width; }
            set { _width = value; }
        }

        public float Height
        {
            get { return _height; }
            set { _height = value; }
        }

        public float Left
        {
            get
            {
                return Owner.Position - Height / 2;
            }
        }

        public float Right
        {
            get
            {
                return _height;
            }
        }

        public float Top
        {
            get
            {
                return _width;
            }
        }

        public float Bottom
        {
            get
            {
                _width;
            }
        }
    }
}
