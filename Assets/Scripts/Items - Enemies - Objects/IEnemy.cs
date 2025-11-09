using UnityEngine;
public interface IEnemy 
{
    public string Name { get; set; }
    public int Health { get; set; }
    public int Damage { get; set; }
    public Outcome Simulate(Outcome waveResult, int localBaseHealth);
}
