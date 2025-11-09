using Assets.Scripts.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Handlers
{
    public class ActionsHandler : MonoBehaviour
    {
        public static ActionsHandler Instance { get; private set; }

        [SerializeField] private GameObject actionDatabase; // assign in Inspector
        // Dictionary keyed by actionName or class name
        private Dictionary<string, IActionObject> _actions;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;

                if (_actions != null) return; // already initialized

                _actions = new Dictionary<string, IActionObject>();

                // Get all IActionObject components attached to the container GameObject
                var actions = actionDatabase.GetComponents<IActionObject>();

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
        }

        public IActionObject GetAction(string name)
        {
            if (_actions == null)
            {
                Debug.LogError("ActionsHandler not initialized!");
                return null;
            }

            return _actions.TryGetValue(name, out var action) ? action : null;
        }

        // Simulate an action by name
        public Queue<Outcome> SimulateAction(string name)
        {
            var action = GetAction(name);
            if (action != null)
            {
                Debug.Log("calling action simulate");
                return action.Simulate();
            }

            Debug.LogWarning($"Action '{name}' not found!");
            return new Queue<Outcome>();
        }

        public List<IActionObject> GetAllActions()
        {
            if (_actions == null || _actions.Count == 0)
            {
                Debug.LogWarning("No actions registered or ActionsHandler not initialized!");
                return new List<IActionObject>();
            }

            return _actions.Values.ToList();
        }

        //// Lazy-load all actions
        //public static void Initialize(GameObject container)
        //{
        //    if (_actions != null) return; // already initialized

        //    _actions = new Dictionary<string, IActionObject>();

        //    // Get all IActionObject components attached to the container GameObject
        //    var actions = container.GetComponents<IActionObject>();

        //    foreach (var action in actions)
        //    {
        //        // Use the actionName field as the key
        //        string key = action.actionName; // make sure IActionObject exposes actionName
        //        if (!_actions.ContainsKey(key))
        //        {
        //            _actions.Add(key, action);
        //            Debug.Log($"Registered action: {key}");
        //        }
        //        else
        //        {
        //            Debug.LogWarning($"Action {key} is already registered!");
        //        }
        //    }
        //}
    }
}

