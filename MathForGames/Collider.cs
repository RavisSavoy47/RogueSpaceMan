using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;

namespace MathForGames
{
    enum ColliderType
    {
        CIRCLE,
        AABB
    }

    abstract class Collider
    {
        private Actor _owner;
        private ColliderType _colliderType;

        public Actor Owner
        {
            get { return _owner; }
            set { _owner = value; }
        }

        public ColliderType ColliderType
        {
            get { return _colliderType; }
        }

        public Collider(Actor owner, ColliderType colliderType)
        {
            _owner = owner;
            _colliderType = colliderType;
        }

        /// <summary>
        /// Checks if their is a collision of type Collider Type circle or AABB
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool CheckCollision(Actor other)
        {
            if (other.Collider.ColliderType == ColliderType.CIRCLE)
                return CheckCollisionCircle((SphereCollider)other.Collider);
            else if (other.Collider.ColliderType == ColliderType.AABB)
                return CheckCollisionAABB((AABBCollider)other.Collider);
            return false;
        }

        public virtual bool CheckCollisionCircle(SphereCollider other) { return false; }

        public virtual bool CheckCollisionAABB(AABBCollider other) { return false; }

        public virtual void Draw()
        {

        }

        public virtual void Update()
        {

        }
    }
}
