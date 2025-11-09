using Assets.Scripts.Game_Controller.HelpersAndClasses;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Actions.Scavenge
{
    public class Scavenge : MonoBehaviour, IActionObject
    {
        [SerializeField] private string _actionName = "Scavenge";
        public string actionName => _actionName;

        [SerializeField] private int _timeChange = 5;
        public int timeChange => _timeChange;

        public int minWood { get; set; }
        public int minMetal { get; set; }
        public int minMedicine { get; set; }
        public int minFood { get; set; }
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

        public void SetMinResourceCosts()
        {
            // Initialize to zero
            int outcomeMinWood = 4;
            int outcomeMinMetal = 4;
            int outcomeMinMedicine = 4;
            //int outcomeMinFood = 0;

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

                //if (outcome.foodChange < outcomeMinFood)
                //    outcomeMinFood = outcome.foodChange;
            }

            // Convert to positive values so min* fields represent required amounts
            minWood = Mathf.Abs(outcomeMinWood);
            minMetal = Mathf.Abs(outcomeMinMetal);
            minMedicine = Mathf.Abs(outcomeMinMedicine);
            //minFood = Mathf.Abs(outcomeMinFood);
        }

        public Queue<Outcome> Simulate()
        {
            // THIS STAYS AS IS
            Queue<Outcome> result = new Queue<Outcome>();

            // FIND OUTCOME RANDOMLY
            int rand = Random.Range(0, outcomes.Count);
            Outcome chosenOutcome = outcomes[rand];

            result.Enqueue(chosenOutcome);
            return result;

            ///WRITE CODE HERE
            //float randomValue = UnityEngine.Random.value;
            //if (randomValue < 0.2)
            //{
            //    result.Enqueue(ShotgunOutcome);
            //    return result;
            //}
            //else if (randomValue < 0.8)
            //{
            //    result.Enqueue(ShotgunOutcome);
            //    return result;
            //}
            //else
            //{
            //    result.Enqueue(ShotgunOutcome);
            //    return result;
            //}
        }

    }
}

