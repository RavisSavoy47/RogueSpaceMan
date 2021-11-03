﻿using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary1;
using Raylib_cs;

namespace MathForGames
{
    class Player : Actor
    {
        private float _speed;
        private Vector3 _velocity;
        private float _timer = 0;
        private float _bulletDistance;

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

        public Player(float x, float y, float speed, string name = "Player", Shape shape = Shape.CUBE) 
            : base(x, y, name, shape)
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
            int zDirection = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_W))
                + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_S));

            //The input for bullet firing
            int bulletDirectionX = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))
                + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT));
            int bulletDirectionY = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_UP))
                + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_DOWN));

            //Gives the bullets a cooldown timer
            _timer += deltaTime;

            if (bulletDirectionX != 0 && _timer >= .5 || bulletDirectionY != 0 && _timer >= .5 )
            {
                Bullet bullet = new Bullet(LocalPosition.X, LocalPosition.Y, bulletDirectionX, bulletDirectionY, 100, "Bullet");
                bullet.SetScale(1, 1, 1);
 
                CircleCollider bulletCollider = new CircleCollider(10, bullet);
                bullet.Collider = bulletCollider;
                currentScene.AddActor(bullet);                
                
                _timer = 0;
            }

            //PLayer movement
            Vector3 moveDirection = new Vector3(xDirection, 0, zDirection);

            Velocity = moveDirection.Normalized * Speed * deltaTime;

            //if(Velocity.Magnitude > 0)
            //Forward = Velocity.Normalized;

            LocalPosition += Velocity;

            base.Update(deltaTime, currentScene);
           
        }

        /// <summary>
        /// Draws the collider draw
        /// </summary>
        public override void Draw()
        {
            base.Draw();
            //Collider.Draw();
        }

        /// <summary>
        /// Checks if the player collides with an enemy
        /// </summary>
        /// <param name="actor"></param>
        /// <param name="currentScene"></param>
        public override void OnCollision(Actor actor, Scene currentScene)
        {
            if (actor is Enemy)
            {
                UIText DeathMessage = new UIText(500, 100, "DeathMessage", Color.BLACK, 70, 70, 15, "You Died!!!");
                currentScene.AddUIElement(DeathMessage);
                currentScene.RemoveActor(this);
            }
        }
    }
}
