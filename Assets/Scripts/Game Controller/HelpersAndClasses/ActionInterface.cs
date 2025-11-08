using NUnit.Framework;
using System.Collections.Generic;

namespace Assets.Scripts.Game_Controller.HelpersAndClasses
{
    public interface IActionObject
    {
        public string actionName { get; }
        List<Outcome> Outcomes { get; set; }

        // Method every action must implement
        Queue<Outcome> Simulate();

        Outcome GetOutcomeByName(string name);
    }
}