using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class Player : MonoBehaviour
{
    #region Singleton Pattern
    public static Player Instance { get; private set; }
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

    #region All Health
    public float health = 100;

    public float bunkerHP = 100;

    public float maxHealth = 100;

    public float maxHealthIncrements = 10;

    public int numberOfWounds = 0;

        #region Health
    public void SetHealth(float amount) 
    {
        health = amount;
    }
    public void TakeDamage(float amount) 
    {
        health -= amount;
        OnHealthChange();
    }
    public void TakeHeal(float amount)
    {
        health += amount;
    }
        #endregion

        #region Max Health
    public void SetMaxHealth(float amount) 
    {
        maxHealth = amount;
        if(health > maxHealth) 
        {
            health = maxHealth;
        }
    }
    public void IncreaseMaxHealth(float amount) 
    {
        if(amount == 0) 
        {
            amount = maxHealthIncrements;
        }
        maxHealth += amount;

    }
    public void DecreaseMaxHealth(float amount)
    {
        if (amount == 0)
        {
            amount = maxHealthIncrements;
        }

        maxHealth -= amount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }

        OnHealthChange();
    }
    #endregion

        #region Max Health increments
    //probably never used, but just in case
    public void SetMaxHealthIncrements(float amount)
    {
        maxHealthIncrements = amount;
    }
    public void IncreaseMaxHealthIncrements(float amount)
    {
                
        maxHealthIncrements += amount;

    }
    public void DecreaseMaxHealthIncrements(float amount)
    {
        maxHealthIncrements -= amount;
        if(maxHealth < 0) 
        {
            maxHealthIncrements = 0;
        }
    }
        #endregion

        #region Wounds
    public void IncerementWounds(int amount) 
    {
        numberOfWounds += amount;
        DecreaseMaxHealth(maxHealthIncrements);
    }
    public void DecrementWounds(int amount)
    {
        numberOfWounds += amount;
        DecreaseMaxHealth(maxHealthIncrements);
    }
    public void SetWounds(int amount)
    {
        numberOfWounds = amount;
        for(int i = 0; i < numberOfWounds; i++) 
        {
            DecreaseMaxHealth(maxHealthIncrements);
        }
    }
    #endregion

        #region Death
    void OnHealthChange() 
    {
        if(health <= 0) 
        {
            PlayerDied();
        }
    }

    void PlayerDied() 
    {
        UIManager.Instance.PrintMessage("You grasp for one more breath and say - Nothing is in vain: \n 0. restart");
    }
        #endregion

    #endregion

    #region Miscelanious
    public float tiredness = 0;
    public float weight = 0;
    public LocationList.Locations currentLocation = LocationList.Locations.bunker;

        #region tiredness
    //Instanity is practically unlock
    public void IncreaseInsanity(float amount) 
    {
        tiredness += amount;
    }

    public void DecreaseInsanity(float amount)
    {
        Mathf.Clamp(tiredness, 0, tiredness -= amount);
    }

    public void ResetInsanity()  //when sleep
    {
        tiredness = 0;
    }

    public void SetInsanity(float amount)
    {
        tiredness = amount;
    }
        #endregion

        #region Weight
    //Weight is practically speed
    public void IncreaseWeight(float amount)
    {
        weight += amount;
    }

    public void DecreaseWeight(float amount)
    {
        Mathf.Clamp(weight, 0, weight -= amount);
    }

    public void ResetWeight()
    {
        weight = 0;
    }

    public void SetWeight(float amount)
    {
        weight = amount;
    }

    public void CalculateAndUpdateWeight() 
    {
        weight += (inventoryDictionary[InventoryItems.Items.wood]);
        weight += (inventoryDictionary[InventoryItems.Items.metal]);
        weight += (inventoryDictionary[InventoryItems.Items.scraps]);
        weight += (inventoryDictionary[InventoryItems.Items.leather]);
    }

    #endregion

        #region Location

    public void SetLocation(LocationList.Locations location) 
    {
        currentLocation = location;
    }
    #endregion
    #endregion

    #region Inventory

    public void DumpResourcesIntoBunker()
    {
        bunkerDictionary[InventoryItems.BunkerItems.wood] += inventoryDictionary[InventoryItems.Items.wood];
        bunkerDictionary[InventoryItems.BunkerItems.metal] += inventoryDictionary[InventoryItems.Items.metal];
        bunkerDictionary[InventoryItems.BunkerItems.scraps] += inventoryDictionary[InventoryItems.Items.scraps];
        bunkerDictionary[InventoryItems.BunkerItems.leather] += inventoryDictionary[InventoryItems.Items.leather];

        inventoryDictionary[InventoryItems.Items.leather] = 0;
        inventoryDictionary[InventoryItems.Items.leather] = 0;
        inventoryDictionary[InventoryItems.Items.leather] = 0;
        inventoryDictionary[InventoryItems.Items.leather] = 0;

        ResetWeight();
    }

        #region Playter Inventory
    public Dictionary<InventoryItems.Items, int> inventoryDictionary = new Dictionary<InventoryItems.Items, int>();

    public int ReadItemAmount(InventoryItems.Items item)
    {
        if (inventoryDictionary.ContainsKey(item))
            return inventoryDictionary[item];
        else
            Debug.LogWarning(item + ": does not exist within item");
        return 0;
    }
            
    public void AddItem(InventoryItems.Items item, int amount)
    {
        if (inventoryDictionary.ContainsKey(item)) 
        {
            if (inventoryDictionary[item] == 0) 
            {
                //Added new item event
            }

            inventoryDictionary[item] += 1;
        }
        else 
        {
            Debug.LogWarning(item + ": does not exist within item");
        }
    }

    public void RemoveItem(InventoryItems.Items item, int amount)
    {
        if (inventoryDictionary.ContainsKey(item))
        {
            if (inventoryDictionary[item] - amount <= 0)
            {
                //Out of item
                Debug.Log("You went past");
                inventoryDictionary[item] = 0;
            }
            else 
            {
                inventoryDictionary[item] -= amount;
            }
        }
        else
        {
            Debug.LogWarning(item + " does not exist");
        }
    }

    public void SetItem(InventoryItems.Items item, int amount) 
    {
        if (inventoryDictionary.ContainsKey(item))
            inventoryDictionary[item] = amount;
        else
            Debug.LogWarning(item + " does not exist");
    }

    public void SetInventory(Dictionary<InventoryItems.Items, int> newInventoryDictionary) 
    {
        inventoryDictionary = newInventoryDictionary;
    }

    public void CreateInventory() 
    {
        foreach (InventoryItems.Items item in Enum.GetValues(typeof(InventoryItems.Items)))
        {
            inventoryDictionary.Add(item, 0);
        }
    }

                #region Crafting 
    public bool canCraftItem(InventoryItems.Items item)
    {
        if (ReadItemAmount(item) == 0 || (item == InventoryItems.Items.gun || item == InventoryItems.Items.bullet))
        {
            if (
               (ReadBunkerItemAmount(InventoryItems.BunkerItems.wood) > InventoryItems.UtilityRecipes[item].wood)
            && (ReadBunkerItemAmount(InventoryItems.BunkerItems.metal) > InventoryItems.UtilityRecipes[item].metal)
            && (ReadBunkerItemAmount(InventoryItems.BunkerItems.scraps) > InventoryItems.UtilityRecipes[item].scraps)
            && (ReadBunkerItemAmount(InventoryItems.BunkerItems.leather) > InventoryItems.UtilityRecipes[item].leather)
               )
                return true;
        }
        return false;

    }

    public bool canCraftAnyItem() //needs testing
    {
        foreach (InventoryItems.Items item in InventoryItems.UtilityRecipes.Keys)
        {
            if (canCraftItem(item))
                return true;
        }
        return false;
    }
    #endregion

                #region Upgrade
    public bool canUpgradeItem(InventoryItems.Items item)
    {
        if (ReadItemAmount(item) > 0)
        {
            if (
               (ReadBunkerItemAmount(InventoryItems.BunkerItems.wood) > InventoryItems.UtilityUpgrades[item].wood)
            && (ReadBunkerItemAmount(InventoryItems.BunkerItems.metal) > InventoryItems.UtilityUpgrades[item].metal)
            && (ReadBunkerItemAmount(InventoryItems.BunkerItems.scraps) > InventoryItems.UtilityUpgrades[item].scraps)
            && (ReadBunkerItemAmount(InventoryItems.BunkerItems.leather) > InventoryItems.UtilityUpgrades[item].leather)
               )
                return true;
        }
        return false;

    }

    public bool canUpgradeAnyItem() //needs testing
    {
        foreach (InventoryItems.Items item in InventoryItems.UtilityUpgrades.Keys)
        {
            if (canUpgradeItem(item))
                return true;
        }
        return false;
    }

    #endregion

                #region Consume
    public bool canConsumeItem(InventoryItems.Items item)
    {
        if (ReadItemAmount(item) > 0)
                return true;
        return false;

    }

    public bool canConsumeAnyItem() //needs testing
    {
        foreach (InventoryItems.Items item in InventoryItems.ConsumableItems)
        {
            if (canConsumeItem(item))
                return true;
        }
        return false;
    }

    #endregion

        #region BunkerInventory
    public Dictionary<InventoryItems.BunkerItems, int> bunkerDictionary = new Dictionary<InventoryItems.BunkerItems, int>();
    public int ReadBunkerItemAmount(InventoryItems.BunkerItems item)
    {
        if (bunkerDictionary.ContainsKey(item))
            return bunkerDictionary[item];
        else
            Debug.LogWarning(item + ": does not exist within item");
        return 0;
    }

    public void AddBunkerItem(InventoryItems.BunkerItems item, int amount)
    {
        if (bunkerDictionary.ContainsKey(item))
        {
            if (bunkerDictionary[item] == 0)
            {
                //Added new item event
                //maybe print the fact? or achievement or narrrative bool?
            }

            bunkerDictionary[item] += 1;
        }
        else
        {
            Debug.LogWarning(item + ": does not exist within item");
        }
    }

    public void RemoveBunkerItem(InventoryItems.BunkerItems item, int amount)
    {
        if (bunkerDictionary.ContainsKey(item))
        {
            if (bunkerDictionary[item] - amount <= 0)
            {
                //Out of item
                Debug.LogWarning("You went past");
                bunkerDictionary[item] = 0;
            }
            else
            {
                bunkerDictionary[item] -= amount;
            }
        }
        else
        {
            Debug.LogWarning(item + " does not exist");
        }
    }

    public void SetBunkerItem(InventoryItems.BunkerItems item, int amount)
    {
        if (bunkerDictionary.ContainsKey(item))
            bunkerDictionary[item] = amount;
        else
            Debug.LogWarning(item + " does not exist");
    }

    public void SetBunkerInventory(Dictionary<InventoryItems.BunkerItems, int> newBunkerInventoryDictionary)
    {
        bunkerDictionary = newBunkerInventoryDictionary;
    }
    public void CreateBunkerInventory()
    {
        foreach (InventoryItems.BunkerItems item in Enum.GetValues(typeof(InventoryItems.BunkerItems)))
        {
            bunkerDictionary.Add(item, 0);
        }
    }
            #region Build
    public bool canBuildItem(InventoryItems.BunkerItems item)
    {
        if (ReadBunkerItemAmount(item) == 0)
        {
            if (
               (ReadBunkerItemAmount(InventoryItems.BunkerItems.wood)     > InventoryItems.BuildingRecipes[item].wood)
            && (ReadBunkerItemAmount(InventoryItems.BunkerItems.metal)    > InventoryItems.BuildingRecipes[item].metal)
            && (ReadBunkerItemAmount(InventoryItems.BunkerItems.scraps)   > InventoryItems.BuildingRecipes[item].scraps)
            && (ReadBunkerItemAmount(InventoryItems.BunkerItems.leather)  > InventoryItems.BuildingRecipes[item].leather)
               )
                return true;
        }
        return false;

    }

    public bool canBuildAnyItem() //needs testing
    {
        bool canBuildItemSoFar = false;
        foreach (InventoryItems.BunkerItems item in InventoryItems.BuildingRecipes.Keys)
        {
            if (canBuildItem(item))
                canBuildItemSoFar = true;
            else
                return false;
        }
        return canBuildItemSoFar;
    }
    #endregion
    #endregion
    #endregion

    #endregion

    #region Player States
    public enum PlayerStates 
    {
        idle,

        upgrading,
        crafting,
        building,

        consuming,

        travelling,
            getting,

        figthing, //if we wanna add dodge and slash actions
        talking,

        surviving, ///maybe for waves

    }

    public PlayerStates currentState = PlayerStates.idle;

    public void SetState(PlayerStates state) 
    {
        currentState = state;
    }

    #endregion

    #region Player Settings
    public PlayerSettings PlayerSettings;

    void SetPlayerSettings() 
    {
        SetHealth(PlayerSettings.health);
        SetMaxHealth(PlayerSettings.maxHealth);
        SetMaxHealthIncrements(PlayerSettings.maxHealthIncrements);
        SetWounds(PlayerSettings.numberOfWounds);
        SetState(PlayerSettings.playerState);
        SetInventory(PlayerSettings.inventoryDictionary);
        SetLocation(PlayerSettings.location);
    }
    #endregion

    #region Start 
    private void Start()
    {
        health = maxHealth;
        if(PlayerSettings != null)
        {
            SetPlayerSettings();
        }
        else 
        {
            CreateInventory();
            CreateBunkerInventory();
        }
        health = maxHealth;
    }
   
    #endregion
}
