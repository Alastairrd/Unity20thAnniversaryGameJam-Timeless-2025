using System;
using System.Collections.Generic;
using UnityEngine;

public class Actions : MonoBehaviour
{
    #region Singleton Pattern
    public static Actions Instance { get; private set; }
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

    public enum AllPossibleActionsEnum
    {
        bunker,
            sleep,
            upgrade, //same as craft, but of things you already have
            craft,
                axe,
                gloves,
                knife,
                picker,
                rod,
                wrench,
                gun,
                bullet,
            build,
                walls,
                reinforcement,
                traps,
                bed,
            consume,
                medicine,
                bandade,
                sushi,
                meat,
                vegetable,
                water,
            scavange,
                yard,
                building,
                city,
                river,
                forest,
                desert,
            get,
                wood,
                metal,
                scraps,
                fish,
                hunt,
                pick,
            fight,
                dash,
                shoot,
                attack,
            flee, //goes to bunker
            talk, //human interaction
                buy,   //will offer a random item at a random cost dependant on sanity
                sell,  //will offer a random price at a random 
                trade, //will offer a random item for another random item
            exit
    };
    public HashSet<string> AllPossibleActions()
    {
        HashSet<string> actionsHash = new HashSet<string>();

        foreach (string action in Enum.GetNames(typeof(AllPossibleActionsEnum))) 
        {
            actionsHash.Add(action);
        }

        return actionsHash;
    }
    public enum ConstantActionsEnum
    {
        help,    //prints all constant actions
        settings,
        up,   
        down,
        faster,
        slower,
        actions, //possible actions
        restart
    }
    public HashSet<string> AllConstantActions()
    {
        HashSet<string> actionsHash = new HashSet<string>();

        foreach (string action in Enum.GetNames(typeof(ConstantActionsEnum)))
        {
            actionsHash.Add(action);
        }

        return actionsHash;
    }


    #region Transforming Actions
    public Queue<string> CreateQueueFromAListOfActions(List<string> possibleActions)
    {
        Queue<string> actionQueue = new Queue<string>();

        actionQueue.Enqueue("PLEASE CHOOSE ONE OF THE FOLLOWING ACTIONS BY TYPING: \n");
        for (int i = 0; i < possibleActions.Count; i++)
        {
            actionQueue.Enqueue(/*(i + 1).ToString() + " "+ */possibleActions[i] + "   ");
        }
        actionQueue.Enqueue("\n");

        return actionQueue;
    }

    public HashSet<string> HashFromListOfPossibleActions(List<string> ProccessedActions) 
    {
        HashSet<string> hash = new HashSet<string>();

        foreach (string action in ProccessedActions)
        {
            hash.Add(action);
        }

        return hash; 
    }

    public Queue<string> CreateQueueFromProcessedActions()
    {
        return CreateQueueFromAListOfActions(ProcessPossibleActions());
    }

    #endregion

    #region ProcessingPossibleChoices
    //we return a list because that way it can have numbers when we create queue from actions
    public List<string> ProcessPossibleActions()
    {
        List<string> list = new List<string>();

        if (Player.Instance.currentState == Player.PlayerStates.idle)
        {
            if (Player.Instance.currentLocation == LocationList.Locations.bunker)
            {
                if ((GameManager.Instance.time > 19 && GameManager.Instance.time < 4) || Player.Instance.tiredness > 16)
                    list.Add("sleep");
                if (Player.Instance.canCraftAnyItem())
                    list.Add("craft");
                if (Player.Instance.canUpgradeAnyItem())
                    list.Add("upgrade");
                if (Player.Instance.canBuildAnyItem())
                    list.Add("build");
            }
                
            list.Add("travel");

            if(Player.Instance.canCraftAnyItem())
                list.Add("consume");
        }

        if (Player.Instance.currentState == Player.PlayerStates.crafting)
        {
            if (Player.Instance.canCraftItem(InventoryItems.Items.axe))
                list.Add("axe");
            if (Player.Instance.canCraftItem(InventoryItems.Items.wrench))
                list.Add("wrench");
            if (Player.Instance.canCraftItem(InventoryItems.Items.knife))
                list.Add("knife");
            if (Player.Instance.canCraftItem(InventoryItems.Items.gloves))
                list.Add("gloves");
            if (Player.Instance.canCraftItem(InventoryItems.Items.gloves))
                list.Add("rod");
            if (Player.Instance.canCraftItem(InventoryItems.Items.gun))
                list.Add("gun");
            if (Player.Instance.canCraftItem(InventoryItems.Items.bullet))
                list.Add("bullet");
        }

        if (Player.Instance.currentState == Player.PlayerStates.upgrading)
        {
            if (Player.Instance.canUpgradeItem(InventoryItems.Items.axe))
                list.Add("axe");
            if (Player.Instance.canUpgradeItem(InventoryItems.Items.wrench))
                list.Add("wrench");
            if (Player.Instance.canUpgradeItem(InventoryItems.Items.knife))
                list.Add("knife");
            if (Player.Instance.canUpgradeItem(InventoryItems.Items.gloves))
                list.Add("gloves");
            if (Player.Instance.canUpgradeItem(InventoryItems.Items.rod))
                list.Add("rod");
        }

        if (Player.Instance.currentState == Player.PlayerStates.consuming) 
        {
            if (Player.Instance.canConsumeItem(InventoryItems.Items.water))
                list.Add("water");
            if (Player.Instance.canConsumeItem(InventoryItems.Items.fish))
                list.Add("sushi");
            if (Player.Instance.canConsumeItem(InventoryItems.Items.meat))
                list.Add("meat");
            if (Player.Instance.canConsumeItem(InventoryItems.Items.vegetables))
                list.Add("vegetables");
            if (Player.Instance.canConsumeItem(InventoryItems.Items.medicine))
                list.Add("medicine");
            if (Player.Instance.canConsumeItem(InventoryItems.Items.bandade))
                list.Add("bandade");

        }

        if (Player.Instance.currentState == Player.PlayerStates.travelling)
        {
            foreach (Location location in LocationManager.Instance.Locations)
            {
                if(location.location != Player.Instance.currentLocation)
                    list.Add(location.location.ToString());
            }
        }

        if (Player.Instance.currentState == Player.PlayerStates.getting)
        {
            foreach (string gettable in LocationManager.Instance.possibleThingsToGetHere()) 
            {
                list.Add("get");
            }
        }

        if (Player.Instance.currentState == Player.PlayerStates.talking)
        {
            list.Add("buy");
            list.Add("sell");
            list.Add("trade");
        }

        if (Player.Instance.currentState == Player.PlayerStates.figthing)
        {
            list.Add("shoot");
            list.Add("dash");
            list.Add("flee");
        }

        if (list.Count == 0)
            {
                Debug.Log("No Actions");
            }

        return list;
    }
    #endregion

    #region Action Functions

        #region Sleep
    public void Sleep() //Sleep if in bunker
    {
    
    }
    #endregion

        #region Upgrading
    public void Upgrade() //if in bunker, has materials for at least one item upgrade
    {
        Player.Instance.currentState = Player.PlayerStates.upgrading;
    }

        public void UpgradeItem(InventoryItems.Items item) 
        {

        }
        #endregion

        #region Crafting
    public void Craft() //if in bunker has materials for at least one item upgrade
    {
        Player.Instance.currentState = Player.PlayerStates.crafting;
    }
        public void CraftItem(InventoryItems.Items item)
        {

        }
    #endregion

        #region Build
    public void Build()
    {
        Player.Instance.currentState = Player.PlayerStates.building;
    }

        public void BuildItem(InventoryItems.BunkerItems item)
        {

        }
    #endregion

        #region Consume
    public void Consume()
    {
        Player.Instance.currentState = Player.PlayerStates.consuming;
    }

        public void ConsumeItem(InventoryItems.Items item)
        {

        }
    #endregion

        #region Travelling
    public void Travel()
    {
        Player.Instance.currentState = Player.PlayerStates.travelling;
    }

        public void TravelTo(LocationList.Locations location)
        {
            Player.Instance.currentState = Player.PlayerStates.travelling;
        }
        #endregion

        #region Get
    public void Get()
    {
        Player.Instance.currentState = Player.PlayerStates.getting;
    }

        public void GetItem(InventoryItems.Items item)
        {

        }
        #endregion

        #region Figthing
    public void Fight()
    {
        Player.Instance.currentState = Player.PlayerStates.talking;
    }

        public void Shoot()
        {

        }

        public void Dash()
        {

        }

        public void Attack()
        {

        }
    #endregion

    public void Flee()
    {

    }

    #region Talkgin
    public void Talk()
    {
        Player.Instance.currentState = Player.PlayerStates.talking;
    }

        public void Buy() 
        {

        }

        public void Trade()
        {

        }

        public void Sell()
        {

        }

    #endregion
    public void Exit()
    {
            Player.Instance.currentState = Player.PlayerStates.idle;
    }
#endregion
}
