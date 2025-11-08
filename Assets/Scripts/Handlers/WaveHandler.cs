using UnityEngine;

public class WaveHandler : MonoBehaviour
{
    public static WaveHandler Instance { get;  private set; }

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

    public void Simulate()
    {
        Debug.Log("Simulate Wave Test");
    } 
    
}
