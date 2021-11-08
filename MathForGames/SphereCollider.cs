using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary1;
using Raylib_cs;

namespace MathForGames
{
    class CircleCollider : Collider
    {
        public float _collisionRadius;

        public float CollisionRadius
        {
            get { return _collisionRadius; }
            set { _collisionRadius = value; }
        }

        public CircleCollider(float collisionRadius, Actor owner) : base(owner, ColliderType.CIRCLE)
        {
            _collisionRadius = collisionRadius;
        }

        public override bool CheckCollisionCircle(CircleCollider other)
        {
            if (other.Owner == Owner)
                return false;

            //Find the distance between teh two actors
            float distance = Vector3.Distance(other.Owner.LocalPosition, Owner.LocalPosition);
            //Find the length of the radii combined
            float combindRadii = other.CollisionRadius + CollisionRadius;

            return distance <= combindRadii;

        }

        public override bool CheckCollisionAABB(AABBCollider other)
        {
            //return false if this collider is checking collision against itself
            if (other.Owner == Owner)
                return false;

            //Get the direction from this collider to th eAABB
            Vector3 direction = Owner.LocalPosition - other.Owner.LocalPosition;

            //Clamp the direction vector to be within the bounds of the AABB
            direction.X = Math.Clamp(direction.X, -other.Width / 2, other.Width / 2);
            direction.Y = Math.Clamp(direction.Y, -other.Height / 2, other.Height / 2);
            direction.Z = Math.Clamp(direction.Z, -other.Height / 2, other.Height / 2);

            //Add the direction vector to the AABB center to get teh closest point to the circle
            Vector3 closestPoint = other.Owner.LocalPosition + direction;

            //Find teh distance from the circle's center to the closest point
            float distanceFromClosestPoint = Vector3.Distance(Owner.LocalPosition, closestPoint);

            //Return whether or not teh distance is less than the circle's radius
            return distanceFromClosestPoint <= CollisionRadius;
        }
        public override void Draw()
        {
            Raylib.DrawSphere(new System.Numerics.Vector3(Owner.WorldPosition.X, Owner.WorldPosition.Y, Owner.WorldPosition.Z), CollisionRadius, Color.GOLD);
        }
    }
}
