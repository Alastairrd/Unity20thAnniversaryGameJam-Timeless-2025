using System;
using System.Collections.Generic;
using UnityEngine;

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
    public float insanity = 0;
    public float weight = 0;
    public LocationList.Locations currentLocation = LocationList.Locations.Bunker;

        #region Insanity
    //Instanity is practically unlock
    public void IncreaseInsanity(float amount) 
    {
        insanity += amount;
    }

    public void DecreaseInsanity(float amount)
    {
        Mathf.Clamp(insanity, 0, insanity -= amount);
    }

    public void ResetInsanity()  //when sleep
    {
        insanity = 0;
    }

    public void SetInsanity(float amount)
    {
        insanity = amount;
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
    #endregion

        #region Location

    public void SetLocation(LocationList.Locations location) 
    {
        currentLocation = location;
    }
    #endregion
    #endregion

    #region Inventory
    Dictionary<InventoryItems.Items, int> inventoryDictionary = new Dictionary<InventoryItems.Items, int>();
    Dictionary<InventoryItems.Items, int> bunkerDictionary = new Dictionary<InventoryItems.Items, int>();
    //HashSet<InventoryItems.Guns> gunsDictionary = new HashSet<InventoryItems.Guns>();

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

    /// <summary>
    /// This function does allow item to go to 0, which means 
    /// </summary>
    public void RemoveItem(InventoryItems.Items item, int amount)
    {
        if (inventoryDictionary.ContainsKey(item))
        {
            if (inventoryDictionary[item] <= inventoryDictionary[item] - amount)
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

    #endregion

    #region Player States
    public enum PlayerStates 
    {
        idle,

        crafting,
        building,

        consuming,

        getting,

        figthing,

        talking,

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
        health = maxHealth;
    }
   
    #endregion
}
