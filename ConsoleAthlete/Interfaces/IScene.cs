using ConsoleGameEngine;
using FastConsoleUI;
using System;

/// <summary>
/// Console Athlete namespace
/// </summary>
namespace ConsoleAthlete
{
    /// <summary>
    /// Scene interface
    /// </summary>
    interface IScene : IManager
    {
        /// <summary>
        /// Window
        /// </summary>
        Window Window { get; }

        /// <summary>
        /// On key pressed
        /// </summary>
        event KeyPressedDelegate OnKeyPressed;

        /// <summary>
        /// Send key
        /// </summary>
        /// <param name="keyInfo">Key information</param>
        void SendKey(ConsoleKeyInfo keyInfo);
    }
}
