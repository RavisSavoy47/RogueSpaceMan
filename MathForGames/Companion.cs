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
        public Actor _friend;
        private float _maxSightDistance;
        private float _maxViewAngle;
        private static float _health = 0;

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

        public Companion(float x, float y, float z, float speed, float maxSightDistance, float maxViewAngle, 
            Actor target, Actor friend, string name = "Companion", Shape shape = Shape.CUBE, float health = 5)
            : base(x, y, z, name, shape, health)
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
            //Follows the player
            Vector3 moveDirection = (_friend.LocalPosition - LocalPosition).Normalized;

            Velocity = moveDirection * Speed * deltaTime;

            LocalPosition += Velocity;

            //Looks at the player
            LookAt(_friend.WorldPosition);

            if (GetTargetInSight())
            {
                //Gives the bullets a cooldown timer
                _timer += deltaTime;

                LookAt(_target.WorldPosition);

                if (Forward.X != 0 && _timer >= .5 || Forward.Z != 0 && _timer >= .5)
                {

                    Bullet bullet = new Bullet(WorldPosition.X, WorldPosition.Y, WorldPosition.Z, Forward.X, Forward.Z, 10, "Bullet", Shape.SPHERE);
                    bullet.SetScale(.15f, .15f, .15f);
                    bullet.SetColor(new Vector4(16, 23, 19, 255));
                    currentScene.AddActor(bullet);

                    SphereCollider bulletCollider = new SphereCollider(.5f, bullet);
                    bullet.Collider = bulletCollider;


                    _timer = 0;
                }
            }

            if (_target.Health == 0)
                GetNewTarget(currentScene);
                

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


        public void GetNewTarget(Scene currentScene)
        {
            for(int i = 0; i < currentScene.Actors.Length; i++)
            {
                if (currentScene.Actors[i] is Enemy)
                    _target = currentScene.Actors[i];
                return;
            }
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
        /// <param name="actor"></param>a
        /// <param name="currentScene"></param>
        public override void OnCollision(Actor actor, Scene currentScene)
        {
            if (actor is Actor friend)
            {
                LocalPosition -= Velocity;
            }
            else if (actor is Actor target)
            {
                Velocity *= -10;
                LocalPosition += Velocity;
                Health--;
            }
            if(Health <= 0)
            {
                currentScene.RemoveActor(this);
            }
        }
    }
}
