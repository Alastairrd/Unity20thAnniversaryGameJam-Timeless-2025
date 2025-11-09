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

        public int minWood;
        public int minMetal;
        public int minMedicine;
        public int minFood;
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
            SetMinResourceCosts();
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

        private void SetMinResourceCosts()
        {
            // Initialize to zero
            int outcomeMinWood = 0;
            int outcomeMinMetal = 0;
            int outcomeMinMedicine = 0;
            int outcomeMinFood = 0;

            foreach (var outcome in outcomes)
            {
                if (outcome == null) continue;

                // If outcome reduces resources (negative value), track the lowest (most negative)
                if (outcome.woodChange < outcomeMinWood)
                    outcomeMinWood = outcome.woodChange;

                if (outcome.metalChange < outcomeMinMetal)
                    outcomeMinMetal = outcome.metalChange;

                if (outcome.medicineChange < outcomeMinMedicine)
                    outcomeMinMedicine = outcome.medicineChange;

                if (outcome.foodChange < outcomeMinFood)
                    outcomeMinFood = outcome.foodChange;
            }

            // Convert to positive values so min* fields represent required amounts
            minWood = Mathf.Abs(outcomeMinWood);
            minMetal = Mathf.Abs(outcomeMinMetal);
            minMedicine = Mathf.Abs(outcomeMinMedicine);
            minFood = Mathf.Abs(outcomeMinFood);
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

