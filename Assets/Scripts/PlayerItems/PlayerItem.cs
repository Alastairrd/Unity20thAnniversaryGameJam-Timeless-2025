using UnityEngine;

[CreateAssetMenu(fileName = "PlayerItem", menuName = "Scriptable Objects/PlayerItem")]
public class PlayerItem : ScriptableObject
{
    [SerializeField]
    string itemName;

    [SerializeField]
    int defensiveValue;

    [SerializeField]
    int offensiveValue; 
}
