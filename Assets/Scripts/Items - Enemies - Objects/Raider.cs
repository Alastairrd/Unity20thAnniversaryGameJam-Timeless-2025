using System;
using UnityEngine;

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
        if(GameController.Instance.wood > 0) 
        {
            stole = Math.Min(10, GameController.Instance.wood);
            waveResult.woodChange -= stole;
            waveResult.messages.Add($"{Name} stole {stole} wood");
        }
        if(GameController.Instance.metal > 0) 
        {
            stole = Math.Min(10, GameController.Instance.metal);
            waveResult.metalChange -= stole;
            waveResult.messages.Add($"{Name} stole {stole} metal");
        }
        if(GameController.Instance.food > 0) 
        {
            stole = Math.Min(10, GameController.Instance.food);
            waveResult.foodChange -= stole;
            waveResult.messages.Add($"{Name} stole {stole} food");
        }

        return waveResult;
    }
}
