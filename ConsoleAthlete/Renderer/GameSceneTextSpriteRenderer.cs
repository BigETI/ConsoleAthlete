using ConsoleAthlete.Scenes;
using ConsoleGameEngine.Managers;

/// <summary>
/// Console Athlete renderer namespace
/// </summary>
namespace ConsoleAthlete.Renderer
{
    /// <summary>
    /// Game scene text sprite renderer
    /// </summary>
    internal class GameSceneTextSpriteRenderer : TextSpriteRenderer
    {
        /// <summary>
        /// Game scene
        /// </summary>
        public GameScene GameScene { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="gameScene"></param>
        public GameSceneTextSpriteRenderer(GameScene gameScene) : base(gameScene.Window)
        {
            GameScene = gameScene;
        }
    }
}
