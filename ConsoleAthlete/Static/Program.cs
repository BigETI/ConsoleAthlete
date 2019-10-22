using System;
using System.Text;

/// <summary>
/// Console Athlete namespace
/// </summary>
namespace ConsoleAthlete
{
    /// <summary>
    /// Programm class
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Main entry point
        /// </summary>
        /// <param name="args">Command line arguments</param>
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            GameWindow game_window = new GameWindow();
            game_window.Run();
        }
    }
}
