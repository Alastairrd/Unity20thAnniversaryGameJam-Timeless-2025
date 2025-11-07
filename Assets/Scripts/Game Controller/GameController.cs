using UnityEngine;
using Assets.Scripts.Game_Controller.HelpersAndClasses;

public class GameController : MonoBehaviour
{
    #region Player Properties
    [SerializeField]
    public float health = 100f;
    [SerializeField]
    public int hours = 24;
    [SerializeField]
    public PlayerResources baseResources;
    #endregion

    #region GameLoop Properties
    [SerializeField]
    public int wave = 0; //waveHandler?
    #endregion

    //Aggregate and control random events and actions scripts for calling
    #region EventsHandler
    //public EventsHandler eventsHandler;
    #endregion

    //Store base actions and statistics??
    #region ActionsHandler
    // public ActionsHandler actionsHandler;
    #endregion

    //Handle simulation of zombie wave attacks at end of the day
    #region WaveHandler
    // public WaveHandler waveHandler
    #endregion



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        baseResources = new PlayerResources();  
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
