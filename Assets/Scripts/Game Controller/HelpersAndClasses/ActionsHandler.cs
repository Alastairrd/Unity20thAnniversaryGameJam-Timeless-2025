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
        public static void Initialize(GameObject container)
        {
            if (_actions != null) return; // already initialized

            _actions = new Dictionary<string, IActionObject>();

            // Get all IActionObject components attached to the container GameObject
            var actions = container.GetComponents<IActionObject>();

            foreach (var action in actions)
            {
                // Use the actionName field as the key
                string key = action.actionName; // make sure IActionObject exposes actionName
                if (!_actions.ContainsKey(key))
                {
                    _actions.Add(key, action);
                    Debug.Log($"Registered action: {key}");
                }
                else
                {
                    Debug.LogWarning($"Action {key} is already registered!");
                }
            }
        }

        public static IActionObject GetAction(string name)
        {
            if (_actions == null)
            {
                Debug.LogError("ActionsHandler not initialized!");
                return null;
            }

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
    }
}

