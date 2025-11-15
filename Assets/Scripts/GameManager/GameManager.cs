using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    #region Singleton Pattern
    public static GameManager Instance { get; private set; }
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

    Queue<string> currentPossibleActions = new Queue<string>();

    public int GlobalZombieDensity = 1;
    public List<Location> Locations;


    #region ProcessingPossibleChoices
    void updatePossibleActionsQueue()
    {
        currentPossibleActions = ProcessPossibleActions();
    }

    Queue<string> ProcessPossibleActions() 
    {
        Queue<string> queue = new Queue<string>();

        if(Player.Instance.currentState == Player.PlayerStates.idle) 
        {
            if (Player.Instance.currentLocation != LocationList.Locations.Bunker)
                queue.Enqueue("bunker");
            
            queue.Enqueue("scavenge");

        }



        if(queue.Count == 0)
        {

        }

        return queue;
    }

    #endregion


    #region Action Functions

    #region ProcessingPossibleChoices

    #endregion

}
