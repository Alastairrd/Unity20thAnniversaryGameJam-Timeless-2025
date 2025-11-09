using NUnit.Framework;
using System.Collections.Generic;

namespace Assets.Scripts.Actions
{
    public interface IActionObject
    {
        public string actionName { get; }
        List<Outcome> Outcomes { get; set; }

        // Method every action must implement
        Queue<Outcome> Simulate();

        Outcome GetOutcomeByName(string name);

        public void SetOutcomeTimeChange();
    }
}