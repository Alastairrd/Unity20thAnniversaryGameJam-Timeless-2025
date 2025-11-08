using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Game_Controller.HelpersAndClasses
{
    public class BuildTrap : ActionObject
    {
        public ActionDetails actionDetails;

        public override Queue<Outcome> Simulate()
        {
            Queue<Outcome> result = new Queue<Outcome>();
            Outcome badOutcome = actionDetails.GetOutcomeByName("Bad");
            Outcome normalOutcome = actionDetails.GetOutcomeByName("Normal");
            Outcome goodOutcome = actionDetails.GetOutcomeByName("Good");
            //blah blah build trap
            //blah //blah

            float randomValue = UnityEngine.Random.value;

            if(randomValue < 0.2)
            {
                result.Enqueue(badOutcome);
                return result;
            } else if (randomValue < 0.8)
            {
                result.Enqueue(normalOutcome);
                return result;
            } else
            {
                result.Enqueue(goodOutcome);
                return result;
            }
        }
    }
}
