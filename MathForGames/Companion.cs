using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{
    class Companion : Actor
    {
        private float _speed;
        private Vector3 _velocity;
        private float _timer = 0;
        private float _bulletDistance;
        public Actor _target;

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

        public Companion(float x, float y, float z, float speed, Actor target, string name = "Companion", Shape shape = Shape.CUBE)
            : base(x, y, z, name, shape)
        {
            _target = target;
            _speed = speed;
        }

        /// <summary>
        /// Lets the player move and shoot bullets 
        /// </summary>
        /// <param name="deltaTime">The timer</param>
        /// <param name="currentScene">Gets the currentScene from Scene</param>
        public override void Update(float deltaTime, Scene currentScene)
        {

            //Gives the bullets a cooldown timer
            _timer += deltaTime;

            if (friendsDirectionX != 0 && _timer >= .5 || friendsDirectionZ != 0 && _timer >= .5)
            {
                Bullet bullet = new Bullet(LocalPosition.X, LocalPosition.Y, LocalPosition.Z, targetsDirectionX, targetsDirectionZ, 10, "Bullet");
                bullet.SetScale(1, 1, 1);
                currentScene.AddActor(bullet);

                SphereCollider bulletCollider = new SphereCollider(1, bullet);
                bullet.Collider = bulletCollider;


                _timer = 0;
            }

            
            Vector3 moveDirection = new Vector3(xDirection, 0, zDirection);

            Velocity = moveDirection.Normalized * Speed * deltaTime;

            if (Velocity.Magnitude > 0)
                Forward = Velocity.Normalized;

            LocalPosition += Velocity;

            base.Update(deltaTime, currentScene);

        }

        public bool GetTargetInSight()
        {
            Vector3 directionOfTarget = (_target.LocalPosition + LocalPosition).Normalized;

            //Created a range for their sight
            float distanceOfTarget = Vector3.Distance(_target.LocalPosition, LocalPosition);

            float dotProduct = Vector3.DotProduct(directionOfTarget, Forward);

            return Math.Acos(dotProduct) < _maxViewAngle && distanceOfTarget < _maxSightDistance;

        }

        /// <summary>
        /// Draws the collider draw
        /// </summary>
        public override void Draw()
        {
            base.Draw();
            Collider.Draw();
        }

        /// <summary>
        /// Checks if the player collides with an enemy
        /// </summary>
        /// <param name="actor"></param>
        /// <param name="currentScene"></param>
        public override void OnCollision(Actor actor, Scene currentScene)
        {

        }
    }
}
