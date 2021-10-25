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
        private Vector2 _velocity;

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

        public Player(char icon, float x, float y, float speed, Color color, string name = "Player") 
            : base(icon, x, y, color, name)
        {
            _speed = speed;
        }

        public override void Update(float deltaTime, Scene currentScene)
        {
            int xDirection = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_A))
                + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_D));
            int yDirection = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_W))
                + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_S));

            int bulletDirectionX = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))
                + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT));
            int bulletDirectionY = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_UP))
                + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_DOWN));


            if (bulletDirectionX != 0 || bulletDirectionY != 0)
            {
                Bullet bullet = new Bullet('.', Position.X, Position.Y, bulletDirectionX, bulletDirectionY, 100, this, Color.RED, "Bullet");
                currentScene.AddActor(bullet);
                CircleCollider bulletCollider = new CircleCollider(10, bullet);
                bullet.Collider = bulletCollider;
            }


                //PLayer movement
                Vector2 moveDirection = new Vector2(xDirection, yDirection);

                Velocity = moveDirection.Normalized * Speed * deltaTime;

                Position += Velocity;

                base.Update(deltaTime, currentScene);
           
        }
        public override void Draw()
        {
            Raylib.DrawText(Icon.Symbol.ToString(), (int)Position.X - 17, (int)Position.Y - 28, 50, Icon.Color);
            Raylib.DrawCircleLines((int)Position.X, (int)Position.Y, 25, Color.BLACK);
        }

        public override void OnCollision(Actor actor, Scene currentScene)
        {
            if (actor is Enemy)
                Engine.CloseApplication();
        }
    }
}
