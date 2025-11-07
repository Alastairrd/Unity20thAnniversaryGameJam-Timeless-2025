using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Game_Controller.HelpersAndClasses
{
    public class ActionsHandler
    {
        public Dictionary<int, string> Actions = new Dictionary<int, string>(); //instead of string, action

        public Outcome SimulateAction(int actionId)
        {
            //simulate action
            // ... 
            // ...
            //chance at random events?
            //return Actions[actionId].Simulate();

            return new Outcome();
        }

        public Outcome SimulateScavenge()
        {
            //ScavengeHandler.SimulateScavenge(stats);
            return new Outcome();
        }
    }
}

