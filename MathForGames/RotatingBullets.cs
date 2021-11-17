using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{
    class RotatingBullets : Actor
    {
        private float _speed;
        private Vector3 _velocity;
        private float _timer = 0;

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

        public RotatingBullets(float x, float y, float z, float velocityX, float velocityZ, float speed, string name = "Bullet", Shape shape = Shape.CUBE)
            : base(x, y, z, name, shape)
        {
            _velocity.X = velocityX;
            _velocity.Z = velocityZ;
            _speed = speed;
        }

        /// <summary>
        /// The movement for each bullet and their range
        /// </summary>
        /// <param name="deltaTime"></param>
        /// <param name="currentScene"></param>
        public override void Update(float deltaTime, Scene currentScene)
        {
            Vector3 moveDirection = new Vector3(_velocity.X, 0, _velocity.Z);

            Velocity = moveDirection.Normalized * Speed * deltaTime;

            if (Velocity.Magnitude > 0)
                Forward = Velocity.Normalized;

            LocalPosition += Velocity;

            //Rotate by 10 times deltatime
            Rotate(10 * deltaTime, 0, 10 * deltaTime);
            UpdateTransforms();

            base.Update(deltaTime, currentScene);

            _timer += deltaTime;

            //Removes the bullet after a set time
            if (_timer >= 2.5f)
                currentScene.RemoveActor(this);
        }

        //Draws their collider
        public override void Draw()
        {
            base.Draw();
            Collider.Draw();
        }

        //Checks if they have coillided woth another actor
        public override void OnCollision(Actor actor, Scene currentScene)
        {
            //If the rotating bullet collides with a enemy
            if (actor is Enemy)
            {
                //subtract health from the enemy
                actor.Health--;
                //remove the bullet after the collision
                currentScene.RemoveActor(this);
            }

        }
    }
}
