using System;

/// <summary>
/// Console Athlete scenes namespace
/// </summary>
namespace ConsoleAthlete.Scenes
{
    /// <summary>
    /// Introduction scene class
    /// </summary>
    internal class IntroScene : AScene
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="gameWindow">Game window</param>
        public IntroScene(GameWindow gameWindow) : base(gameWindow)
        {
            OnKeyPressed += KeyPressedEvent;
        }

        /// <summary>
        /// Key pressed event
        /// </summary>
        /// <param name="keyInfo">Key information</param>
        private void KeyPressedEvent(ConsoleKeyInfo keyInfo)
        {
            GameWindow.SceneType = ESceneType.MainMenu;
        }
    }
}
