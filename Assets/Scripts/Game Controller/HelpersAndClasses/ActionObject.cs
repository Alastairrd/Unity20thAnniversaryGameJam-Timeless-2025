using NUnit.Framework;
using System.Collections.Generic;

namespace Assets.Scripts.Game_Controller.HelpersAndClasses
{
    public abstract class ActionObject
    {
        public ActionDetails details;

        public abstract Queue<Outcome> Simulate();
    }
}