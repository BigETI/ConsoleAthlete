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
    /// Ground renderer class
    /// </summary>
    internal class GroundRenderer : AManager
    {
        /// <summary>
        /// Window
        /// </summary>
        private Window window;

        /// <summary>
        /// Background
        /// </summary>
        private ColoredTextField background;

        /// <summary>
        /// Top pattern
        /// </summary>
        private string topPattern;

        /// <summary>
        /// Bottom pattern
        /// </summary>
        private string bottomPattern;

        /// <summary>
        /// Last ground height
        /// </summary>
        private uint lastGroundHeight;

        /// <summary>
        /// Last pattern body length
        /// </summary>
        private uint lastPatternBodyLength;

        /// <summary>
        /// Last pattern line length
        /// </summary>
        private uint lastPatternLineLength;

        /// <summary>
        /// Last horizontal position
        /// </summary>
        private int lastHorizontalPosition;

        /// <summary>
        /// Last window width
        /// </summary>
        private int lastWindowWidth;

        /// <summary>
        /// Last top ground color
        /// </summary>
        private ConsoleColor lastTopGroundColor;

        /// <summary>
        /// Last bottom ground color
        /// </summary>
        private ConsoleColor lastBottomGroundColor;

        /// <summary>
        /// Last top line color
        /// </summary>
        private ConsoleColor lastTopLineColor;

        /// <summary>
        /// Last bottom bottom line color
        /// </summary>
        private ConsoleColor lastBottomLineColor;

        /// <summary>
        /// Top ground color
        /// </summary>
        public ConsoleColor TopGroundColor { get; set; } = ConsoleColor.DarkGray;

        /// <summary>
        /// Bottom ground color
        /// </summary>
        public ConsoleColor BottomGroundColor { get; set; } = ConsoleColor.Gray;

        /// <summary>
        /// Top line color
        /// </summary>
        public ConsoleColor TopLineColor { get; set; } = ConsoleColor.Gray;

        /// <summary>
        /// Bottom line color
        /// </summary>
        public ConsoleColor BottomLineColor { get; set; } = ConsoleColor.White;

        /// <summary>
        /// Ground height
        /// </summary>
        public uint GroundHeight { get; set; } = 4U;

        /// <summary>
        /// Pattern body height
        /// </summary>
        public uint PatternBodyLength { get; set; } = 30U;

        /// <summary>
        /// Pattern line length
        /// </summary>
        public uint PatternLineLength { get; set; } = 2U;

        /// <summary>
        /// Horizontal position
        /// </summary>
        public int HorizontalPosition { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="window">Window</param>
        public GroundRenderer(Window window)
        {
            this.window = window;
            background = window.AddControl<ColoredTextField>(0, 0, window.Width, (int)GroundHeight);
            background.TextAlignment = ETextAlignment.BottomLeft;
            lastPatternBodyLength = PatternBodyLength;
            lastPatternLineLength = PatternLineLength;
            lastTopGroundColor = TopGroundColor;
            lastBottomGroundColor = BottomGroundColor;
            lastTopLineColor = TopLineColor;
            lastBottomLineColor = BottomLineColor;
            lastGroundHeight = GroundHeight;
            lastHorizontalPosition = HorizontalPosition;
            lastWindowWidth = window.Width;
            UpdateGroundPattern();
        }

        /// <summary>
        /// Update ground pattern
        /// </summary>
        private void UpdateGroundPattern()
        {
            topPattern = "[" + TopGroundColor + "]" + (new string(' ', (int)PatternBodyLength)) + "[" + TopLineColor + "]" + (new string(' ', (int)PatternLineLength));
            bottomPattern = "[" + BottomGroundColor + "]" + (new string(' ', (int)PatternBodyLength)) + "[" + BottomLineColor + "]" + (new string(' ', (int)PatternLineLength));
            UpdateGround();
        }

        /// <summary>
        /// Update ground
        /// </summary>
        private void UpdateGround()
        {
            StringBuilder ground_string_builder = new StringBuilder();
            int h_pos = HorizontalPosition;
            int pattern_length = (int)(PatternBodyLength + PatternLineLength);
            int columns = (window.Width / pattern_length) + 2;
            while (h_pos < 0)
            {
                h_pos += pattern_length;
            }
            for (int i = 0, j; i < GroundHeight; i++)
            {
                if (i > 0)
                {
                    ground_string_builder.Append("\n");
                }
                for (j = 0; j < columns; j++)
                {
                    ground_string_builder.Append((i > 0) ? bottomPattern : topPattern);
                }
            }
            int offset = h_pos % pattern_length;
            background.Rectangle = new RectInt(-offset, window.Height - (int)GroundHeight, window.Width + offset, (int)GroundHeight);
            background.Text = ground_string_builder.ToString();
            ground_string_builder.Clear();
        }

        /// <summary>
        /// Update
        /// </summary>
        public override void Update()
        {
            base.Update();
            if ((lastPatternBodyLength != PatternBodyLength) || (lastPatternLineLength != PatternLineLength) || (lastTopGroundColor != TopGroundColor) || (lastBottomGroundColor != BottomGroundColor) || (lastTopLineColor != TopLineColor) || (lastBottomLineColor != BottomLineColor))
            {
                lastPatternBodyLength = PatternBodyLength;
                lastPatternLineLength = PatternLineLength;
                lastTopGroundColor = TopGroundColor;
                lastBottomGroundColor = BottomGroundColor;
                lastTopLineColor = TopLineColor;
                lastBottomLineColor = BottomLineColor;
                UpdateGroundPattern();
            }
            else if ((lastGroundHeight != GroundHeight) || (lastHorizontalPosition != HorizontalPosition) || (lastWindowWidth != window.Width))
            {
                lastGroundHeight = GroundHeight;
                lastHorizontalPosition = HorizontalPosition;
                lastWindowWidth = window.Width;
                UpdateGround();
            }
        }
    }
}
