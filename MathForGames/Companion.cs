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
        public Actor _target;
        public Player _friend;
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

        public Companion(float x, float y, float z, float speed, float maxSightDistance, float maxViewAngle, Actor target, Player friend, string name = "Companion", Shape shape = Shape.CUBE)
            : base(x, y, z, name, shape)
        {
            _target = target;
            _friend = friend;
            _speed = speed;
            _maxSightDistance = maxSightDistance;
            _maxViewAngle = maxViewAngle;
        }

        /// <summary>
        /// Lets the player move and shoot bullets 
        /// </summary>
        /// <param name="deltaTime">The timer</param>
        /// <param name="currentScene">Gets the currentScene from Scene</param>
        public override void Update(float deltaTime, Scene currentScene)
        {
            Vector3 moveDirection = (_friend.LocalPosition - LocalPosition).Normalized;

            Velocity = moveDirection * Speed * deltaTime;

            LocalPosition += Velocity;

            if (GetTargetInSight())
            {
                //Gives the bullets a cooldown timer
                _timer += deltaTime;

                if (_target.WorldPosition.X != 0 && _timer >= .5 || _target.WorldPosition.Z != 0 && _timer >= .5)
                {
                    Bullet bullet = new Bullet(LocalPosition.X, LocalPosition.Y, LocalPosition.Z, _target.WorldPosition.X, _target.WorldPosition.Z, 10, "Bullet");
                    bullet.SetScale(1, 1, 1);
                    currentScene.AddActor(bullet);

                    SphereCollider bulletCollider = new SphereCollider(1, bullet);
                    bullet.Collider = bulletCollider;


                    _timer = 0;
                }

                //Sets its forward to the targets position
                LookAt(_target.WorldPosition);
            }

            base.Update(deltaTime, currentScene);

        }

        /// <summary>
        /// Finds the target that is in its range
        /// </summary>
        /// <returns></returns>
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
