using Assets.Scripts.Game_Controller.HelpersAndClasses;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ActionDatabase", menuName = "Scriptable Objects/ActionDatabase")]
public class ActionDatabase : ScriptableObject
{
    [Header("All available actions in the game")]
    [SerializeField] // ensures private fields show up in Inspector
    public List<IActionObject> allActions = new List<IActionObject>();
}

