using System;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public Queue<Outcome> waveResults = new Queue<Outcome>();
    
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

    
    public Queue<Outcome> Simulate()
    {
        Outcome waveResult = ScriptableObject.CreateInstance<Outcome>();
        waveResult.messages = new List<string>();
        waveResult.messages.Add($"\n");
        waveResult.messages.Add("<align=\"justified\">=== Wave ===</center>");
        waveResult.messages.Add($"\n");
         
        
        for (int i = 0; i < Random.Range(wave+1, 2*wave); i++)
        {
            enemies.Enqueue(new Zombie(Random.Range(1, wave+1)));
            enemies.Enqueue(new Raider());
        }
        
        waveResult.messages.Add($"Watch out!!! {enemies.Count} enemies");
        
        while (GameController.Instance.checkHealth() && enemies.Count > 0)
        {
            IEnemy enemy = enemies.Dequeue(); 
            // lucky move
            if (Random.Range(0, 1) < 0.1)
            {
                enemy.Health -= GameController.Instance.damage;
                waveResult.messages.Add("Lucky Strike Enemy Startled!!!");
            }

            // enemy move
            waveResult = enemy.Simulate(waveResult); 
            
            // player move
            enemy.Health -= GameController.Instance.damage;
            
            if(enemy.Health > 0) enemies.Enqueue(enemy);
        }
        if (!GameController.Instance.checkHealth()) waveResult.messages.Add("Game Over");
        else waveResult.messages.Add($"Wave Survived {wave}");
        wave++;
        waveResults.Enqueue(waveResult);
        GameController.Instance.hoursLeftToday = 24;
        return waveResults;
    } 
    
}