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
        Debug.Log("Adding Zombie");
    }
    public void Simulate()
    {
        GameController.Instance.baseHealth -= Damage;
        Debug.Log($"{Name} attacked Bas for {Damage}"); // base health use in separate file
        
        GameController.Instance.playerHealth -= Damage;
        Debug.Log($"{Name} attacked Player for {Damage}"); 
        
        Health -= 20;
        Debug.Log($"Player attacked {Name} for {20} Damage"); 
    }
}
