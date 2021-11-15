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
        private int _enemyCount = 2;

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
            Actor target, Actor friend, string name = "Companion", Shape shape = Shape.CUBE, float health = 0)
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


            //Gets a new target after it's target is dead
            if (_target.Health == 0 && _enemyCount != 0)
            {
                _enemyCount--;
                GetNewTarget(currentScene);
            }
            

            if (GetTargetInSight())
            {
                //Gives the bullets a cooldown timer
                _timer += deltaTime;

                //Looks at its targets position
                LookAt(_target.WorldPosition);

                //Shoots bullets from its forward
                if ((Forward.X != 0 && _timer >= .5 || Forward.Z != 0 && _timer >= .5) && _enemyCount != 0)
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

        //Finds a new enemy after their target is killed
        public void GetNewTarget(Scene currentScene)
        {
            for(int i = 0; i < currentScene.Actors.Length; i++)
            {
                if (currentScene.Actors[i] is Enemy)
                    _target = currentScene.Actors[i];

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
            //If the companion collides with a player
            if (actor is Player)
            {
                //Reverse his velocity
                LocalPosition -= Velocity;
            }
            
            //If the companion collides with a enemy
            if (actor is Enemy)
            {
                //Push the companion back and they lose health
                Velocity *= -10;
                LocalPosition += Velocity;
                Health--;
            }
            //Checks if th ecompanion is dead
            if(Health <= 0)
            {
                //..Removes if health is 0
                currentScene.RemoveActor(this);
            }
        }
    }
}
