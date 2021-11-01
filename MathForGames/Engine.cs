using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Diagnostics;
using MathLibrary1;
using Raylib_cs;

namespace MathForGames
{
    class Engine
    {
        private static bool _applicationShouldClose = false;
        private static int _currentSceneIndex;
        private Scene[] _scenes = new Scene[0];
        private Stopwatch _stopwatch = new Stopwatch();

        /// <summary>
        /// Called to begin the application
        /// </summary>
        public void Run()
        {
            //Call start for the entire application
            Start();

            float currentTime = 0;
            float lastTime = 0;
            float deltaTime = 0;

            //loop until the application is told to close
            while (!_applicationShouldClose && !Raylib.WindowShouldClose())
            {
                //Get how much time has passed since the application started 
                currentTime = _stopwatch.ElapsedMilliseconds / 1000.0f;

                //Set data time to be the different in time from the last time recorded to the current time
                deltaTime = currentTime - lastTime;

                //Update the application
                Update(deltaTime);
                //Draw all items
                Draw();

                //Set the last time recorded to be the current time
                lastTime = currentTime;
            }

            //Call end for the entire application
            End();
        }

        /// <summary>
        /// Called when the application starts 
        /// </summary>
        private void Start()
        {
            _stopwatch.Start();

            //Create a window using raylib
            Raylib.InitWindow(800, 450, "Math For Games");
            Raylib.SetTargetFPS(60);

            Scene scene = new Scene();   
            
            Player player = new Player(380, 400, 200, "Player", "Images/player.png");
            player.SetScale(50, 50);
            player.SetRotation(1);
            AABBCollider playerCollider = new AABBCollider(50, 50, player);
            player.Collider = playerCollider;

            Enemy enemy1 = new Enemy(300, 30, 200, 350, 2, player, "Enemy", "Images/enemy.png");
            enemy1.SetScale(50, 50);
            enemy1.LookAt(new Vector2(20000, 9000));
            AABBCollider enemy1Collider = new AABBCollider(50, 50, enemy1);
            enemy1.Collider = enemy1Collider;

            Enemy enemy2 = new Enemy(350, 30, 100, 500, 2, player, "Enemy", "Images/enemy.png");
            enemy2.SetScale(50, 50);
            AABBCollider enemy2Collider = new AABBCollider(50, 50, enemy2);
            enemy2.Collider = enemy2Collider;

            Enemy enemy3 = new Enemy(400, 30, 100, 500, 2, player, "Enemy", "Images/enemy.png");
            enemy3.SetScale(50, 50);
            AABBCollider enemy3Collider = new AABBCollider(50, 50, enemy3);
            enemy3.Collider = enemy3Collider;

            Enemy enemy4 = new Enemy(450, 30, 200, 350, 2, player, "Enemy", "Images/enemy.png");
            enemy4.SetScale(50, 50);
            AABBCollider enemy4Collider = new AABBCollider(50, 50, enemy4);
            enemy4.Collider = enemy4Collider;

            scene.AddActor(player);
            scene.AddActor(enemy1);
            scene.AddActor(enemy2);
            scene.AddActor(enemy3);
            scene.AddActor(enemy4);

            _currentSceneIndex = AddScene(scene);
            _scenes[_currentSceneIndex].Start();
            Console.CursorVisible = false;
        }

        /// <summary>
        /// Called everytime the game loops.
        /// </summary>
        private void Update(float deltaTime)
        {
            _scenes[_currentSceneIndex].Update(deltaTime, _scenes[_currentSceneIndex]);
            _scenes[_currentSceneIndex].UpdateUI(deltaTime, _scenes[_currentSceneIndex]);

            while (Console.KeyAvailable)
                Console.ReadKey(true);
        }

        /// <summary>
        /// Called every time the game loops to update visuals
        /// </summary>
        private void Draw()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.SKYBLUE);
            //Adds all actors icons to buffer
            _scenes[_currentSceneIndex].Draw();
            _scenes[_currentSceneIndex].DrawUI();

            Raylib.EndDrawing();
        }

        /// <summary>
        /// Called when the application exits
        /// </summary>
        private void End()
        {
            _scenes[_currentSceneIndex].End();
            Raylib.CloseWindow();
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
        /// Ends teh application
        /// </summary>
        public static void CloseApplication()
        {
            _applicationShouldClose = true;
        }
    }
}
