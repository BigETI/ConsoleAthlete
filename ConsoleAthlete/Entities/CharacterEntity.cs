using ConsoleAthlete.Components;
using ConsoleGameEngine;
using ConsoleGameEngine.Components;
using FastConsoleUI;
using System;

/// <summary>
/// Console Athlete entities namespace
/// </summary>
namespace ConsoleAthlete.Entities
{
    /// <summary>
    /// Character entity class
    /// </summary>
    internal class CharacterEntity : AEntity
    {
        /// <summary>
        /// Player character text format
        /// </summary>
        private static readonly string[] playerCharacterTextFormat = new string[]
        {
            " <{0}>O \n<{2}>/<{1}>|<{3}>\\\n <{4}>Λ ",
            " <{0}>O \n<{2}>/<{1}>|<{3}>\\\n <{4}>Δ ",
            " <{0}>O \n<{2}>/<{1}>|<{3}>\\\n <{4}>? "
        };

        /// <summary>
        /// Gravity
        /// </summary>
        private static readonly double gravity = -98.0;

        /// <summary>
        /// Fall multiplier
        /// </summary>
        private static readonly double fallMultiplier = 1.5;

        /// <summary>
        /// Jump force
        /// </summary>
        private static readonly double jumpForce = 30.0;

        /// <summary>
        /// Gender
        /// </summary>
        private EGender gender;

        /// <summary>
        /// Character text sprite
        /// </summary>
        private TextSprite characterTextSprite;

        /// <summary>
        /// Body part health
        /// </summary>
        private HealthComponent[] bodyPartHealth = new HealthComponent[Enum.GetValues(typeof(EBodyPart)).Length];

        /// <summary>
        /// Vertical force
        /// </summary>
        private double verticalForce;

        /// <summary>
        /// Height
        /// </summary>
        private double height;

        /// <summary>
        /// Last date and time
        /// </summary>
        private DateTime lastDateTime;

        /// <summary>
        /// Rectangle transformation
        /// </summary>
        public RectTransform RectangleTransform { get; private set; }

        /// <summary>
        /// Rectangle collider
        /// </summary>
        public RectCollider RectangleCollider { get; private set; }

        /// <summary>
        /// Gender
        /// </summary>
        public EGender Gender
        {
            get => gender;
            set
            {
                gender = value;
                UpdateCharacterText();
            }
        }

        /// <summary>
        /// Is on ground
        /// </summary>
        public bool IsOnGround => ((RectangleTransform == null) ? false : (RectangleTransform.Position.Y == 0));

        /// <summary>
        /// Constuctor
        /// </summary>
        /// <param name="manager">Manager</param>
        public CharacterEntity(IManager manager) : base(manager)
        {
            // ...
        }

        /// <summary>
        /// Jump
        /// </summary>
        public void Jump()
        {
            if (IsOnGround)
            {
                verticalForce = jumpForce;
            }
        }

        /// <summary>
        /// Get body part health color
        /// </summary>
        /// <param name="bodyPart">Body part</param>
        /// <returns>Color</returns>
        private ConsoleColor GetBodyPartHealthColor(EBodyPart bodyPart)
        {
            ConsoleColor ret = ConsoleColor.White;
            float health = bodyPartHealth[(int)bodyPart].Health;
            if (health < 5.0f)
            {
                ret = ConsoleColor.Red;
            }
            else if (health < 10.0f)
            {
                ret = ConsoleColor.Yellow;
            }
            return ret;
        }

        /// <summary>
        /// Update character text
        /// </summary>
        private void UpdateCharacterText()
        {
            if (characterTextSprite != null)
            {
                characterTextSprite.Text = string.Format(playerCharacterTextFormat[(int)gender], GetBodyPartHealthColor(EBodyPart.Head), GetBodyPartHealthColor(EBodyPart.Torso), GetBodyPartHealthColor(EBodyPart.LeftArm), GetBodyPartHealthColor(EBodyPart.RightArm), GetBodyPartHealthColor(EBodyPart.LeftLeg), GetBodyPartHealthColor(EBodyPart.RightLeg));
            }
        }

        /// <summary>
        /// Distribute damage
        /// </summary>
        /// <param name="bodyPart">Body part</param>
        /// <param name="amount">Amount</param>
        /// <param name="distribution">Distribution</param>
        public void DistributeDamage(EBodyPart bodyPart, float amount, float distribution)
        {
            if (bodyPartHealth != null)
            {
                float direct_distribution = Math.Clamp(distribution, 0.0f, 1.0f);
                float absolute_amount = Math.Abs(amount);
                float direct_damage = absolute_amount * direct_distribution;
                float indirect_damage = absolute_amount * (1.0f - direct_distribution) / (bodyPartHealth.Length - 1);
                for (int i = 0; i < bodyPartHealth.Length; i++)
                {
                    EBodyPart body_part = (EBodyPart)i;
                    bodyPartHealth[i].Damage((body_part == bodyPart) ? direct_damage : indirect_damage);
                }
            }
            UpdateCharacterText();
        }

        /// <summary>
        /// Damage body part
        /// </summary>
        /// <param name="bodyPart">Body part</param>
        /// <param name="amount">Amount</param>
        public void DamageBodyPart(EBodyPart bodyPart, float amount)
        {
            UpdateCharacterText();
        }

        /// <summary>
        /// Initialize
        /// </summary>
        public override void Init()
        {
            RectangleTransform = RequireComponent<RectTransform>();
            RectangleCollider = RequireComponent<RectCollider>();
            characterTextSprite = RequireComponent<TextSprite>();
            for (int i = 0; i < bodyPartHealth.Length; i++)
            {
                HealthComponent health = AddComponent<HealthComponent>();
                health.Health = 20.0f;
                bodyPartHealth[i] = health;
            }
            if (RectangleTransform != null)
            {
                RectangleTransform.Rectangle = new RectInt(0, 0, 3, 3);
            }
            UpdateCharacterText();
            lastDateTime = DateTime.Now;
            base.Init();
        }

        /// <summary>
        /// Update
        /// </summary>
        public override void Update()
        {
            DateTime now = DateTime.Now;
            double seconds = (now - lastDateTime).TotalSeconds;
            lastDateTime = now;
            height = Math.Max(height + (((verticalForce < 0.0) ? (verticalForce * fallMultiplier) : verticalForce) * seconds), 0.0);
            verticalForce += gravity * seconds;
            if (height <= float.Epsilon)
            {
                verticalForce = 0.0f;
            }
            RectangleTransform.Position = new Vector2Int(RectangleTransform.Position.X, (int)(Math.Floor(-height)));
        }
    }
}
