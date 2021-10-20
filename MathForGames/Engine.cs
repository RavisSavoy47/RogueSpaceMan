using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using MathLibrary1;

namespace MathForGames
{
    class Engine
    {
        private static bool _applicationShouldClose = false;
        private static int _currentSceneIndex;
        private Scene[] _scenes = new Scene[0];
        private static Icon[,] _buffer;


        /// <summary>
        /// Called to begin the application
        /// </summary>
        public void Run()
        {
            //Call start for the entire application
            Start();

            //loop until the application is told to close
            while(!_applicationShouldClose)
            {
                Update();
                Draw();

                Thread.Sleep(50);
            }

            //Call end for the entire application
            End();
        }

        /// <summary>
        /// Called when th eapplication starts 
        /// </summary>
        private void Start()
        {
            Scene scene = new Scene();
            Actor actor = new Actor('P', 0, 0, "Actor1", ConsoleColor.Yellow);
            Actor actor2 = new Actor('A', new MathLibrary1.Vector2 { X = 10, Y = 4 }, "Actor2", ConsoleColor.Blue);
            Player player = new Player('@', 5, 5, 1, "Player", ConsoleColor.DarkMagenta);
            scene.AddActor(player);
            scene.AddActor(actor2);
            scene.AddActor(actor);

            _currentSceneIndex = AddScene(scene);

            _scenes[_currentSceneIndex].Start();

            Console.CursorVisible = false;
        }

        /// <summary>
        /// Called everytime the game loops.
        /// </summary>
        private void Update()
        {
            _scenes[_currentSceneIndex].Update();

            while (Console.KeyAvailable)
                Console.ReadKey(true);
        }

        /// <summary>
        /// Called every time the game loops to update visuals
        /// </summary>
        private void Draw()
        {
            //Clear the stuff that was on the screen in the last frame
            _buffer = new Icon[Console.WindowWidth - 1, Console.WindowHeight - 1];

            //Reset the cursor position to the top so the previous screen is drawn over
            Console.SetCursorPosition(0, 0);

            //Adds all actors icons to buffer
            _scenes[_currentSceneIndex].Draw();

            //Iterate through buffer
            for (int y = 0; y < _buffer.GetLength(1); y++)
            {
                for(int x = 0; x < _buffer.GetLength(0); x++)
                {
                    if (_buffer[x, y].Symbol == '\0')
                        _buffer[x, y].Symbol = ' ';

                    //Set console texxt color to be color of item at buffer
                    Console.ForegroundColor = _buffer[x, y].Color;
                    //Print the symbol of the item in the buffer
                    Console.Write(_buffer[x, y].Symbol);
                }

                //Skip a line once the end of the row has been reached
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Called when the application exits
        /// </summary>
        private void End()
        {
            _scenes[_currentSceneIndex].End();
        }

        /// <summary>
        /// Adds a scene to the engine's scene array 
        /// </summary>
        /// <param name="scene">The scene that will be added to the scene array</param>
        /// <returns>The index where th enew scene is located</returns>
        public int AddScene(Scene scene)
        {
            //Create a new temporary array
            Scene[] tempArray = new Scene[_scenes.Length + 1];

            //Copy all values from old array into the new array
            for (int i = 0; i < _scenes.Length; i++)
            {
                tempArray[i] = _scenes[i];
            }

            //Set the last item to be the new scene
            tempArray[_scenes.Length] = scene;

            //Set the old array to be the new array
            _scenes = tempArray;

            //Return the last index
            return _scenes.Length -1;
        }

        /// <summary>
        /// Gets the key in the input stream
        /// </summary>
        /// <returns>teh key that was pressed</returns>
        public static ConsoleKey GetNextKey()
        {
            //If there is no key being pressed
            if (!Console.KeyAvailable)
                //..return
                return 0;

            //return teh current key being pressed
            return Console.ReadKey(true).Key;
        }

        /// <summary>
        /// Adds the icon to the buffer to print to the screen in the next draw call.
        /// Prints the icon at the given position in th ebuffer.
        /// </summary>
        /// <param name="icon">The icon to draw</param>
        /// <param name="positison">The position of the icon in the buffer</param>
        /// <returns>Falseif teh position is outside the bounds of the buffer</returns>returns>
        public static bool Render(Icon icon, Vector2 positison)
        {
            //If the position is out of bounds..
            if (positison.X < 0 || positison.X >= _buffer.GetLength(0) || positison.Y < 0 || positison.Y >= _buffer.GetLength(1))
                //..return false
                return false;

            //Set th ebuffer at the index of th egiven position to be teh icon
            _buffer[(int)positison.X, (int)positison.Y] = icon;
            return true;
        }

        public static void CloseApplication()
        {
            _applicationShouldClose = true;
        }
    }
}
