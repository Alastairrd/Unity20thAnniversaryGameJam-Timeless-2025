using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Game_Controller.HelpersAndClasses
{
    public class PlayerResources
    {
        public int wood;
        public int medicine;
        public int food;
        public int metal;

        //this can be changed to be seperate inventories for diff items or a dict of dicts, some way to sort would be good
        //ie: sort gun by highest dps, etc
        public Dictionary<string, object> inventory;

        public PlayerResources()
        {
            wood = 0;
            medicine = 0;
            metal = 0;
            inventory = new Dictionary<string, object>();
        }
    }
}
