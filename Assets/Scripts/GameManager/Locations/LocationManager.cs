using System.Collections.Generic;
using UnityEngine;
using static LocationList;

public class LocationManager : MonoBehaviour
{
    #region Singleton Pattern
    public static LocationManager Instance { get; private set; }
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
    #endregion

    public Location[] Locations;

    private void Start()
    {
        Locations = GetComponentsInChildren<Location>();   
    }

    public Location currentLocationScript() 
    {
        Location currentLocationScript = null;
        foreach (Location location in Locations)
        {
            if(location.location == Player.Instance.currentLocation)
                currentLocationScript = location;
        }

        return currentLocationScript;
    }

    public List<string> possibleThingsToGetHere()
    {
        Location currentLocation = currentLocationScript();
        List<string> result = new List<string>();

        if (currentLocation.fishDensity > 0)
            result.Add("fish");
        if (currentLocation.animalDensity > 0)
            result.Add("hunt");
        if (currentLocation.vegDensity > 0)
            result.Add("pick");

        if (currentLocation.metalDensity > 0)
            result.Add("metal");
        if (currentLocation.woodDensity > 0)
            result.Add("wood");
        if (currentLocation.scrapsDensity > 0)
            result.Add("scraps");
        if (currentLocation.leatherDensity > 0)
            result.Add("leather");


        return result;
    }

}
