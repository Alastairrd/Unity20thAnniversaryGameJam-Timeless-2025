using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Game_Controller.HelpersAndClasses
{
    public class Outcome
    {
        public List<string> outcomeMessages;
        public float playerDamageTaken;
        public float baseDamageTaken;

        public Outcome()
        {

            playerDamageTaken = 0;
            baseDamageTaken = 0;
            outcomeMessages = new List<string>();

            //getting an item




        }
    }
}
