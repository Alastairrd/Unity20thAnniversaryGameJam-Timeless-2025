using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSettings", menuName = "Scriptable Objects/PlayerSettings")]
public class PlayerSettings : ScriptableObject
{
    public float health = 100;
    public float maxHealth = 100;
    public float maxHealthIncrements = 10;
    public int numberOfWounds = 0;
    public float tiredness = 0;
    public float weight = 0;
    public Player.PlayerStates playerState = Player.PlayerStates.idle;
    public LocationList.Locations location = LocationList.Locations.bunker;
    public Dictionary<InventoryItems.Items, int> inventoryDictionary = new Dictionary<InventoryItems.Items, int>();
    public Dictionary<InventoryItems.BunkerItems, int> bunkerDictionary = new Dictionary<InventoryItems.BunkerItems, int>();


    [Header("DO NOT CHANGE NAME")]
    [SerializeField]
    private List<string> itemNames = new List<string>();

    [Header("Use Item Names to know what item it is")]
    [SerializeField]
    private List<int> itemAmounts = new List<int>();

    [Header("DO NOT CHANGE NAME")]
    [SerializeField]
    private List<string> bunkerItemNames = new List<string>();

    [Header("Use Bunker Item Names to know what item it is")]
    [SerializeField]
    private List<int> bunkerItemAmounts = new List<int>();

    public int GetItemsAmount(InventoryItems.Items item)
    {
        int index = (int)item;
        if (index < 0 || index >= itemAmounts.Count)
            return 0;

        return itemAmounts[index];
    }

    public int GetBunkerItemsAmount(InventoryItems.Items item)
    {
        int index = (int)item;
        if (index < 0 || index >= itemAmounts.Count)
            return 0;

        return itemAmounts[index];
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        var items = Enum.GetNames(typeof(InventoryItems.Items));

        // Resize name list to match enum
        itemNames = new List<string>(items);

        // Resize amount list to match enum
        if (itemAmounts.Count != items.Length)
        {
            while (itemAmounts.Count < items.Length)
                itemAmounts.Add(0);

            while (itemAmounts.Count > items.Length)
                itemAmounts.RemoveAt(itemAmounts.Count - 1);
        }

        for(int i = 0; i < itemNames.Count; i++) 
        {
            if(itemAmounts[i] != 0) 
                inventoryDictionary.Add((InventoryItems.Items)i, itemAmounts[i]);
        }


        var bunkerItems = Enum.GetNames(typeof(InventoryItems.BunkerItems));

        // Resize name list to match enum
        bunkerItemNames = new List<string>(bunkerItems);

        // Resize amount list to match enum
        if (bunkerItemAmounts.Count != bunkerItems.Length)
        {
            while (bunkerItemAmounts.Count < bunkerItems.Length)
                bunkerItemAmounts.Add(0);

            while (bunkerItemAmounts.Count > bunkerItems.Length)
                bunkerItemAmounts.RemoveAt(bunkerItemAmounts.Count - 1);
        }

        for (int i = 0; i < bunkerItemNames.Count; i++)
        {
            if (bunkerItemAmounts[i] != 0)
                inventoryDictionary.Add((InventoryItems.Items)i, bunkerItemAmounts[i]);
        }
    }
#endif
}
