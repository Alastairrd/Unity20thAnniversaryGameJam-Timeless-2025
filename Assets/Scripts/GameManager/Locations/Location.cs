using System.Collections.Generic;
using UnityEngine;

public class Location : MonoBehaviour
{
    public LocationList.Locations location;

    [Header("Neighbor Location")]
    public List<Location> neighborLocations = new List<Location>(1);

    [Header("Distance to Neighbors of corresponding index")]
    public List<int> neighborDistances;

    public int zombieDensity = 0;
    public int humanDensity = 0;

    public int fishDensity = 0;
    public int animalDensity = 0;
    public int vegDensity = 0;

    public int moneyDensity = 0;

    public int woodDensity = 0;
    public int metalDensity = 0;
    public int scrapsDensity = 0;
    public int leatherDensity = 0;


#if UNITY_EDITOR
    private void OnValidate()
    {
        while(neighborLocations.Count < neighborDistances.Count)
        {
            neighborDistances.Remove(0);
        }

        while (neighborLocations.Count > neighborDistances.Count)
        {
            neighborDistances.Add(0);
        }
    }
#endif
}