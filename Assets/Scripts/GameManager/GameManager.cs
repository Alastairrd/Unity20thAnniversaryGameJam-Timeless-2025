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

    public int GlobalZombieDensity = 1;
    public List<Location> Locations;
    public int time = 8;


    private void Start()
    {
        UIManager.Instance.InputQueue(Actions.Instance.CreateQueueFromProcessedActions());
    }
}
