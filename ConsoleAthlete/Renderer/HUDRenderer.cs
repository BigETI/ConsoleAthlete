using ConsoleGameEngine;
using FastConsoleUI;
using System;
using System.Text;

/// <summary>
/// Console Athlete renderer namespace
/// </summary>
namespace ConsoleAthlete.Renderer
{
    /// <summary>
    /// HUD renderer class
    /// </summary>
    internal class HUDRenderer : AManager
    {
        /// <summary>
        /// Coins per second
        /// </summary>
        private static readonly double coinsPerSecond = 50.0;

        /// <summary>
        /// Points per second
        /// </summary>
        private static readonly double pointsPerSecond = 100000.0;

        /// <summary>
        /// HUD text field
        /// </summary>
        private ColoredTextField hudTextField;

        /// <summary>
        /// Points
        /// </summary>
        private long points;

        /// <summary>
        /// Coins
        /// </summary>
        private uint coins;

        /// <summary>
        /// Lives
        /// </summary>
        private uint lives;

        /// <summary>
        /// Display points
        /// </summary>
        private long displayPoints;

        /// <summary>
        /// Display coins
        /// </summary>
        private uint displayCoins;

        /// <summary>
        /// IS game running
        /// </summary>
        private bool isGameRunning;

        /// <summary>
        /// Last date and time
        /// </summary>
        private DateTime lastDateTime;

        /// <summary>
        /// Coins
        /// </summary>
        public uint Coins
        {
            get => coins;
            set
            {
                coins = value;
            }
        }

        /// <summary>
        /// Points
        /// </summary>
        public long Points
        {
            get => points;
            set
            {
                points = value;
            }
        }

        /// <summary>
        /// Lives
        /// </summary>
        public uint Lives
        {
            get => lives;
            set
            {
                if (lives != value)
                {
                    lives = value;
                    UpdateHUDText();
                }
            }
        }

        /// <summary>
        /// IS game running
        /// </summary>
        public bool IsGameRunning
        {
            get => isGameRunning;
            set
            {
                if (isGameRunning != value)
                {
                    isGameRunning = value;
                    UpdateHUDText();
                }
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="window">Window</param>
        public HUDRenderer(Window window)
        {
            hudTextField = window.AddControl<ColoredTextField>(1, 1, 0, 0);
            hudTextField.AllowTransparency = true;
            window.OnWindowResized += WindowResizedEvent;
            lastDateTime = DateTime.Now;
            UpdateHUDText();
        }

        /// <summary>
        /// Update HUD text
        /// </summary>
        private void UpdateHUDText()
        {
            if (hudTextField != null)
            {
                if (isGameRunning)
                {
                    StringBuilder hud_text_builder = new StringBuilder();
                    if (lives == 0U)
                    {
                        hudTextField.TextAlignment = ETextAlignment.Center;
                        hud_text_builder.Append("<Red>GAME OVER!\n\n<Default>");
                    }
                    else
                    {
                        hudTextField.TextAlignment = ETextAlignment.TopLeft;
                    }
                    hud_text_builder.Append("Points: <Cyan>");
                    hud_text_builder.Append(displayPoints.ToString());
                    hud_text_builder.Append("\n<Default>Coins: <Yellow>");
                    hud_text_builder.Append(displayCoins.ToString());
                    hud_text_builder.Append("\n<Default>Lives: <Red>");
                    hud_text_builder.Append(lives.ToString());
                    hudTextField.Text = hud_text_builder.ToString();
                    hud_text_builder.Clear();
                }
                else
                {
                    hudTextField.TextAlignment = ETextAlignment.Center;
                    hudTextField.Text = "Press\n<Green>Spacebar<Default>, <Green>W <Default>or <Green>Up <Default>\nto continue!";
                }
            }
        }

        /// <summary>
        /// Window resized event
        /// </summary>
        /// <param name="size">Size</param>
        private void WindowResizedEvent(Vector2Int size)
        {
            hudTextField.Size = Vector2Int.Max(size - Vector2Int.two, Vector2Int.zero);
        }

        /// <summary>
        /// Update
        /// </summary>
        public override void Update()
        {
            DateTime now = DateTime.Now;
            double seconds = (now - lastDateTime).TotalSeconds;
            uint count_coins = (uint)(Math.Round(seconds * coinsPerSecond));
            long count_points = (uint)(Math.Round(seconds * pointsPerSecond));
            bool update_hud_text = false;
            lastDateTime = now;
            if (displayCoins < coins)
            {
                displayCoins += count_coins;
                if (displayCoins > coins)
                {
                    displayCoins = coins;
                }
                update_hud_text = true;
            }
            else if (displayCoins > coins)
            {
                if (count_coins > displayCoins)
                {
                    displayCoins = 0U;
                }
                else
                {
                    displayCoins -= count_coins;
                }
                if (displayCoins < coins)
                {
                    displayCoins = coins;
                }
                update_hud_text = true;
            }
            if (displayPoints < points)
            {
                displayPoints += count_points;
                if (displayPoints > points)
                {
                    displayPoints = points;
                }
                update_hud_text = true;
            }
            else if (displayPoints > points)
            {
                displayPoints -= count_points;
                if (displayPoints < points)
                {
                    displayPoints = points;
                }
                update_hud_text = true;
            }
            if (update_hud_text)
            {
                UpdateHUDText();
            }
            base.Update();
        }
    }
}
