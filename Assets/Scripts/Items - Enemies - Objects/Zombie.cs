using UnityEngine;

public class Zombie : IEnemy
{
    public string Name { get; set; }
    public int Health { get; set; }
    public int Damage { get; set; }

    public Zombie(int level = 1)
    {
        Name = "Zombie";
        Health = 100;
        Damage = Random.Range(level*2,level*5);
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
