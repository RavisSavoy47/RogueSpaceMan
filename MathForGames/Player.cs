using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary1;
using Raylib_cs;

namespace MathForGames
{
    class Player : Actor
    {
        private float _speed;
        private Vector2 _velocity;
        private float _timer = 0;
        private float _bulletDistance;

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

        public Player(float x, float y, float speed, string name = "Player", string path = "") 
            : base(x, y, name, path)
        {
            _speed = speed;
        }

        /// <summary>
        /// Lets the player move and shoot bullets 
        /// </summary>
        /// <param name="deltaTime">The timer</param>
        /// <param name="currentScene">Gets the currentScene from Scene</param>
        public override void Update(float deltaTime, Scene currentScene)
        {
            //The input for the player
            int xDirection = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_A))
                + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_D));
            int yDirection = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_W))
                + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_S));

            //The input for bullet firing
            int bulletDirectionX = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))
                + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT));
            int bulletDirectionY = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_UP))
                + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_DOWN));

            //Gives the bullets a cooldown timer
            _timer += deltaTime;

            if (bulletDirectionX != 0 && _timer >= .5 || bulletDirectionY != 0 && _timer >= .5)
            {
                Bullet bullet = new Bullet('.', Position.X, Position.Y, bulletDirectionX, bulletDirectionY, 100, Color.RED, "Bullet");
                currentScene.AddActor(bullet);
                CircleCollider bulletCollider = new CircleCollider(10, bullet);
                bullet.Collider = bulletCollider;
                _timer = 0;
            }

            //PLayer movement
            Vector2 moveDirection = new Vector2(xDirection, yDirection);

                Velocity = moveDirection.Normalized * Speed * deltaTime;

                Position += Velocity;

                base.Update(deltaTime, currentScene);
           
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
            if (actor is Enemy)
                Engine.CloseApplication();
        }
    }
}
