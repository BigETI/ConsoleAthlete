using ConsoleAthlete.Scenes;
using FastConsoleUI;
using System;

/// <summary>
/// Console Athlete namespace
/// </summary>
namespace ConsoleAthlete
{
    /// <summary>
    /// Game window class
    /// </summary>
    internal class GameWindow
    {
        /// <summary>
        /// Scenes
        /// </summary>
        private IScene[] scenes = new IScene[Enum.GetValues(typeof(ESceneType)).Length];

        /// <summary>
        /// Is game running
        /// </summary>
        private bool isGameRunning;

        /// <summary>
        /// Window
        /// </summary>
        public Window Window { get; private set; }

        /// <summary>
        /// Scene type
        /// </summary>
        public ESceneType SceneType { get; set; } = ESceneType.Game;

        /// <summary>
        /// Current scene
        /// </summary>
        public IScene CurrentScene => (((int)SceneType < scenes.Length) ? scenes[(int)SceneType] : null);

        /// <summary>
        /// On key pressed
        /// </summary>
        public event KeyPressedDelegate OnKeyPressed;

        /// <summary>
        /// Default constructor
        /// </summary>
        public GameWindow()
        {
            Window = new Window();
            scenes[(int)(ESceneType.Intro)] = new IntroScene(this);
            scenes[(int)(ESceneType.MainMenu)] = new MainMenuScene(this);
            scenes[(int)(ESceneType.Options)] = new OptionsScene(this);
            scenes[(int)(ESceneType.Credits)] = new CreditsScene(this);
            scenes[(int)(ESceneType.Game)] = new GameScene(this);
            Window.OnKeyPressed += KeyPressedEvent;
            Window.OnWindowResized += WindowResizedEvent;
        }

        /// <summary>
        /// Window resized event
        /// </summary>
        /// <param name="size">Size</param>
        private void WindowResizedEvent(Vector2Int size)
        {
            // ...
        }

        /// <summary>
        /// Key pressed event
        /// </summary>
        /// <param name="keyInfo">Key information</param>
        private void KeyPressedEvent(ConsoleKeyInfo keyInfo)
        {
            OnKeyPressed?.Invoke(keyInfo);
            CurrentScene?.SendKey(keyInfo);
        }

        /// <summary>
        /// Run game
        /// </summary>
        public void Run()
        {
            isGameRunning = true;
            foreach (IScene scene in scenes)
            {
                scene.Init();
            }
            while (isGameRunning)
            {
                Console.CursorVisible = false;
                Window.Refresh();
                CurrentScene?.Update();
                Update();
            }
            Console.CursorVisible = true;
        }

        /// <summary>
        /// Applies game rules, calculates enemy movement
        /// </summary>
        private void Update()
        {
            CurrentScene?.Update();
        }
    }
}
