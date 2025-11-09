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
    }
    public Outcome Simulate(Outcome waveResult)
    {
        waveResult.baseHealthChange -= Damage;
        waveResult.messages.Add($"{Name} attacked Base for {Damage}");
        
        waveResult.playerHealthChange -= Damage;
        waveResult.messages.Add($"{Name} attacked Player for {Damage}");
        return waveResult;
    }
}
