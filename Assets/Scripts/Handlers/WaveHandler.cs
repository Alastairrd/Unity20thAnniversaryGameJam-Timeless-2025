using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveHandler : MonoBehaviour
{
    public static WaveHandler Instance { get;  private set; }
    private int wave = 1;
    // [SerializeField]
    // public List<IEnemy> enemyTypes;
    [SerializeField]
    public Queue<IEnemy> enemies = new Queue<IEnemy>();
    
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
        Debug.Log($"Simulate Wave Test {wave}");
        
        for (int i = 0; i < Random.Range(wave, 2*wave); i++)
        {
            enemies.Enqueue(new Zombie());
            enemies.Enqueue(new Raider());
        }
        
        Debug.Log($"Watch out!!! {enemies.Count} enemies");
        
        while (checkHealth() && enemies.Count > 0)
        {
            IEnemy enemy = enemies.Dequeue(); 
            // lucky move
            if (Random.Range(0, 1) < 0.1)
            {
                enemy.Health -= GameController.Instance.damage;
                Debug.Log("Lucky Strick Enemy Startled!!!");
            }

            // enemy move
            enemy.Simulate(); 
            // player move
            enemy.Health -= GameController.Instance.damage;
            
            if(enemy.Health > 0) enemies.Enqueue(enemy);
            Debug.Log("Playing");
        }
        if (!checkHealth()) Debug.Log("Game Over");
        else Debug.Log($"Wave Survived {wave}");
        wave++;

    } 
    
}