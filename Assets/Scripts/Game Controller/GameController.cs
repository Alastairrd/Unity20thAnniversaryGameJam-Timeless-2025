using UnityEngine;
using Assets.Scripts.Game_Controller.HelpersAndClasses;
using System.Collections.Generic;
using Assets.Scripts.Handlers;
using UnityEngine.SceneManagement;
using Assets.Scripts.Actions;


public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }


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

    #region temp fields
    [SerializeField]
    public bool isWaveRunning  = false;
    [SerializeField]
    public int hoursLeftToday = 16;
    [SerializeField]  
    public int wallCount = 0;
    [SerializeField]
    public int trapCount = 0;
    [SerializeField]
    public int playerHealth = 100;
    [SerializeField]
    public int baseHealth = 10;
    [SerializeField]
    public int wood = 10;
    [SerializeField]
    public int metal = 5;
    [SerializeField]
    public int medicine = 1;
    [SerializeField]
    public int food = 10;
    [SerializeField]
    public int damage = 10;

    [Header("Buildings")]
    [SerializeField] private List<Building> buildings = new();
    public List<Building> Buildings
    {
        get => buildings;
        set => buildings = value;
    }

    [Header("Items")]
    [SerializeField] private List<PlayerItem> items = new();
    public List<PlayerItem> Items
    {
        get => items;
        set => items = value;
    }
    #endregion

    #region Debug functions
    [ContextMenu("Add Trap")]
    void AddTrap()
    {
        trapCount++;
    }
    [ContextMenu("Add Wall")]
    void AddWall()
    {
        wallCount++;
    }

    [ContextMenu("Add Health")]
    void AddHealth()
    {
        playerHealth+= 10;
    }

    [ContextMenu("Add BaseHealth")]
    void AddBaseHealth()
    {
        baseHealth+= 10;
    }

    [ContextMenu("Wave Simulate")]
    void DebugSimulateWave()
    {
        Queue<Outcome> waveOutcome = WaveHandler.Instance.Simulate();
        while (waveOutcome.Count > 0)
        {
            ProcessOutcome(waveOutcome.Dequeue());
        }
    }

    [ContextMenu("Action Simulate")]
    void DebugSimulateAction()
    {
        Queue<Outcome> result = ActionsHandler.Instance.SimulateAction("Scavenge");
        result.Enqueue(ActionsHandler.Instance.SimulateAction("BuildWall").Dequeue());

        while (result.Count > 0) 
        {
            Outcome outcome = result.Dequeue();
            ProcessOutcome(outcome);
        }
    }
    #endregion

    #region GameLoop Checks and Logic
    public bool TimeLeft()
    {
        if (hoursLeftToday > 0) return true;
        return false;
    }

    bool IsPlayerDead()
    {
        if(playerHealth > 0) return true;
        return false;
    }

    bool IsBaseDestroyed()
    {
        if (baseHealth > 0) return true;
        return false;
    }
    #endregion

    void GameOver()
    {
        List<string> list = new List<string>();
        //UIManager.Instance.PrintMessage($"Please choose one of the following actions: \n");
        //UIManager.Instance.TakePossibleActions(list);
        UIManager.Instance.PrintMessage($"0: Reset");
        
    }

    public void Reset()
    {
        SceneManager.LoadScene(0);
    }

    #region Outcome Processing
    void ProcessOutcome(Outcome outcome)
    {
        //ui messages
        Queue<string> messages = new Queue<string>();
        for (int i = 0; i < outcome.messages.Count; i++)
        {
            Debug.Log(outcome.messages[i]);
            messages.Enqueue(outcome.messages[i]);
        }
        UIManager.Instance.InputQueue(messages);

        //player health
        playerHealth += outcome.playerHealthChange;
        if (playerHealth > 100) playerHealth = 100; //clamp to 100

        //resources & inventory
        wood += outcome.woodChange;
        metal += outcome.metalChange;
        food += outcome.foodChange;
        medicine += outcome.medicineChange;
        for (int i = 0; i < outcome.ItemsChange.Count; i++)
        {
            var itemReturn = outcome.ItemsChange[i];
            var itemReturnCount = outcome.ItemsChangeAmount[i];
            for (int j = 0; j < itemReturnCount; j++)
            {
                items.Add(itemReturn);
            }
        }

        //base handling
        baseHealth += outcome.baseHealthChange;
        for (int i = 0; i < outcome.BuildingsChange.Count; i++)
        {
            var buildingReturn = outcome.BuildingsChange[i];
            var buildingReturnCount = outcome.BuildingsChangeAmount[i];
            for (int j = 0; j < buildingReturnCount; j++)
            {
                buildings.Add(buildingReturn);
            }
        }
        Debug.Log($"Time Left {TimeLeft()}");

        //time cost
        hoursLeftToday -= outcome.timeChange;
        Debug.Log($"Time Left {TimeLeft()}");
        
        if (!TimeLeft() && !isWaveRunning)
        {
            isWaveRunning = true;
            DebugSimulateWave();

            
            if(checkHealth())
            {
                Debug.Log("True");
                UIManager.Instance.PrintMessage($"\n");
                UIManager.Instance.PrintMessage($"<align=\"center\">=== You survived day {(WaveHandler.Instance.wave - 1).ToString()} === </align>");
                UIManager.Instance.PrintMessage($"\n");

            }

            isWaveRunning = false;

        }

        //checks

        if (!checkHealth() && !isWaveRunning)
        {

            UIManager.Instance.PrintMessage($"\n");
            UIManager.Instance.PrintMessage($"<align=\"center\">=== You died on day {(WaveHandler.Instance.wave - 1).ToString()} === </align>");
            
            UIManager.Instance.PrintMessage($"\n");
            UIManager.Instance.PrintMessage($"<align=\"center\">=== Game Over === </align>");
            UIManager.Instance.PrintMessage("");

            GameOver();
        }
    }

    public bool checkHealth()
    {
        // Check if Player is Still Alive
        if (playerHealth <= 0)
        {
            Debug.Log("Player Death");
            return false;
        }
        // Check if Base is still Standing
        //if (baseHealth < 0)
        //{
        //    return false;
        //}
        
        return true;
    }

    void GameStateCheck()
    {
        ///
        IsBaseDestroyed();
        IsPlayerDead();
        TimeLeft();
    }

    #endregion

    #region Action Display and Simulation
    public void SimulateAction(string actionName)
    {
        Queue<Outcome> result = ActionsHandler.Instance.SimulateAction(actionName);

        while (result.Count > 0)
        {
            Outcome outcome = result.Dequeue();
            ProcessOutcome(outcome);
        }

        if(checkHealth())
        {
            UIManager.Instance.PrintMessage($"You have {hoursLeftToday} hours until the horde approaches.");
            //UIManager.Instance.TakePossibleActions(DecideActionsToDisplay());
        }
    }

    List<string> DecideActionsToDisplay()
    {
        var actionList = new List<IActionObject>(ActionsHandler.Instance.GetAllActions());

        Debug.Log(actionList.Count);
        // Time cost restrictions
        actionList.RemoveAll(action => action.timeChange > hoursLeftToday);

        // Resource cost restrictions
        actionList.RemoveAll(action =>
            wood < action.minWood ||
            metal < action.minMetal ||
            medicine < action.minMedicine ||
            food < action.minFood
        );

        //healing possible?
        actionList.RemoveAll(action => !(playerHealth < 100) && action.actionName == "HealPlayer");

        // Convert to string names
        List<string> actions = new List<string>();
        foreach (var action in actionList)
        {
            actions.Add(action.actionName);
        }
        Debug.Log(actions.Count);

        return actions;
    }
    void SendActionsToUI()
    {
        //UIManager.Instance.TakePossibleActions(DecideActionsToDisplay())
    }
    #endregion




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //UIManager.Instance.TakePossibleActions(DecideActionsToDisplay());
    }

    
    
    //void SimulateWave()
    //{
    //    //based on wave
    //    //call WaveHandler, pass base stats in, return outcomes?
    //}

    //void SimulateEvent()
    //{
    //    //call eventsHandler pool, find random event
    //    //pass in necessary info, return outcome from simulation?
    //}

    //void SimulateAction()
    //{
    //    //call actionsHandler actions pool with appropriate action
    //    //return outcome from simulation
    //}

    //void RemoveTime(int hours)
    //{
    //    //pass in cost property of an action or event
    //}

    //void DamagePlayer(float damage)
    //{
    //    //pass in damage outcome from wave, event, action
    //}
    //void AddItemToInventory(float damage)
    //{
    //    //pass in damage outcome from wave, event, action
    //}

    //void AddResource(string resource)
    //{
    //    //pass in damage outcome from wave, event, action
    //}

}
