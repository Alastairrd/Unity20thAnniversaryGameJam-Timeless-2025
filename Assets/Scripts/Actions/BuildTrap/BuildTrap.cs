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

        [SerializeField] private List<Outcome> outcomes = new();
        public List<Outcome> Outcomes
        {
            get => outcomes;
            set => outcomes = value;
        }

        public Outcome GetOutcomeByName(string name)
        {
            return outcomes.Find(o => o.outcomeName == name);
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
