using UnityEngine;

public class Zombie : IEnemy
{
    public string Name { get; set; }
    public int Health { get; set; }
    public int Damage { get; set; }

    public Zombie()
    {
        Name = "Zombie";
        Health = 100;
        Damage = 10;
        UIManager.Instance.currentQueue.Enqueue("Adding Zombie");
    }
    public void Simulate()
    {
        GameController.Instance.baseHealth -= Damage;
        UIManager.Instance.currentQueue.Enqueue($"{Name} attacked Base for {Damage}"); // base health use in separate file
        
        GameController.Instance.playerHealth -= Damage;
        UIManager.Instance.currentQueue.Enqueue($"{Name} attacked Player for {Damage}"); 
    }
}
