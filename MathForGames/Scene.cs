﻿using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;

namespace MathForGames
{
    class Scene
    {
        /// <summary>
        /// Array that contain all actors in the scene
        /// </summary>
        private Actor[] _actors;
        private Actor[] _UIElements;
        private int _enemyCount = 2;
        private bool checkswin;

        /// <summary>
        /// checks the count of enemies
        /// </summary>
        public int EnemyCount
        {
            get { return _enemyCount; }
            set { _enemyCount = value; }
        }

        /// <summary>
        /// The array of actors
        /// </summary>
        public Actor[] Actors
        {
            get { return _actors; }
        }

        /// <summary>
        /// Creates an instance of a actors array and a ui elements array
        /// </summary>
        public Scene()
        {
            _actors = new Actor[0];
            _UIElements = new Actor[0];
        }

        /// <summary>
        /// Calls start for all actors in the actors array
        /// </summary>
        public virtual void Start()
        {
        }

        /// <summary>
        /// Calls update for every actor in the scene. Calls start for the actor if it hasn't been called.
        /// </summary>
        public virtual void Update(float deltaTime, Scene currentScene)
        {
            checkswin = false;

            for (int i = 0; i < _actors.Length; i++)
            {
                if (!_actors[i].Started)
                    _actors[i].Start();

                _actors[i].Update(deltaTime, currentScene);

                //Check for collision
                for (int j = 0; j < _actors.Length; j++)
                {
                    if (i < _actors.Length)
                    {
                        if (_actors[i].CheckForCollision(_actors[j]) && j != i)
                        _actors[i].OnCollision(_actors[j], currentScene);
                    }
                    
                }
            }

            //Checks if their are no more enemies
            if (EnemyCount == 0)
            {
                //Display the win screen
                UIText win = new UIText(400, 10, 50, "WinBox", Color.YELLOW, 250, 150, 50, "You Won!!!");
                currentScene.AddUIElement(win);
            }
        }

        //Updates the Ui in all scenes
        public virtual void UpdateUI(float deltatTime, Scene currentScene)
        {
            for (int i = 0; i < _UIElements.Length; i++)
            {
                if (!_UIElements[i].Started)
                    _UIElements[i].Start();

                _UIElements[i].Update(deltatTime, currentScene);
            }
        }

        //Draws the ui
        public virtual void DrawUI()
        {
            //gets the length of the ui elements
            for (int i = 0; i < _UIElements.Length; i++)
            {
                //calls the actor's draw
                _UIElements[i].Draw();
            }
        }

        //Draws the actors
        public virtual void Draw()
        {
            //gets the actors length
            for (int i = 0; i < _actors.Length; i++)
            {
                //calls the actor's draw
                _actors[i].Draw();
            }
        }

        //Gets the end
        public virtual void End()
        {
            //gets the actors length
            for (int i = 0; i < _actors.Length; i++)
            {
                //calls the end function
                _actors[i].End();
            }
        }

        /// <summary>
        /// Adds an UI to the scenes list of ui elements.
        /// </summary>
        /// <param name="UI">The actor to add to the scene</param>
        public virtual void AddUIElement(Actor UI)
        {
            //Create a new temp arary larger than the current one
            Actor[] tempArray = new Actor[_UIElements.Length + 1];

            //Copy all values from old array into the temp array
            for (int i = 0; i < _UIElements.Length; i++)
            {
                tempArray[i] = _UIElements[i];
            }

            //Add the new actor to the end of the new array
            tempArray[_UIElements.Length] = UI;

            //Set the old array to be the new array
            _UIElements = tempArray;
        }

        /// <summary>
        /// Removes the UI from the scene
        /// </summary>
        /// <param name="UI">The aUI to remove</param>
        /// <returns>False if the UI was not in the scene array</returns>
        public virtual bool RemoveUIElement(Actor UI)
        {
            //Create a variable to store if the removal was successful
            bool actorRemoved = false;

            //Create a new temp arary smaller than the current one
            Actor[] tempArray = new Actor[_UIElements.Length - 1];

            //Copy all values except the actor we dont want into the new array
            int j = 0;
            for (int i = 0; i < tempArray.Length; i++)
            {
                //If the actor that the loop is on is not the temp array counter
                if (_UIElements[i] != UI)
                {
                    //...adds the actor into the new array and increments the tmep array counter
                    tempArray[j] = _UIElements[i];
                    j++;
                }
                //Otherwise if the actor is the one to remove...
                else
                {
                    //...set actorRemove to true
                    actorRemoved = true;
                }
            }

            //If the actorRemove was successful them
            if (actorRemoved)
                //Add the new array to the old array
                _UIElements = tempArray;

            return actorRemoved;
        }

        /// <summary>
        /// Adds an actor the scenes list of actors.
        /// </summary>
        /// <param name="actor">The actor to add to the scene</param>
        public virtual void AddActor(Actor actor)
        {
            //Create a new temp arary larger than the current one
            Actor[] tempArray = new Actor[_actors.Length + 1];

            //Copy all values from old array into the temp array
            for (int i = 0; i < _actors.Length; i++)
            {
                tempArray[i] = _actors[i];
            }

            //Add the new actor to the end of the new array
            tempArray[_actors.Length] = actor;

            //Set the old array to be the new array
            _actors = tempArray;
        }

        /// <summary>
        /// Removes the actor from the scene
        /// </summary>
        /// <param name="actor">The actor to remove</param>
        /// <returns>False if the actor was not in the scene array</returns>
        public virtual bool RemoveActor(Actor actor)
        {
            //Create a variable to store if the removal was successful
            bool actorRemoved = false;

            //Create a new temp arary smaller than the current one
            Actor[] tempArray = new Actor[_actors.Length - 1];

            //Copy all values except the actor we dont want into the new array
            int j = 0;
            for (int i = 0; i < _actors.Length; i++)
            {
                //If the actor that the loop is on is not the temp array counter
                if (_actors[i] != actor)
                {
                    //...adds the actor into the new array and increments the tmep array counter
                    tempArray[j] = _actors[i];
                    j++;
                }
                //Otherwise if the actor is the one to remove...
                else
                {
                    //...set actorRemove to true
                    actorRemoved = true;
                }
            }

            //If the actorRemove was successful them
            if (actorRemoved)
                //Add the new array to the old array
                _actors = tempArray;

            return actorRemoved;
        }
    }
}
