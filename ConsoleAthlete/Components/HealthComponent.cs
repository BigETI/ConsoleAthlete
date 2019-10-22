using ConsoleGameEngine;
using System;

/// <summary>
/// Console Athlete components namespace
/// </summary>
namespace ConsoleAthlete.Components
{
    /// <summary>
    /// Health component
    /// </summary>
    public class HealthComponent : AComponent
    {
        /// <summary>
        /// Health
        /// </summary>
        public float Health { get; set; }

        /// <summary>
        /// Is alive
        /// </summary>
        public bool IsAlive => (Health <= 0.0f);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="entity">Entity</param>
        public HealthComponent(IEntity entity) : base(entity)
        {
            // ...
        }

        /// <summary>
        /// Damage
        /// </summary>
        /// <param name="amount">Amount</param>
        public void Damage(float amount)
        {
            Health -= Math.Abs(amount);
        }

        /// <summary>
        /// Initialize
        /// </summary>
        public override void Init()
        {
            // ...
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
