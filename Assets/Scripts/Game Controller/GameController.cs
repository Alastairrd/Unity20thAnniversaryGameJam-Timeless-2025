using UnityEngine;
using Assets.Scripts.Game_Controller.HelpersAndClasses;
using System.Collections.Generic;

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
    [SerializeField]
    public int damage = 10;

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

    [ContextMenu("Action Simulate")]
    void DebugSimulateAction()
    {
        Queue<Outcome> result = ActionsHandler.Instance.SimulateAction("BuildWall");

        while (result.Count > 0) 
        {
            Outcome outcome = result.Dequeue();
            for (int i = 0; i < outcome.messages.Count; i++) 
            {
                Debug.Log(outcome.messages[i]);
            } 
        }
    }

    [ContextMenu("MessageProcessDebug")]
    void DebugMessageSend()
    {
        Queue<string> messages = new Queue<string>();

        messages.Enqueue("Test message 1");
        messages.Enqueue("Test message 2");
        messages.Enqueue("Test message 3");
        messages.Enqueue("Test message 4");
        
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
