using System.Collections.Generic;
using Unity.VisualScripting;
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
    public List<Location> Locations = new List<Location>();
    public int time = 8;

    #region Time

    #endregion

    private void Start()
    {
        UIManager.Instance.InputQueue(Actions.Instance.CreateQueueFromProcessedActions());
    }
}
