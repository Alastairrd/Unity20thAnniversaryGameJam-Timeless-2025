using System;
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
        if (Actions.Instance.AllConstantActions().Contains(input))
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

        //if (Enum.TryParse(input, true, out Actions.AllPossibleActionsEnum action)) 

        switch (input)
        {
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
                    Actions.Instance.CraftItem(InventoryItems.Items.axe);
                    break;

                case "gloves":
                    Actions.Instance.CraftItem(InventoryItems.Items.gloves);
                    break;

                case "knife":
                    Actions.Instance.CraftItem(InventoryItems.Items.knife);
                    break;

                case "picker":
                    Actions.Instance.CraftItem(InventoryItems.Items.knife);
                    break;

                case "rod":
                    Actions.Instance.CraftItem(InventoryItems.Items.knife);
                    break;

                case "wrench":
                    Actions.Instance.CraftItem(InventoryItems.Items.wrench);
                    break;

                case "gun":
                    Actions.Instance.CraftItem(InventoryItems.Items.wrench);
                    break;

                case "bullet":
                    Actions.Instance.CraftItem(InventoryItems.Items.wrench);
                    break;

            case "build":
                Actions.Instance.Build();
                break;

                case "walls":
                    Actions.Instance.BuildItem(InventoryItems.BunkerItems.walls);
                    break;

                case "reinforcement":
                    Actions.Instance.BuildItem(InventoryItems.BunkerItems.reinforcement);
                    break;

                case "traps":
                    Actions.Instance.BuildItem(InventoryItems.BunkerItems.traps);
                    break;

                case "bed":
                Actions.Instance.BuildItem(InventoryItems.BunkerItems.bed);
                    break;

                case "consume":
                    Actions.Instance.Consume();
                    break;

                    case "medicine":
                        Actions.Instance.ConsumeItem(InventoryItems.Items.medicine);
                        break;

                    case "bandade":
                        Actions.Instance.ConsumeItem(InventoryItems.Items.bandade);
                        break;

                    case "sushi":
                        Actions.Instance.ConsumeItem(InventoryItems.Items.fish);
                        break;

                    case "meat":
                        Actions.Instance.ConsumeItem(InventoryItems.Items.meat);
                        break;

                    case "vegetable":
                        Actions.Instance.ConsumeItem(InventoryItems.Items.meat);
                        break;

                    case "water":
                        Actions.Instance.ConsumeItem(InventoryItems.Items.medicine);
                        break;

            case "travel":
                Actions.Instance.Travel();
                break;

                case "yard":
                    Actions.Instance.TravelTo(LocationList.Locations.yard);
                    break;

                case "building":
                    Actions.Instance.TravelTo(LocationList.Locations.bulding);
                    break;

                case "city":
                    Actions.Instance.TravelTo(LocationList.Locations.city);
                    break;

                case "river":
                    Actions.Instance.TravelTo(LocationList.Locations.river);
                    break;

            case "forest":
                    Actions.Instance.TravelTo(LocationList.Locations.forest);
                    break;

                case "desert":
                    Actions.Instance.TravelTo(LocationList.Locations.desert);
                    break;

            case "get":
                Actions.Instance.Get();
                break;

                case "wood":
                    Actions.Instance.GetItem(InventoryItems.Items.wood);
                    break;

                case "metal":
                    Actions.Instance.GetItem(InventoryItems.Items.metal);
                    break;

                case "scraps":
                    Actions.Instance.GetItem(InventoryItems.Items.scraps);
                    break;

                case "fish":
                    Actions.Instance.GetItem(InventoryItems.Items.fish);
                    break;

                case "hunt":
                    Actions.Instance.GetItem(InventoryItems.Items.meat);
                    break;

                case "pick":
                    Actions.Instance.GetItem(InventoryItems.Items.picker);
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
