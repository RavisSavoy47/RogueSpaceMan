using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{
    class Enemy : Actor
    {
        private float _speed;
        private Vector3 _velocity;
        public Actor _target;
        private float _maxSightDistance;
        private float _maxViewAngle;

        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public Vector3 Velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }

        public Enemy(float x, float y, float z, float speed, float maxSightDistance, float maxViewAngle, 
            Actor target, string name = "Enemy", Shape shape = Shape.CUBE, float health = 0)
            : base(x, y, z, name, shape, health)
        {
            _target = target;
            _speed = speed;
            _maxSightDistance = maxSightDistance;
            _maxViewAngle = maxViewAngle;
        }

        /// <summary>
        /// Lets the enemy move 
        /// </summary>
        /// <param name="deltaTime"></param>
        /// <param name="currentScene"></param>
        public override void Update(float deltaTime, Scene currentScene)
        {
            //Create a vector that stores the move input
            Vector3 moveDirection = (_target.LocalPosition - LocalPosition).Normalized;

            Velocity = moveDirection * Speed * deltaTime;

            //Once target it in sight enemy moves
            if (GetTargetInSight())
            {
                LocalPosition += Velocity;

                LookAt(_target.WorldPosition);

                if (Velocity.Magnitude > 0)
                    Forward = Velocity.Normalized;
            }



            base.Update(deltaTime, currentScene);
        }

        /// <summary>
        /// Calls the collider draw
        /// </summary>
        public override void Draw()
        {
            base.Draw();
            Collider.Draw();
        }

        /// <summary>
        /// Lets the enemy see their target
        /// </summary>
        /// <returns>True if there is a target in sight</returns>
        public bool GetTargetInSight()
        {
            Vector3 directionOfTarget = (_target.LocalPosition + LocalPosition).Normalized;

            //Created a range for their sight
            float distanceOfTarget = Vector3.Distance(_target.LocalPosition, LocalPosition);

            float dotProduct = Vector3.DotProduct(directionOfTarget, Forward);

            return Math.Acos(dotProduct) < _maxViewAngle &&  distanceOfTarget < _maxSightDistance;

        }

        public override void OnCollision(Actor actor, Scene currentScene)
        {
            if(actor is Player)
            {
                Velocity *= -50;
                LocalPosition += Velocity;
                Health--;

            }

            else if(actor is Companion)
            {
                Velocity *= -50;
                LocalPosition += Velocity;
                Health--;
            }

            else if(actor is Bullet)
            {
                Health--;
            }

            if (Health <= 0)
            {
                currentScene.RemoveActor(this);
            }
        }
    }
}
