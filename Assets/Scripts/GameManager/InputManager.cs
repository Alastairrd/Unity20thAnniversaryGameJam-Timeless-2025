using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    #region Singleton Pattern
    public static InputManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion

    public HashSet<string> AllPossibleActions = new HashSet<string>()
    {
            "bunker",
            "sleep",
            "upgrade", //same as craft, but of things you already have
            "craft",
                "axe",      
                "gloves",
                "knife",
                "picker",
                "rod",
                "wrench",
                "gun",
                "bullet",
            "build",
                "walls",
                "reinforcement",
                "traps",
                "bed",
            "consume",
                "medicine",
                "bandade",
                "fish",
                "meat",
                "vegetable",
                "water",
            "scavange",
                "yard",
                "building",
                "city",
                "river",
                "forest",
                "desert",
            "get",
                "wood",
                "metal",
                "scraps",
                "fish",
                "hunt",
                "pick",
                "water",
            "fight",
            "flee", //goes to bunker
            "talk", //human interaction
                "buy",   //will offer a random item at a random cost dependant on sanity
                "sell",  //will offer a random price at a random 
                "trade", //will offer a random item for another random item
            "exit"
    };


    public HashSet<string> CurrentPossibleActions = new HashSet<string> 
    {
         "scavenge",
    };

    public HashSet<string> ConstantActions = new HashSet<string>
    {
        "help",    //prints all constant actions
        "settings",
        "up",
        "down",
        "faster",
        "slower",
        "actions", //possible actions
    };


    public Queue<string> CreateQueueFromActions(List<string> possibleActions)
    {
        Queue<string> actionQueue = new Queue<string>();

        actionQueue.Enqueue("\n Please choose one of the following actions: \n");
        for (int i = 0; i < possibleActions.Count; i++)
        {
            actionQueue.Enqueue((i + 1).ToString() + ". " + possibleActions[i] + "   ");
        }
        actionQueue.Enqueue("\n");

        return actionQueue;
    }


    public void HandleInputLogic(string input) 
    {
        if (ConstantActions.Contains(input))
            GameManager.Instance.ValidInput(input);
        else if (CurrentPossibleActions.Contains(input))
            GameManager.Instance.ValidInput(input);
        else
            UIManager.Instance.PrintMessage("You are unable to:" + input);
    }
}
