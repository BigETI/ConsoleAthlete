using ConsoleAthlete.Renderer;
using ConsoleGameEngine;
using ConsoleGameEngine.Components;
using FastConsoleUI;
using System.Text;

/// <summary>
/// Console Athlete entities namespace
/// </summary>
namespace ConsoleAthlete.Entities
{
    /// <summary>
    /// Barrier entity class
    /// </summary>
    internal class BarrierEntity : AEntity
    {
        /// <summary>
        /// Barrier text sprite
        /// </summary>
        private TextSprite barrierTextSprite;

        /// <summary>
        /// Rectangle collider
        /// </summary>
        private RectCollider rectangleCollider;

        /// <summary>
        /// Barrier height
        /// </summary>
        private uint barrierHeight;

        /// <summary>
        /// Rectangle transformation
        /// </summary>
        public RectTransform RectangleTransform { get; private set; }

        /// <summary>
        /// Barrier height
        /// </summary>
        public uint BarrierHeight
        {
            get => barrierHeight;
            set
            {
                if (barrierHeight != value)
                {
                    barrierHeight = value;
                    UpdateBarrierSprite();
                }
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="manager">Manager</param>
        public BarrierEntity(IManager manager) : base(manager)
        {
            barrierTextSprite = RequireComponent<TextSprite>();
            rectangleCollider = RequireComponent<RectCollider>();
            RectangleTransform = RequireComponent<RectTransform>();
            UpdateBarrierSprite();
        }

        /// <summary>
        /// Update barrier sprite
        /// </summary>
        private void UpdateBarrierSprite()
        {
            if ((RectangleTransform != null) && (barrierTextSprite != null))
            {
                RectangleTransform.Size = new Vector2Int(1, (int)barrierHeight);
                RectangleTransform.Y = 3 - RectangleTransform.Height;
                StringBuilder barrier_string_builder = new StringBuilder();
                for (int i = 0; i < barrierHeight; i++)
                {
                    if (i > 0)
                    {
                        barrier_string_builder.Append('\n');
                    }
                    barrier_string_builder.Append("X");
                }
                barrierTextSprite.Text = barrier_string_builder.ToString();
                barrier_string_builder.Clear();
            }
        }

        /// <summary>
        /// Update
        /// </summary>
        public override void Update()
        {
            if (rectangleCollider != null)
            {
                if (rectangleCollider.IsColliding)
                {
                    if (Manager is GameSceneTextSpriteRenderer)
                    {
                        GameSceneTextSpriteRenderer game_scene_text_sprite_renderer = (GameSceneTextSpriteRenderer)Manager;
                        game_scene_text_sprite_renderer.GameScene.TouchBarrier();
                    }
                    Manager.RemoveEntity(this);
                }
            }
        }
    }
}
