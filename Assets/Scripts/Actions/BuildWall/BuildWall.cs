using Assets.Scripts.Game_Controller.HelpersAndClasses;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Actions.BuildTrap
{
    public class BuildWall : MonoBehaviour, IActionObject
    {
        [SerializeField] private string _actionName = "BuildWall";
        public string actionName => _actionName;

        [SerializeField] private int _timeChange = 3;
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

        private void Awake()
        {
            SetOutcomeTimeChange();
        }

        // THIS STAYS AS IS
        public Outcome GetOutcomeByName(string name)
        {
            return outcomes.Find(o => o.outcomeName == name);
        }

        public void SetOutcomeTimeChange()
        {
            foreach (var outcome in outcomes)
            {
                if (outcome != null)
                {
                    outcome.timeChange = _timeChange;
                }
            }
        }

        public Queue<Outcome> Simulate()
        {
            // THIS STAYS AS IS
            Queue<Outcome> result = new Queue<Outcome>();

            // FIND YOUR OUTCOMES BY NAME
            Outcome badOutcome = GetOutcomeByName("Bad");
            Outcome normalOutcome = GetOutcomeByName("Normal");
            Outcome goodOutcome = GetOutcomeByName("Good");


            ///WRITE CODE HERE
            float randomValue = UnityEngine.Random.value;
            if (randomValue < 0.2)
            {
                result.Enqueue(badOutcome);
                return result;
            }
            else if (randomValue < 0.8)
            {
                result.Enqueue(normalOutcome);
                return result;
            }
            else
            {
                result.Enqueue(goodOutcome);
                return result;
            }
        }

    }
}

