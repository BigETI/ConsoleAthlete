using ConsoleAthlete.Renderer;
using ConsoleGameEngine;
using ConsoleGameEngine.Components;
using FastConsoleUI;

/// <summary>
/// Console Athlete entities namespace
/// </summary>
namespace ConsoleAthlete.Entities
{
    /// <summary>
    /// Coin entity class
    /// </summary>
    internal class CoinEntity : AEntity
    {
        /// <summary>
        /// Coin sprite
        /// </summary>
        private TextSprite coinSprite;

        /// <summary>
        /// Rectangle collider
        /// </summary>
        private RectCollider rectangleCollider;

        /// <summary>
        /// Coin type
        /// </summary>
        private ECoinType coinType;

        /// <summary>
        /// REctangle transformation
        /// </summary>
        public RectTransform RectangleTransform { get; private set; }

        /// <summary>
        /// Coin type
        /// </summary>
        public ECoinType CoinType
        {
            get => coinType;
            set
            {
                if (coinType != value)
                {
                    coinType = value;
                    UpdateCoinSprite();
                }
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="manager">Manager</param>
        public CoinEntity(IManager manager) : base(manager)
        {
            coinSprite = RequireComponent<TextSprite>();
            RectangleTransform = RequireComponent<RectTransform>();
            rectangleCollider = RequireComponent<RectCollider>();
            RectangleTransform.Size = Vector2Int.one;
            UpdateCoinSprite();
        }

        /// <summary>
        /// Update coin sprite
        /// </summary>
        private void UpdateCoinSprite()
        {
            if (coinSprite != null)
            {
                string text = string.Empty;
                switch (coinType)
                {
                    case ECoinType.One:
                        text = "<Yellow>$";
                        break;
                    case ECoinType.Ten:
                        text = "<Blue>$";
                        break;
                    case ECoinType.Fifty:
                        text = "<Red>$";
                        break;
                }
                coinSprite.Text = text;
            }
        }

        /// <summary>
        /// Update
        /// </summary>
        public override void Update()
        {
            if (rectangleCollider.IsColliding)
            {
                Manager.RemoveEntity(this);
                if (Manager is GameSceneTextSpriteRenderer)
                {
                    GameSceneTextSpriteRenderer game_scene_text_sprite_renderer = (GameSceneTextSpriteRenderer)Manager;
                    game_scene_text_sprite_renderer.GameScene.CollectCoin(coinType);
                }
            }
        }
    }
}
