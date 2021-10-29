﻿using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary1;
using Raylib_cs;

namespace MathForGames
{
    class Enemy : Actor
    {
        private float _speed;
        private Vector2 _velocity;
        public Actor _target;
        private float _maxSightDistance;
        private float _maxViewAngle;

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

        public Enemy(float x, float y, float speed, float maxSightDistance, float maxViewAngle, 
            Actor target, string name = "Enemy", string path = "")
            : base(x, y, name, path)
        {
            _target = target;
            _speed = speed;
            _maxSightDistance = maxSightDistance;
            _maxViewAngle = maxViewAngle;
        }

        /// <summary>
        /// Lets the enemy move 
        /// </summary>
        /// <param name="deltaTime"></param>
        /// <param name="currentScene"></param>
        public override void Update(float deltaTime, Scene currentScene)
        {
            //Create a vector that stores the move input
            Vector2 moveDirection = (_target.Position - Position).Normalized;

            Velocity = moveDirection * Speed * deltaTime;

            //Once target it in sight enemy moves
            if (GetTargetInSight())
                base.Translate(Velocity.X, Velocity.Y);

            

            base.Update(deltaTime, currentScene);
        }

        /// <summary>
        /// Calls the collider draw
        /// </summary>
        public override void Draw()
        {
            base.Draw();
            Collider.Draw();
        }

        /// <summary>
        /// Lets the enemy see their target
        /// </summary>
        /// <returns>True if there is a target in sight</returns>
        public bool GetTargetInSight()
        {
            Vector2 directionOfTarget = (_target.Position + Position).Normalized;

            //Created a range for their sight
            float distanceOfTarget = Vector2.Distance(_target.Position, Position);

            float dotProduct = Vector2.DotProduct(directionOfTarget, Forward);

            return Math.Acos(dotProduct) < _maxViewAngle &&  distanceOfTarget < _maxSightDistance;

        }

        public override void OnCollision(Actor actor, Scene currentScene)
        {
            Console.WriteLine("Collision Occurred");
        }
    }
}
