using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Outcome", menuName = "Scriptable Objects/Outcome")]
public class Outcome : ScriptableObject
{
    [SerializeField]
    public string outcomeName;

    [Header("Outcome Messages")]
    [field: SerializeField]
    public List<string> messages { get; set; }

    #region Resource Changes
    [Header("Resource Changes")]
    [SerializeField]
    int woodChange;
    [SerializeField]
    int metalChange;
    [SerializeField]
    int medicineChange;
    [SerializeField]
    int foodChange;
    #endregion

    [Header("Time Cost")]
    #region Time Cost
    [SerializeField]
    int timeCost;
    #endregion

    #region Health Changes
    [Header("Health Changes")]
    [SerializeField]
    int playerHealthChange;
    [SerializeField]
    int baseHealthChange;
    #endregion

    #region Base Changes
    [Header("Base Changes")]
    [SerializeField] private List<Building> buildingsChange = new();
    [SerializeField] private List<int> buildingsChangeAmount = new();

    public List<Building> BuildingsChange => buildingsChange;
    public List<int> BuildingsChangeAmount => buildingsChangeAmount;
    #endregion

    #region Item Changes
    [Header("Item Changes")]
    [SerializeField] private List<PlayerItem> itemsChange = new();
    [SerializeField] private List<int> itemsChangeAmount = new();

    public List<PlayerItem> ItemsChange => itemsChange;
    public List<int> ItemsChangeAmount => itemsChangeAmount;
    #endregion

}
