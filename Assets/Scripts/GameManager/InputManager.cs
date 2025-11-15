using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        "restart"
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
            ValidConstantAction(input);
        else if (CurrentPossibleActions.Contains(input))
            ValidPossibleAction(input);
        else
            UIManager.Instance.PrintMessage("You are unable to:" + input);
    }

    public void ValidConstantAction(string input)
    {
        switch (input)
        {
            case "help":
                {
                    List<string> helpQueue = new List<string>();
                    helpQueue.Add("settings -> shows commands for changing volume and text speed");
                    helpQueue.Add("actions -> shows all possible actions");
                    helpQueue.Add("restart -> start the game over");
                    UIManager.Instance.InputQueue(CreateQueueFromActions(helpQueue));
                }
                break;
            case "settings":
                {
                    List<string> helpQueue = new List<string>();
                    helpQueue.Add("up -> raise volume");
                    helpQueue.Add("down -> lower volume");
                    helpQueue.Add("faster -> make text speed faster");
                    helpQueue.Add("slow -> make text speed slower");
                    UIManager.Instance.InputQueue(CreateQueueFromActions(helpQueue));
                }
                break;
            case "up":
                UIManager.Instance.PrintMessage("Volume is no: " );
                break;
            case "down":
                break;
            case "faster":
                break;
            case "slower":
                break;
            case "actions":
                break;
            case "restart":
            SceneManager.LoadScene(0);
                break;
            default:
                break;
        }
    }

    #region Valid Input
    public void ValidPossibleAction(string input)
    {
        switch (input)
        {
            case "bunker":
                Actions.Instance.Bunker();
                break;

            case "sleep":
                Actions.Instance.Sleep();
                break;

            case "upgrade":
                Actions.Instance.Upgrade();
                break;

            case "craft":
                Actions.Instance.Craft();
                break;

                case "axe":
                    Actions.Instance.Axe();
                    break;

                case "gloves":
                    Actions.Instance.Gloves();
                    break;

                case "knife":
                    Actions.Instance.Knife();
                    break;

                case "picker":
                    Actions.Instance.Picker();
                    break;

                case "rod":
                    Actions.Instance.Rod();
                    break;

                case "wrench":
                    Actions.Instance.Wrench();
                    break;

                case "gun":
                    Actions.Instance.Gun();
                    break;

                case "bullet":
                    Actions.Instance.Bullet();
                    break;

            case "build":
                Actions.Instance.Build();
                break;

                case "walls":
                    Actions.Instance.Walls();
                    break;

                case "reinforcement":
                    Actions.Instance.Reinforcement();
                    break;

                case "traps":
                    Actions.Instance.Traps();
                    break;

                case "bed":
                    Actions.Instance.Bed();
                    break;

                case "consume":
                    Actions.Instance.Consume();
                    break;

                case "medicine":
                    Actions.Instance.Medicine();
                    break;

                case "bandade":
                    Actions.Instance.Bandade();
                    break;

                case "sushi":
                    Actions.Instance.Sushi();
                    break;

                case "meat":
                    Actions.Instance.Meat();
                    break;

                case "vegetable":
                    Actions.Instance.Vegetable();
                    break;

            case "water":
                Actions.Instance.Water();
                break;

            case "scavange":
                Actions.Instance.Scavange();
                break;

                case "yard":
                    Actions.Instance.Yard();
                    break;

                case "building":
                    Actions.Instance.Building();
                    break;

                case "city":
                    Actions.Instance.City();
                    break;

                case "river":
                    Actions.Instance.River();
                    break;

                case "forest":
                    Actions.Instance.Forest();
                    break;

                case "desert":
                    Actions.Instance.Desert();
                    break;

            case "get":
                Actions.Instance.Get();
                break;

                case "wood":
                    Actions.Instance.Wood();
                    break;

                case "metal":
                    Actions.Instance.Metal();
                    break;

                case "scraps":
                    Actions.Instance.Scraps();
                    break;

                case "fish":
                    Actions.Instance.Fish();
                    break;

                case "hunt":
                    Actions.Instance.Hunt();
                    break;

                case "pick":
                    Actions.Instance.Pick();
                    break;

            case "fight":
                Actions.Instance.Fight();
                break;

            case "flee":
                Actions.Instance.Flee();
                break;

            case "talk":
                Actions.Instance.Talk();
                break;

                case "buy":
                    Actions.Instance.Buy();
                    break;

                case "sell":
                    Actions.Instance.Sell();
                    break;

                case "trade":
                    Actions.Instance.Trade();
                    break;

            case "exit":
                Actions.Instance.Exit();
                break;

            default:
                    Debug.LogWarning(input + "should not have passed as valid input");
                return;
        }
    }

    #endregion
}
