using ConsoleGameEngine;
using FastConsoleUI;
using System;

/// <summary>
/// Console Athlete namespace
/// </summary>
namespace ConsoleAthlete
{
    /// <summary>
    /// Scene abstract class
    /// </summary>
    internal abstract class AScene : AManager, IScene
    {
        /// <summary>
        /// Game window
        /// </summary>
        public GameWindow GameWindow { get; private set; }

        /// <summary>
        /// Window
        /// </summary>
        public Window Window => GameWindow.Window;

        /// <summary>
        /// On key pressed event
        /// </summary>
        public event KeyPressedDelegate OnKeyPressed;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="gameWindow">Game window</param>
        public AScene(GameWindow gameWindow)
        {
            GameWindow = gameWindow;
        }

        /// <summary>
        /// Send key
        /// </summary>
        /// <param name="keyInfo">Key information</param>
        public void SendKey(ConsoleKeyInfo keyInfo)
        {
            OnKeyPressed?.Invoke(keyInfo);
        }
    }
}
