using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Game_Controller.HelpersAndClasses
{
    public static class ActionsHandler
    {
        // Dictionary keyed by actionName or class name
        private static Dictionary<string, IActionObject> _actions;

        // Lazy-load all actions
        public static void Initialize()
        {
            if (_actions != null) return; // already initialized

            _actions = new Dictionary<string, IActionObject>();

            // Load all ScriptableObjects of type IActionObject (i.e., all actions)
            var loadedActions = Resources.LoadAll<ScriptableObject>("Actions");

            foreach (var obj in loadedActions)
            {
                if (obj is IActionObject action)
                {
                    string key = action.Details != null ? action.Details.name : obj.name;
                    if (!_actions.ContainsKey(key))
                    {
                        _actions.Add(key, action);
                        Debug.Log($"Registered Action: {key}");
                    }
                }
            }
        }

        // Get an action by name
        public static IActionObject GetAction(string name)
        {
            Initialize(); // ensure dictionary is loaded
            return _actions.TryGetValue(name, out var action) ? action : null;
        }

        // Simulate an action by name
        public static Queue<Outcome> SimulateAction(string name)
        {
            var action = GetAction(name);
            if (action != null)
            {
                return action.Simulate();
            }

            Debug.LogWarning($"Action '{name}' not found!");
            return new Queue<Outcome>();
        }

        // Optional: get all registered actions
        public static IEnumerable<string> GetAllActionNames()
        {
            Initialize();
            return _actions.Keys;
        }
    }
}

