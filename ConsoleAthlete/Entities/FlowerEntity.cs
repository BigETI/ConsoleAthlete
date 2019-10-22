using ConsoleGameEngine;
using ConsoleGameEngine.Components;
using FastConsoleUI;

/// <summary>
/// Console Athlete entities namespace
/// </summary>
namespace ConsoleAthlete.Entities
{
    /// <summary>
    /// Flower entity
    /// </summary>
    public class FlowerEntity : AEntity
    {
        /// <summary>
        /// REctangle transformation
        /// </summary>
        private RectTransform rectangleTransform;

        /// <summary>
        /// Flower sprite
        /// </summary>
        private TextSprite flowerSprite;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="manager">Manager</param>
        public FlowerEntity(IManager manager) : base(manager)
        {
            // ...
        }

        /// <summary>
        /// Initialize
        /// </summary>
        public override void Init()
        {
            rectangleTransform = AddComponent<RectTransform>();
            flowerSprite = AddComponent<TextSprite>();
            if (rectangleTransform != null)
            {
                rectangleTransform.Rectangle = new RectInt(0, 0, 5, 3);
            }
        }

        /// <summary>
        /// Update
        /// </summary>
        public override void Update()
        {
            // ...
        }
    }
}
