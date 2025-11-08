using UnityEngine;

[CreateAssetMenu(fileName = "Building", menuName = "Scriptable Objects/Building")]
public class Building : ScriptableObject
{
    [SerializeField]
    string buildingName;

    [SerializeField]
    int defensiveValue;

    [SerializeField]
    int offensiveValue; 
}
