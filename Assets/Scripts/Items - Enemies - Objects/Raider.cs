using System;
using UnityEngine;

public class Raider : IEnemy
{
    public string Name { get; set; }
    public int Health { get; set; }
    public int Damage { get; set; }

    public Raider()
    {
        Name = "Raider";
        Health = 20;
        Damage = 0;
    }
    public void Simulate()
    {
        int stole = 0;
        if(GameController.Instance.wood > 0) 
        {
            stole = Math.Min(10, GameController.Instance.wood);
            GameController.Instance.wood -= stole;
            Debug.Log($"{Name} stole {stole} wood");
        }
        if(GameController.Instance.metal > 0) 
        {
            stole = Math.Min(10, GameController.Instance.metal);
            GameController.Instance.metal -= stole;
            Debug.Log($"{Name} stole {stole} metal");
        }
        if(GameController.Instance.food > 0) 
        {
            stole = Math.Min(10, GameController.Instance.food);
            GameController.Instance.food -= stole;
            Debug.Log($"{Name} stole {stole} food");
        }
    }
}
