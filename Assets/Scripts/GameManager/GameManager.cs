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
    public int time = 8;

    #region Time

    #endregion

    private void Start()
    {
        UIManager.Instance.PrintMessage(UIManager.Instance.startMessage);
        UIManager.Instance.PrintMessage("IF LOST TYPE help. TO SKIP TEXT YOU CAN PRESS DOWN-ARROW");
        UIManager.Instance.InputQueue(Actions.Instance.CreateQueueFromProcessedActions());
    }

    
}
