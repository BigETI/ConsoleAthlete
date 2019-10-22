using System;

/// <summary>
/// Console Athlete scenes
/// </summary>
namespace ConsoleAthlete.Scenes
{
    /// <summary>
    /// Main menu scene class
    /// </summary>
    internal class MainMenuScene : AScene
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="gameWindow">Game window</param>
        public MainMenuScene(GameWindow gameWindow) : base(gameWindow)
        {
            OnKeyPressed += KeyPressedEvent;
        }

        /// <summary>
        /// Key pressed event
        /// </summary>
        /// <param name="keyInfo">Key information</param>
        private void KeyPressedEvent(ConsoleKeyInfo keyInfo)
        {
            // TODO
            throw new NotImplementedException();
        }
    }
}
