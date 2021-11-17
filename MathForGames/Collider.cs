namespace MathForGames
{
    enum ColliderType
    {
        Sphere,
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
        /// Checks if their is a collision of type Collider Type sphere or AABB
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool CheckCollision(Actor other)
        {
            if (other.Collider.ColliderType == ColliderType.Sphere)
                return CheckCollisionSphere((SphereCollider)other.Collider);
            else if (other.Collider.ColliderType == ColliderType.AABB)
                return CheckCollisionAABB((AABBCollider)other.Collider);
            return false;
        }

        //Chceks if the collider collides with a sphere collider
        public virtual bool CheckCollisionSphere(SphereCollider other) { return false; }

        //Chceks if the collider collides with a AABB collider
        public virtual bool CheckCollisionAABB(AABBCollider other) { return false; }

        //Gives the colliders a draw
        public virtual void Draw()
        {
        }

        /// <summary>
        /// Gives the collidera draw
        /// </summary>
        public virtual void Update()
        {
        }
    }
}
