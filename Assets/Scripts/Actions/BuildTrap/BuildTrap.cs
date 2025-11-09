using Assets.Scripts.Game_Controller.HelpersAndClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Actions.BuildTrap
{
    public class BuildTrap : MonoBehaviour, IActionObject
    {
        [SerializeField] private string _actionName = "BuildTrap";
        public string actionName => _actionName;

        [SerializeField] private int _timeChange = 2;
        public int timeChange => _timeChange;

        public int minWood { get; set; }
        public int minMetal { get; set; }
        public int minMedicine { get; set; }
        public int minFood { get; set; }

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
            Queue<Outcome> result = new Queue<Outcome>();
            Outcome badOutcome = GetOutcomeByName("Bad");
            Outcome normalOutcome = GetOutcomeByName("Normal");
            Outcome goodOutcome = GetOutcomeByName("Good");

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
