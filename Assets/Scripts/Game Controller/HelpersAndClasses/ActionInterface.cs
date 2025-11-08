using NUnit.Framework;
using System.Collections.Generic;

namespace Assets.Scripts.Game_Controller.HelpersAndClasses
{
    public interface IActionObject
    {
        // Property to hold ActionDetails
        ActionDetails Details { get; set; }

        // Method every action must implement
        Queue<Outcome> Simulate();
    }
}