using Assets.Scripts.Game_Controller.HelpersAndClasses;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ActionDetails", menuName = "Scriptable Objects/ActionDetails")]
public class ActionDetails : ScriptableObject
{
    #region Outcomes
    [SerializeField]
    private List<Outcome> outcomes = new();

    public List<Outcome> Outcomes => outcomes;
    #endregion

    public Outcome GetOutcomeByName(string name)
    {
        return outcomes.Find(o => o.outcomeName == name);
    }
}
