using System;

/// <summary>
/// Console Athlete scenes namespace
/// </summary>
namespace ConsoleAthlete.Scenes
{
    /// <summary>
    /// Options scene
    /// </summary>
    internal class OptionsScene : AScene
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="gameWindow">Game window</param>
        public OptionsScene(GameWindow gameWindow) : base(gameWindow)
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
