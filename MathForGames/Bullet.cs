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

        public Bullet(char icon, float x, float y, float speed, Color color, string name = "Bullet")
            : base(icon, x, y, color, name)
        {
            _speed = speed;
        }

        public override void Update(float deltaTime, Scene currentScene)
        {

            base.Update(deltaTime, currentScene);
        }

        public override void Draw()
        {
            base.Draw();
        }

        public override void OnCollision(Actor actor)
        {
            if (actor is Enemy)
                Engine.CloseApplication();
        }
    }
}
