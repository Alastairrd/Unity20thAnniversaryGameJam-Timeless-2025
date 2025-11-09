using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
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
            UIManager.Instance.currentQueue.Enqueue("Player is Dead");
            return false;
        }
        // Check if Base is still Standing
        if (GameController.Instance.baseHealth < 0)
        {
            UIManager.Instance.currentQueue.Enqueue("Base has been Destroyed");
            return false;
        }
        
        return true;
    }
    public void Simulate()
    {
        UIManager.Instance.currentQueue.Enqueue($"Simulate Wave Test {wave}");
        
        for (int i = 0; i < Random.Range(wave, 2*wave); i++)
        {
            enemies.Enqueue(new Zombie());
            enemies.Enqueue(new Raider());
        }
        
        UIManager.Instance.currentQueue.Enqueue($"Watch out!!! {enemies.Count} enemies");
        
        while (checkHealth() && enemies.Count > 0)
        {
            IEnemy enemy = enemies.Dequeue(); 
            // lucky move
            if (Random.Range(0, 1) < 0.1)
            {
                enemy.Health -= GameController.Instance.damage;
                UIManager.Instance.currentQueue.Enqueue("Lucky Strike Enemy Startled!!!");
            }

            // enemy move
            enemy.Simulate(); 
            // player move
            enemy.Health -= GameController.Instance.damage;
            
            if(enemy.Health > 0) enemies.Enqueue(enemy);
        }
        if (!checkHealth()) UIManager.Instance.currentQueue.Enqueue("Game Over");
        else UIManager.Instance.currentQueue.Enqueue($"Wave Survived {wave}");
        wave++;
        Debug.Log("qCount " + UIManager.Instance.currentQueue.Count);
        StartCoroutine(UIManager.Instance.PrintCurrentQueue());

    } 
    
}