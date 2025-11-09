using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;

public class WaveHandler : MonoBehaviour
{
    public static WaveHandler Instance { get;  private set; }
    public int wave = 1;
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
        int localBaseHealth = GameController.Instance.baseHealth;
        int localPlayerHealth = GameController.Instance.playerHealth;
        Outcome waveResult = ScriptableObject.CreateInstance<Outcome>();
        waveResult.messages = new List<string>();
        waveResult.messages.Add($"\n");
        waveResult.messages.Add($"<align=\"center\"> === Wave {wave} === </align>");
        waveResult.messages.Add($"\n");
         
        
        for (int i = 0; i < Random.Range(wave+1, 2*wave); i++)
        {
            enemies.Enqueue(new Zombie(Random.Range(1, wave+1)));
            enemies.Enqueue(new Raider());
        }
        
        waveResult.messages.Add($"Watch out!!! {enemies.Count} enemies");
        
        while (GameController.Instance.checkHealth() && enemies.Count > 0)
        {
            waveResult.messages.Add($"\n");
            IEnemy enemy = enemies.Dequeue(); 
            // lucky move
            if (Random.Range(0f, 1f) < 0.05)
            {
                enemy.Health -= GameController.Instance.damage;
                
                waveResult.messages.Add($"\n");
                waveResult.messages.Add("<align=\"center\">***Lucky Strike! Gained extra move***</align>");
                waveResult.messages.Add($"\n");
                
                waveResult.messages.Add($"Hit {enemy.Name} for {GameController.Instance.damage} damage");
            }

            // checks enemy health 
            if (enemy.Health < 0)
            {
                waveResult.messages.Add($"\n");
                waveResult.messages.Add($"<align=\"center\">{enemy.Name} defeated (X~X)</align>");
                waveResult.messages.Add($"\n");
                waveResult.messages.Add("<align=\"center\">********************</align>");
                continue;
            }

            // enemy move
            if (Random.Range(0f, 1f) < Random.Range(0.5f, 1.0f)) 
            {
                waveResult = enemy.Simulate(waveResult, localBaseHealth);
                localBaseHealth += waveResult.baseHealthChange;
                localPlayerHealth += waveResult.playerHealthChange;
            } 
            else waveResult.messages.Add($"<align=\"right\">{enemy.Name} Missed</align>");
            if (GameController.Instance.playerHealth <= 0) break;
            if (localPlayerHealth <= 0) break;
            
            // player move
            if (Random.Range(0f, 1f) < Random.Range(0.5f, 1.0f))
            {
                enemy.Health -= GameController.Instance.damage;
                waveResult.messages.Add($"Hit {enemy.Name} for {GameController.Instance.damage} damage");
            }
            else
            {
                waveResult.messages.Add($"Player missed {enemy.Name}");
            }
            // checks enemy health 
            if (enemy.Health < 0)
            {
                waveResult.messages.Add($"\n");
                waveResult.messages.Add($"<align=\"center\">{enemy.Name} defeated (X~X)</align>");
                waveResult.messages.Add($"\n");
                waveResult.messages.Add("<align=\"center\">********************</align>");
                continue;
            }
            if(enemy.Health > 0) enemies.Enqueue(enemy);
            waveResult.messages.Add("\n");
            waveResult.messages.Add("<align=\"center\">********************</align>");
        }
        

        Debug.Log("past bool statements");

        wave++;
        waveResults.Enqueue(waveResult);
        GameController.Instance.hoursLeftToday = 16;
        return waveResults;
    } 
    
}