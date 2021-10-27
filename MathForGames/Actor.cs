﻿using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary1;
using Raylib_cs;

namespace MathForGames
{
    struct Icon
    {
        public char Symbol;
        public Color Color;
    }

    class Actor
    {
        private Icon _icon;
        private string _name;
        private bool _started;
        private Vector2 _forward = new Vector2(1,0);
        private Collider _collider;
        private Matrix3 _transform = Matrix3.Identity;

        /// <summary>
        /// True if the start fuction has been called for this actor
        /// </summary>
        public bool Started
        {
            get { return _started; }
        }

        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public Icon Icon
        {
            get { return _icon; }
        }

        public Vector2 Forward
        {
            get { return _forward; }
            set { _forward = value; }
        }
        
        public Collider Collider
        {
            get { return _collider; }
            set { _collider = value; }
        }

        public Actor(char icon, float x, float y, Color color, string name = "Actor") :
         this(icon, new Vector2 { X = x, Y = y }, color, name ) {}       

        public Actor(char icon, Vector2 position, Color color, string name = "Actor")
        {
            _icon = new Icon { Symbol = icon, Color = color };
            _position = position;
            _name = name;
        }

        public virtual void Start()
        {
            _started = true;
        }

        public virtual void Update(float deltaTime, Scene currentScene)
        {
            Console.WriteLine(_name + ": " + Position.X + ", " + Position.Y);
            
        }

        public virtual void Draw()
        {
            Raylib.DrawText(Icon.Symbol.ToString(), (int)Position.X - 17, (int)Position.Y - 25, 50, Icon.Color);
        }

        public virtual void End()
        {

        }

        public virtual void OnCollision(Actor actor, Scene currentScene)
        {

        }

        /// <summary>
        /// Checks if this actor collided with another actor
        /// </summary>
        /// <param name="other">Teh actor to check for a collision against</param>
        /// <returns>True if the distance between the actors is less than the radii of the two combined</returns>
        public virtual bool CheckForCollision(Actor other)
        {
            //Return false if either actor doesn't have a collider attached
            if (Collider == null || other.Collider == null)
                return false;

            return Collider.CheckCollision(other);
        }
    }
}
