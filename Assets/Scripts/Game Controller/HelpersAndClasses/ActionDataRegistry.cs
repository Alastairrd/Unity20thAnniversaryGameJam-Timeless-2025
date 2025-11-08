using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Game_Controller.HelpersAndClasses
{
    public static class ActionDataRegistry
    {
        private static Dictionary<string, ActionDetails> _details;

        static ActionDataRegistry()
        {
            _details = Resources.LoadAll<ActionDetails>("Actions")
                                .ToDictionary(d => d.name);
        }

        public static ActionDetails Get(string name) =>
            _details.TryGetValue(name, out var d) ? d : null;
    }
}
