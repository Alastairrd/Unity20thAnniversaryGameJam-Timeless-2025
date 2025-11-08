using System.Collections.Generic;
using UnityEngine;

public class WaveHandler : MonoBehaviour
{
    public static WaveHandler Instance { get;  private set; }
    [SerializeField]
    public List<EnemyObject> enemyTypes;
    
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

    private bool checkHealth()
    {
        // Check if Player is Still Alive
        if (GameController.Instance.playerHealth < 0)
        {
            Debug.Log("Player is Dead");
            return false;
        }
        // Check if Base is still Standing
        if (GameController.Instance.baseHealth < 0)
        {
            Debug.Log("Base has been Destroyed");
            return false;
        }
        
        return true;
    }
    public void Simulate()
    {
        Debug.Log("Simulate Wave Test");
        if (!checkHealth()) Debug.Log("Game Over");
        
        



    } 
    
    
    
}