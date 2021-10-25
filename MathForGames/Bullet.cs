﻿using System;
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
        private Vector2 _startPosition;
        private Actor _owner;

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

        public Bullet(char icon, float x, float y, float velocityX, float velocityY, float speed, Actor owner, Color color, string name = "Bullet")
            : base(icon, x, y, color, name)
        {
            _velocity.X = velocityX;
            _velocity.Y = velocityY;
            _speed = speed;
            _owner = owner;
        }

        public override void Start()
        {
            base.Start();
            _startPosition = _owner.Position;
        }

        public override void Update(float deltaTime, Scene currentScene)
        {
            if (Vector2.Distance(Position, _startPosition) >= 200)
                currentScene.RemoveActor(this);

            Vector2 moveDirection = new Vector2 (_velocity.X, _velocity.Y);

            Velocity = moveDirection.Normalized * Speed * deltaTime;

            Position += Velocity;

            base.Update(deltaTime, currentScene);
        }

        public override void Draw()
        {
            Raylib.DrawText(Icon.Symbol.ToString(), (int)Position.X - 3, (int)Position.Y - 38, 50, Icon.Color);
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
