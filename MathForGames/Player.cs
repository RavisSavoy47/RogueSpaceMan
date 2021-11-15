using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{
    class Player : Actor
    {
        private float _speed;
        private Vector3 _velocity;
        private float _timer = 0;
        private float _health;
        private bool spawned;

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

        public Player(float x, float y, float z, float speed, string name = "Player", Shape shape = Shape.CUBE, float health = 0) 
            : base(x, y, z, name, shape, health)
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
            //Checks if th eplayer is dead
            if (Health <= 0)
            {
                currentScene.RemoveActor(this);
                Engine.CloseApplication();
            }

            //The input for the player
            int xDirection = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_A))
                + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_D));
            int zDirection = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_W))
                + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_S));
            int rotationbullets = Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_SPACE));

            //The input for bullet firing
            int bulletDirectionX = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))
                + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT));
            int bulletDirectionZ = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_UP))
                + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_DOWN));

                //Gives the bullets a cooldown timer
                _timer += deltaTime;

            //Creates a bullet and it changes direction by the bullet direction's x and z
            if (bulletDirectionX != 0 && _timer >= .5 || bulletDirectionZ != 0 && _timer >= .5 )
            {
                Bullet bullet = new Bullet(LocalPosition.X, LocalPosition.Y, LocalPosition.Z, bulletDirectionX, bulletDirectionZ, 10, "Bullet", Shape.SPHERE);
                bullet.SetScale(.15f, .15f, .15f);
                bullet.SetColor(new Vector4(16, 23, 19, 255));
                currentScene.AddActor(bullet);

                SphereCollider bulletCollider = new SphereCollider(.5f, bullet);
                bullet.Collider = bulletCollider;

                _timer = 0;
            }


            //Rotates the player so the rotating bullet rotates 
            Rotate(0, 5 * deltaTime, 0);

            //Creates a bullet to rotate around the player
            if (rotationbullets != 0 && _timer >= .5)
            {
                RotatingBullets rbullet = new RotatingBullets(1, 0, 1, 1, 1, 10, "Bullet", Shape.SPHERE);
                rbullet.SetScale(.15f, .15f, .15f);
                rbullet.SetColor(new Vector4(16, 23, 19, 255));
                currentScene.AddActor(rbullet);

                SphereCollider rbulletCollider = new SphereCollider(.5f, rbullet);
                rbullet.Collider = rbulletCollider;

                AddChild(rbullet);

                _timer = 0;
            }

            //PLayer movement
            Vector3 moveDirection = new Vector3(xDirection, 0, zDirection);

            Velocity = moveDirection.Normalized * Speed * deltaTime;

            if (Velocity.Magnitude > 0)
                Forward = Velocity.Normalized;

            LocalPosition += Velocity;

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
            //If the player collides with an enemy
            if (actor is Enemy)
            {
                //pushes the player back and subtracts health
                Velocity *= -10;
                LocalPosition += Velocity;
                Health--;

                //Checks if their is a collision
                Console.WriteLine(actor + "Collision!");
            }
        }
    }
}
