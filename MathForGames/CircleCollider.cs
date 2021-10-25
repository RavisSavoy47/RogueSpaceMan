using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary1;

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
            float distance = Vector2.Distance(other.Owner.Position, Owner.Position);
            //Find the length of the radii combined
            float combindRadii = other.CollisionRadius + CollisionRadius;

            return distance <= combindRadii;

        }
    }
}
