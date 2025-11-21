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

    public void HandleInputLogic(string input) 
    {
        if (Actions.Instance.ConstantActions.Contains(input))
            ValidConstantAction(input);
        else if (Actions.Instance.HashFromListOfPossibleActions(Actions.Instance.ProcessPossibleActions()).Contains(input))
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
                    List<string> helpList = new List<string>()
                    {
                        "settings -> shows commands for changing volume and text speed",
                        "actions -> shows all possible actions",
                        "restart -> start the game over"
                    };
                    UIManager.Instance.InputQueue(Actions.Instance.CreateQueueFromAListOfActions(helpList));
                }
                break;
            case "settings":
                {
                    List<string> helpList = new List<string>()
                    {
                        "up -> raise volume",
                        "down -> lower volume",
                        "faster -> make text speed faster",
                        "slow -> make text speed slower"
                    };
                    UIManager.Instance.InputQueue(Actions.Instance.CreateQueueFromAListOfActions(helpList));
                }
                break;
            case "up":
                UIManager.Instance.IncreaseVolume();
                UIManager.Instance.PrintMessage("volume: " + AudioListener.volume);
                break;
            case "down":
                UIManager.Instance.DecreaseVolume();
                UIManager.Instance.PrintMessage("volume: " + AudioListener.volume);
                break;
            case "faster":
                UIManager.Instance.IncreaseTextSpeed();
                UIManager.Instance.PrintMessage("text Speed: " + UIManager.Instance.textSpeed);
                break;
            case "slower":
                UIManager.Instance.DecreaseTextSpeed();
                UIManager.Instance.PrintMessage("text Speed: " + UIManager.Instance.textSpeed);
                break;
            case "actions":
                UIManager.Instance.InputQueue(Actions.Instance.CreateQueueFromProcessedActions());
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
                case "shoot":
                    Actions.Instance.Shoot();
                    break;

                case "attack":
                    Actions.Instance.Attack();
                    break;

                case "dash":
                    Actions.Instance.Dash();
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
                    Debug.LogWarning(input + " should not have passed as valid input");
                return;
        }
    }

    #endregion
}
