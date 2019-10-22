using ConsoleGameEngine;
using FastConsoleUI;
using System;

/// <summary>
/// Console Athlete scenes namespace
/// </summary>
namespace ConsoleAthlete.Scenes
{
    /// <summary>
    /// Credits scene
    /// </summary>
    internal class CreditsScene : AScene
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="gameWindow"></param>
        public CreditsScene(GameWindow gameWindow) : base(gameWindow)
        {
            OnKeyPressed += KeyPressedEvent;
        }

        /// <summary>
        /// Key pressed event
        /// </summary>
        /// <param name="keyInfo"></param>
        private void KeyPressedEvent(ConsoleKeyInfo keyInfo)
        {
            // TODO
            switch (keyInfo.Key)
            {
                case ConsoleKey.Enter:
                case ConsoleKey.Escape:
                    GameWindow.SceneType = ESceneType.MainMenu;
                    break;
            }
        }
    }
}
