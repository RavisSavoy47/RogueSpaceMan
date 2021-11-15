using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{
    class AABBCollider : Collider
    {
        private float _width;
        private float _height;
        private float _length;

        /// <summary>
        /// The size of thsi collider on the x axis
        /// </summary>
        public float Width
        {
            get { return _width; }
            set { _width = value; }
        }

        /// <summary>
        /// The size of this collider pn the y axis
        /// </summary>
        public float Height
        {
            get { return _height; }
            set { _height = value; }
        }

        public float Length
        {
            get { return _length; }
            set { _length = value; }
        }

        /// <summary>
        /// The farthest left x position of this collider
        /// </summary>
        public float Left
        {
            get
            {
                return Owner.WorldPosition.X - Width / 2;
            }
        }

        /// <summary>
        /// The farthest right x position of this collider
        /// </summary>
        public float Right
        {
            get
            {
                return Owner.WorldPosition.X + Width / 2;
            }
        }

        /// <summary>
        /// The farthest position upwards
        /// </summary>
        public float Top
        {
            get
            {
                return Owner.WorldPosition.Y - Height / 2;
            }
        }

        /// <summary>
        /// The farthest posituion downwards
        /// </summary>
        public float Bottom
        {
            get
            {
                return Owner.WorldPosition.Y + Height / 2;
            }
        }

        /// <summary>
        /// The farthest posituion forwards
        /// </summary>
        public float Front
        {
            get
            {
                return Owner.WorldPosition.Z - Length / 2;
            }
        }

        /// <summary>
        /// The farthest posituion backwards
        /// </summary>
        public float Back
        {
            get
            {
                return Owner.WorldPosition.Z + Length / 2;
            }
        }

        public AABBCollider(float width, float height, float length, Actor owner) : base(owner, ColliderType.AABB)
        {
            _width = width;
            _height = height;
            _length = length;
        }

        public override bool CheckCollisionAABB(AABBCollider other)
        {
            //Return false if this owner is checking for a collision against itself
            if (other.Owner == Owner)
                return false;

            //Return true if there is an overlap between boxes
            if (other.Left <= Right &&
               other.Top <= Bottom &&
               Left <= other.Right &&
               Top <= other.Bottom &&
               other.Front <= Back &&
               Front <= other.Back)
            {
                return true;
            }

            //Return false if there is no overlap
            return false;
        }

        //Sets the collider size to the actors size
        public override void Update()
        {
            _height = Owner.Size.X;
            _width = Owner.Size.Y;
            _length = Owner.Size.Z;
        }

        //Checks if it collides with a sphere 
        public override bool CheckCollisionCircle(SphereCollider other)
        {
            return other.CheckCollisionAABB(this);
        }

        //Draws a cube for its collider 
        public override void Draw()
        {
            Raylib.DrawCube(new System.Numerics.Vector3(Owner.WorldPosition.X, Owner.WorldPosition.Y, Owner.WorldPosition.Z), 1, 1, 1, new Color(30, 200, 1, 100));           
            //Raylib.DrawRectangleLines((int)Left, (int)Top, (int)Width, (int)Height, Color.BLACK);
        }
    }
}
