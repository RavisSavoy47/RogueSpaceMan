using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
