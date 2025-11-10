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
    public Outcome Simulate(Outcome waveResult, ref int localBaseHealth, ref int localPlayerHealth)
    {
        Debug.Log("base health inside sim " + localBaseHealth);
        if (localBaseHealth > 0)
        {
            Debug.Log("attacking base for " + Damage);
            localBaseHealth -= Damage;
            waveResult.baseHealthChange -= Damage;
            waveResult.messages.Add($"<align=\"right\">{Name} attacked Base for {Damage}</align>");
        }
        Debug.Log("base health now  " + localBaseHealth);
        if (localBaseHealth <= 0)
        {
            Debug.Log("attacking player " + localBaseHealth + " for " + Damage);
            localPlayerHealth -= Damage;
            waveResult.playerHealthChange -= Damage;
            waveResult.messages.Add($"<align=\"right\">{Name} attacked Player for {Damage}</align>");
            
        }
        return waveResult;
    }
}
