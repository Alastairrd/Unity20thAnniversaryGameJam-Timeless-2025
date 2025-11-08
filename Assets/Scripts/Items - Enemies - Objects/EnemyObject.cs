using UnityEngine;

[CreateAssetMenu(fileName = "EnemyObject", menuName = "Scriptable Objects/EnemyObject")]
public class EnemyObject : ScriptableObject {
    [SerializeField]
    string itemName;

    [SerializeField]
    int defensiveValue;

    [SerializeField]
    int offensiveValue; 
}
