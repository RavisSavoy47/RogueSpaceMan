using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{
    class Bullet : Actor
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

        public Bullet(float x, float y, float z, float velocityX, float velocityZ, float speed, string name = "Bullet", Shape shape = Shape.CUBE)
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
            //Moves by the velocity's x, y, z
            Vector3 moveDirection = new Vector3 (_velocity.X, _velocity.Y, _velocity.Z);

            Velocity = moveDirection.Normalized * Speed * deltaTime;

            if (Velocity.Magnitude > 0)
                Forward = Velocity.Normalized;

            LocalPosition += Velocity;

            base.Update(deltaTime, currentScene);

            _timer += deltaTime;

            //Removes the bullet after a set time
            if (_timer >= 3)
                currentScene.RemoveActor(this);
        }

        //Calls the base draw and the collider draw
        public override void Draw()
        {
            base.Draw();
            Collider.Draw();
        }

        //Checks if they have coillided woth another actor
        public override void OnCollision(Actor actor, Scene currentScene)
        {
            //if a bullet collides with an enemy
            if (actor is Enemy)
            {
                actor.Health--;
                currentScene.RemoveActor(this);
            }

        }
    }
}
