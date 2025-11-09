using NUnit.Framework;
using System.Collections.Generic;

namespace Assets.Scripts.Actions
{
    public interface IActionObject
    {
        public string actionName { get; }

        public int timeChange { get; }

        public int minWood { get; set; }
        public int minMetal { get; set; }
        public int minMedicine { get; set; }
        public int minFood { get; set; }
        List<Outcome> Outcomes { get; set; }

        // Method every action must implement
        Queue<Outcome> Simulate();

        Outcome GetOutcomeByName(string name);

        public void SetOutcomeTimeChange();

        public void SetMinResourceCosts();
        
    }
}