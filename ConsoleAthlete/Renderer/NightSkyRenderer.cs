using ConsoleGameEngine;
using FastConsoleUI;
using System;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Console Athlete renderer namespace
/// </summary>
namespace ConsoleAthlete.Renderer
{
    /// <summary>
    /// Night sky renderer class
    /// </summary>
    internal class NightSkyRenderer : AManager
    {
        /// <summary>
        /// Window
        /// </summary>
        private Window window;

        /// <summary>
        /// Night sky strings
        /// </summary>
        private string[,] nightSkyStrings;

        /// <summary>
        /// Night sky offsets
        /// </summary>
        private int[] nightSkyOffsets;

        /// <summary>
        /// Random
        /// </summary>
        private Random random = new Random();

        /// <summary>
        /// Background
        /// </summary>
        private ColoredTextField background;

        /// <summary>
        /// Last horizontal position
        /// </summary>
        private int lastHorizontalPosition;

        /// <summary>
        /// Last horizon start
        /// </summary>
        private int lastHorizonStart;

        /// <summary>
        /// Last window size
        /// </summary>
        private Vector2Int lastWindowSize;

        /// <summary>
        /// Horizontal position
        /// </summary>
        public int HorizontalPosition { get; set; }

        /// <summary>
        /// Horizon start
        /// </summary>
        public int HorizonStart { get; set; } = 3;

        /// <summary>
        /// Night sky strings
        /// </summary>
        private string[,] NightSkyStrings
        {
            get
            {
                if (nightSkyStrings == null)
                {
                    nightSkyStrings = new string[128, 64];
                    nightSkyOffsets = new int[nightSkyStrings.GetLength(1)];
                    for (int x, y = 0, x_len = nightSkyStrings.GetLength(0), y_len = nightSkyStrings.GetLength(1); y < y_len; y++)
                    {
                        nightSkyOffsets[y] = random.Next(0, x_len);
                        for (x = 0; x < x_len; x++)
                        {
                            ConsoleColor color = ConsoleColor.Gray;
                            switch (random.Next(0, 4))
                            {
                                case 1:
                                    color = ConsoleColor.DarkYellow;
                                    break;
                                case 2:
                                    color = ConsoleColor.DarkCyan;
                                    break;
                                case 3:
                                    color = ConsoleColor.DarkRed;
                                    break;
                            }
                            nightSkyStrings[x, y] = ((random.Next(0, 50) == 25) ? ("<" + color + ">*") : " ");
                            //nightSkyStrings[x, y] = "<" + color + ">*";
                        }
                    }
                }
                return nightSkyStrings;
            }
        }

        /// <summary>
        /// Update night sky
        /// </summary>
        private void UpdateNightSky()
        {
            Vector2Int size = new Vector2Int(window.Width, window.Height - HorizonStart);
            if ((size.X > 0) && (size.Y > 0))
            {
                string[,] night_sky_strings = new string[size.X, size.Y];
                int width = NightSkyStrings.GetLength(0);
                int height = NightSkyStrings.GetLength(1);
                StringBuilder night_sky_string_builder = new StringBuilder();
                int h_pos = HorizontalPosition;
                while (h_pos < 0)
                {
                    h_pos += width;
                }
                Parallel.For(0, size.Y, (y) =>
                {
                    int y_index = y % height;
                    for (int x = 0; x < size.X; x++)
                    {
                        night_sky_strings[x, y] = NightSkyStrings[(h_pos + x + nightSkyOffsets[y_index]) % width, y_index];
                    }
                });
                for (int x, y = 0; y < size.Y; y++)
                {
                    if (y > 0)
                    {
                        night_sky_string_builder.Append('\n');
                    }
                    for (x = 0; x < size.X; x++)
                    {
                        night_sky_string_builder.Append(night_sky_strings[x, y]);
                    }
                }
                background.Text = night_sky_string_builder.ToString();
                night_sky_string_builder.Clear();
            }
            else
            {
                background.Text = string.Empty;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="window">Window</param>
        public NightSkyRenderer(Window window)
        {
            this.window = window;
            background = window.AddControl<ColoredTextField>(RectInt.zero);
            background.AllowTransparency = true;
            lastHorizontalPosition = HorizontalPosition;
            lastHorizonStart = HorizonStart;
            lastWindowSize = window.Size;
            UpdateNightSky();
        }

        /// <summary>
        /// Update
        /// </summary>
        public override void Update()
        {
            base.Update();
            if ((lastHorizontalPosition != HorizontalPosition) || (lastHorizonStart != HorizonStart) || (lastWindowSize != window.Size))
            {
                lastHorizontalPosition = HorizontalPosition;
                lastHorizonStart = HorizonStart;
                lastWindowSize = window.Size;
                background.Size = new Vector2Int(window.Width, window.Height - HorizonStart);
                UpdateNightSky();
            }
        }
    }
}
