using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Raider : IEnemy
{
    public string Name { get; set; }
    public int Health { get; set; }
    public int Damage { get; set; }

    public Raider(int level = 0)
    {
        Name = "Raider";
        Health = 20;
        Damage = 0;
    }
    public Outcome Simulate(Outcome waveResult)
    {
        int stole = 0;
        if(GameController.Instance.wood > 0 && Random.Range(0f, 1f) < 0.5f) 
        {
            stole = Math.Min(Random.Range(0,10), GameController.Instance.wood);
            waveResult.woodChange -= stole;
            waveResult.messages.Add($"<align=\"right\">{Name} stole {stole} wood</align>");
        }
        if(GameController.Instance.metal > 0 && Random.Range(0f, 1f) < 0.3f) 
        {
            stole = Math.Min(Random.Range(0,10), GameController.Instance.metal);
            waveResult.metalChange -= stole;
            waveResult.messages.Add($"<align=\"right\">{Name} stole {stole} metal</align>");
        }
        if(GameController.Instance.food > 0 && Random.Range(0f, 1f) < 0.3f) 
        {
            stole = Math.Min(Random.Range(0,10), GameController.Instance.food);
            waveResult.foodChange -= stole;
            waveResult.messages.Add($"<align=\"right\">{Name} stole {stole} food</align>");
        }

        return waveResult;
    }
}
