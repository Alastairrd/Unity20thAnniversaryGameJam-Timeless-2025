using UnityEngine;
using Assets.Scripts.Game_Controller.HelpersAndClasses;

public class GameController : MonoBehaviour
{
    
    //#region Player Properties
    //[SerializeField]
    //public float health = 100f;
    //[SerializeField]
    //public int hours = 24;
    //[SerializeField]
    //public PlayerResources baseResources;
    //#endregion

    //#region GameLoop Properties
    //[SerializeField]
    //public int wave = 0; //waveHandler?
    //#endregion

    ////Aggregate and control random events and actions scripts for calling
    //#region EventsHandler
    ////public EventsHandler eventsHandler;
    //#endregion

    ////Store base actions and statistics??
    //#region ActionsHandler
    //// public ActionsHandler actionsHandler;
    //#endregion

    //Handle simulation of zombie wave attacks at end of the day

    #region temp fields
    [SerializeField]  
    public int wallCount = 0;
    [SerializeField]
    public int trapCount = 0;
    [SerializeField]
    public int playerHealth = 10;
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
    #endregion

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
        WaveHandler.Instance.Simulate();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //look for player input on ui
        //handle displaying of ui based on game state, display right options

    }
    [SerializeField] private GameObject actionDatabase; // assign in Inspector

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
        ActionsHandler.Initialize(actionDatabase);
    }
    void SimulateWave()
    {
        //based on wave
        //call WaveHandler, pass base stats in, return outcomes?
    }

    void SimulateEvent()
    {
        //call eventsHandler pool, find random event
        //pass in necessary info, return outcome from simulation?
    }

    void SimulateAction()
    {
        //call actionsHandler actions pool with appropriate action
        //return outcome from simulation
    }

    void RemoveTime(int hours)
    {
        //pass in cost property of an action or event
    }

    void DamagePlayer(float damage)
    {
        //pass in damage outcome from wave, event, action
    }
    void AddItemToInventory(float damage)
    {
        //pass in damage outcome from wave, event, action
    }

    void AddResource(string resource)
    {
        //pass in damage outcome from wave, event, action
    }

}
