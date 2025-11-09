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
    public int woodChange;
    [SerializeField]
    public int metalChange;
    [SerializeField]
    public int medicineChange;
    [SerializeField]
    public int foodChange;
    #endregion

    #region Time Change
    public int timeChange;
    #endregion

    #region Health Changes
    [Header("Health Changes")]
    [SerializeField]
    public int playerHealthChange;
    [SerializeField]
    public int baseHealthChange;
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
