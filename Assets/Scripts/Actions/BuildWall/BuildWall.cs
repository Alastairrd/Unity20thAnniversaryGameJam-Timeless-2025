using Assets.Scripts.Game_Controller.HelpersAndClasses;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildWall", menuName = "Scriptable Objects/BuildWall")]
public class BuildWall : ScriptableObject, IActionObject
{
    public string actionName = "BuildWall";
    // The actual serialized field, visible in the Inspector
    [SerializeField] private ActionDetails actionDetails;

    // Interface property wraps the serialized field
    public ActionDetails Details
    {
        get => actionDetails;
        set => actionDetails = value;
    }

    public BuildWall()
    {
    }

    public Queue<Outcome> Simulate()
    {
        Queue<Outcome> result = new Queue<Outcome>();
        Outcome badOutcome = actionDetails.GetOutcomeByName("Bad");
        Outcome normalOutcome = actionDetails.GetOutcomeByName("Normal");
        Outcome goodOutcome = actionDetails.GetOutcomeByName("Good");

        float randomValue = UnityEngine.Random.value;



        ///WRITE CODE HERE
        if (randomValue < 0.2)
        {
            result.Enqueue(badOutcome);
            return result;
        }
        else if (randomValue < 0.8)
        {
            result.Enqueue(normalOutcome);
            return result;
        }
        else
        {
            result.Enqueue(goodOutcome);
            return result;
        }
    }

}
