using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary1;
using Raylib_cs;

namespace MathForGames
{
    class Bullet : Actor
    {
        private float _speed;
        private Vector2 _velocity;
        private Vector2 _bulletPosition;

        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public Vector2 Velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }

        public Bullet(float x, float y, float velocityX, float velocityY, float speed, string name = "Bullet", string path = "")
            : base(x, y, name, path)
        {
            _velocity.X = velocityX;
            _velocity.Y = velocityY;
            _speed = speed;
        }

        /// <summary>
        /// The movement for each bullet and their range
        /// </summary>
        /// <param name="deltaTime"></param>
        /// <param name="currentScene"></param>
        public override void Update(float deltaTime, Scene currentScene)
        {
            
            Vector2 moveDirection = new Vector2 (_velocity.X, _velocity.Y);

            Velocity = moveDirection.Normalized * Speed * deltaTime;

            base.Translate(Velocity.X, Velocity.Y);

            base.Update(deltaTime, currentScene);

            //The range of bullets
            //if (Position.X - _bulletPosition.X > 150 || Position.Y - _bulletPosition.Y > 150 ||
            //    Position.X - _bulletPosition.X < -150 || Position.Y - _bulletPosition.Y < -150)
            //    currentScene.RemoveActor(this);
        }

        public override void Draw()
        {
            base.Draw();
            Collider.Draw();
            Raylib.DrawCircleLines((int)Position.X, (int)Position.Y, 15, Color.BLACK);
        }

        public override void OnCollision(Actor actor, Scene currentScene)
        {
            if (actor is Enemy)
            {
                currentScene.RemoveActor(actor);
                currentScene.EnemyCount--;
            }
        }
    }
}
