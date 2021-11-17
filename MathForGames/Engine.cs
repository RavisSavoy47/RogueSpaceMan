using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Diagnostics;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{
    class Engine
    {
        private static bool _applicationShouldClose = false;
        private static int _currentSceneIndex;
        private Scene[] _scenes = new Scene[0];
        private Stopwatch _stopwatch = new Stopwatch();
        private Camera3D _camera = new Camera3D();
        private Scene theScene;
        private Player _player;
        private Enemy _enemy1;
        private Enemy _enemy2;
        private Companion _compan;
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

        private void InitializeCamera()
        {
            // Camera position
            _camera.position = new System.Numerics.Vector3(0, 10, 10);
            // Point the camera is focused on
            _camera.target = new System.Numerics.Vector3(0, 0, 0);
            // Camera up vector (rotation towards target)
            _camera.up = new System.Numerics.Vector3(0, 1, 0);
            // Camera feild of view
            _camera.fovy = 45;
            // Camera mode type
            _camera.projection = CameraProjection.CAMERA_PERSPECTIVE; 
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

            InitializeCamera();

            InitializeActors();

            _scenes[_currentSceneIndex].Start();
            Console.CursorVisible = false;
        }

        public void InitializeActors()
        {
            Scene scene = new Scene();

            theScene = scene;

            Player player = new Player(1, 3, 1, 12, "player", Shape.CUBE, 5);
            player.SetScale(1, 1, 1);
            //max color value 255
            //last color slot is transprancy
            player.SetColor(new Vector4(86, 98, 3, 255));
            scene.AddActor(player);

            //Lets the camera and the ui text get information about the player
            _player = player;

            player.Collider = new AABBCollider(2, 2, 2, player);

            //The body for the player
            Actor actor1 = new Actor(0, 1, 0, "Gun", Shape.CUBE);
            actor1.SetScale(.5f, .5f, .5f);
            actor1.SetColor(new Vector4(86, 98, 3, 255));
            scene.AddActor(actor1);
            player.AddChild(actor1);

            Actor actor2 = new Actor(0, -1, 0, "body", Shape.CUBE);
            actor2.SetScale(1.5f, 0, 1.5f);
            actor2.SetColor(new Vector4(35, 98, 3, 255));
            scene.AddActor(actor2);
            player.AddChild(actor2);


            Enemy enemy1 = new Enemy(10, 1, 10, 5, 10, 100, player, "Enemy", Shape.SPHERE, 5);
            enemy1.SetScale(1, 1, 1);
            scene.AddActor(enemy1);
            enemy1.SetColor(new Vector4(26, 78, 6, 255));

            enemy1.Collider = new SphereCollider(1, enemy1);
            //Giving the ui text the enemy's health
            _enemy1 = enemy1;

            Enemy enemy2 = new Enemy(20, 1, 5, 10, 10, 100, player, "Enemy", Shape.SPHERE, 5);
            enemy2.SetScale(1, 1, 1);
            scene.AddActor(enemy2);
            enemy2.SetColor(new Vector4(26, 78, 6, 255));

            enemy2.Collider = new SphereCollider(1, enemy2);
            //Giving the ui text the enemy's health
            _enemy2 = enemy2;


            //Follows the player and shoot the enemy if the enemy is in sight
            Companion tinyMan = new Companion(1, 1, 4, 10, 11, 400, enemy1, player, "Companion", Shape.CUBE, 5);
            tinyMan.SetScale(1, 1, 1);
            tinyMan.SetColor(new Vector4(200, 10, 25, 255));
            scene.AddActor(tinyMan);

            tinyMan.Collider = new AABBCollider(1, 1, 1, tinyMan);

            //The body for the companion
            Actor actorC1 = new Actor(0, 1, 0, "Gun", Shape.CUBE);
            actorC1.SetScale(.5f, .5f, .5f);
            actorC1.SetColor(new Vector4(200, 120, 45, 255));
            scene.AddActor(actorC1);
            tinyMan.AddChild(actorC1);

            Actor actorC2 = new Actor(0, -1, 0, "body", Shape.CUBE);
            actorC2.SetScale(1.5f, 0, 1.5f);
            actorC2.SetColor(new Vector4(200, 130, 25, 255));
            scene.AddActor(actorC2);
            tinyMan.AddChild(actorC2);
            //Giving the ui text the companion's health
            _compan = tinyMan;

            _currentSceneIndex = AddScene(scene);
        }

        /// <summary>
        /// Called everytime the game loops.
        /// </summary>
        private void Update(float deltaTime)
        {
            //Updates the player and enemies health and displays it
            UIText actorsHP = new UIText(1, 1, 1, "ActorHPBox", Color.BLACK, 150, 100, 15, "Player's Health " + _player.Health + "\nCompanion's Health " + _compan.Health
                + "\nEnemy's Health " + _enemy1.Health + "\nEnemy2's Health " + _enemy2.Health);

            theScene.AddUIElement(actorsHP);

            //Displays The Controls
            UIText controls = new UIText(550, 1, 1, "ControlBox", Color.BLACK, 250, 100, 15, "Use WASD to Move the player." + "\nPress the arrow keys to shoot bullets. " 
                + "\nPress Space to shoot rotating Bullets ");

            theScene.AddUIElement(controls);

            // Camera position on the player position
            _camera.position = new System.Numerics.Vector3(_player.WorldPosition.X, _player.WorldPosition.Y + 15, _player.WorldPosition.Z + 15);
            // Point the camera is focused on the player position
            _camera.target = new System.Numerics.Vector3(_player.WorldPosition.X, _player.WorldPosition.Y, _player.WorldPosition.Z);

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
            Raylib.BeginMode3D(_camera);
            Raylib.DrawGrid(50, 1);
            Raylib.ClearBackground(Color.RAYWHITE);
            //Adds all actors icons to buffer
            _scenes[_currentSceneIndex].Draw();

            Raylib.EndMode3D();
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
        /// Ends the application
        /// </summary>
        public static void CloseApplication()
        {
            _applicationShouldClose = true;
        }
    }
}
