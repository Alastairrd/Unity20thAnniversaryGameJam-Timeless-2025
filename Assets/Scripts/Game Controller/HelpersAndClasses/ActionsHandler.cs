using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Game_Controller.HelpersAndClasses
{
    public class ActionsHandler
    {
        public Dictionary<int, Action> Actions = new Dictionary<int, Action>(); //instead of string, action

        public ActionsHandler() { 
  
        }

        //public OldOutcome SimulateAction(int actionId)
        //{
        //    //simulate action
        //    // ... 
        //    // ...
        //    //chance at random events?
        //    //return Actions[actionId].Simulate();

        //    return new OldOutcome();
        //}

        //public OldOutcome SimulateScavenge()
        //{
        //    //ScavengeHandler.SimulateScavenge(stats);
        //    return new OldOutcome();
        //}
    }
}

