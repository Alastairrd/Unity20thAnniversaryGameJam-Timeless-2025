using Assets.Scripts.Game_Controller.HelpersAndClasses;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Actions.Scavenge
{
    public class Scavenge : MonoBehaviour, IActionObject
    {
        [SerializeField] private string _actionName = "Scavenge";
        public string actionName => _actionName;

        [SerializeField] private int _timeChange = 5;
        public int timeChange => _timeChange;
        /// <summary>
        /// RENAME ACTION NAME AND FILENAMES TO WHATEVER
        /// </summary>

        // THIS STAYS AS IS
        [SerializeField] private List<Outcome> outcomes = new();
        public List<Outcome> Outcomes
        {
            get => outcomes;
            set => outcomes = value;
        }

        // THIS STAYS AS IS
        public Outcome GetOutcomeByName(string name)
        {
            return outcomes.Find(o => o.outcomeName == name);
        }

        public Queue<Outcome> Simulate()
        {
            // THIS STAYS AS IS
            Queue<Outcome> result = new Queue<Outcome>();

            // FIND YOUR OUTCOMES BY NAME
            Outcome ShotgunOutcome = GetOutcomeByName("ShotgunFound");

            ///WRITE CODE HERE
            float randomValue = UnityEngine.Random.value;
            if (randomValue < 0.2)
            {
                result.Enqueue(ShotgunOutcome);
                return result;
            }
            else if (randomValue < 0.8)
            {
                result.Enqueue(ShotgunOutcome);
                return result;
            }
            else
            {
                result.Enqueue(ShotgunOutcome);
                return result;
            }
        }
    }
}

