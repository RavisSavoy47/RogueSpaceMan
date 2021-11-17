using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{
    class SphereCollider : Collider
    {
        public float _collisionRadius;

        public float CollisionRadius
        {
            get { return _collisionRadius; }
            set { _collisionRadius = value; }
        }

        public SphereCollider(float collisionRadius, Actor owner) : base(owner, ColliderType.Sphere)
        {
            _collisionRadius = collisionRadius;
        }

        /// <summary>
        /// Checks if it has collided with a sphere collider
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool CheckCollisionSphere(SphereCollider other)
        {
            if (other.Owner == Owner)
                return false;

            //Find the distance between teh two actors
            float distance = Vector3.Distance(other.Owner.WorldPosition, Owner.WorldPosition);
            //Find the length of the radii combined
            float combindRadii = other.CollisionRadius + CollisionRadius;

            return distance <= combindRadii;

        }

        /// <summary>
        /// Checks if it has collided with a AABB collider
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool CheckCollisionAABB(AABBCollider other)
        {
            //return false if this collider is checking collision against itself
            if (other.Owner == Owner)
                return false;

            //Get the direction from this collider to th eAABB
            Vector3 direction = Owner.WorldPosition - other.Owner.WorldPosition;

            //Clamp the direction vector to be within the bounds of the AABB
            direction.X = Math.Clamp(direction.X, -other.Width / 2, other.Width / 2);
            direction.Y = Math.Clamp(direction.Y, -other.Height / 2, other.Height / 2);
            direction.Z = Math.Clamp(direction.Z, -other.Length / 2, other.Length / 2);

            //Add the direction vector to the AABB center to get teh closest point to the circle
            Vector3 closestPoint = other.Owner.WorldPosition + direction;

            //Find teh distance from the circle's center to the closest point
            float distanceFromClosestPoint = Vector3.Distance(Owner.WorldPosition, closestPoint);

            //Return whether or not teh distance is less than the circle's radius
            return distanceFromClosestPoint <= CollisionRadius;
        }
        
        //Draws the sphere based on the owner's world position x, y, and z;
        public override void Draw()
        {
            Raylib.DrawSphere(new System.Numerics.Vector3(Owner.WorldPosition.X, Owner.WorldPosition.Y, Owner.WorldPosition.Z), CollisionRadius, new Color(200, 130, 20, 100));
        }
    }
}
