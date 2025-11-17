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

    public HashSet<string> AllPossibleActions = new HashSet<string>()
    {
            "bunker",
            "sleep",
            "upgrade", //same as craft, but of things you already have
            "craft",
                "axe",
                "gloves",
                "knife",
                "picker",
                "rod",
                "wrench",
                "gun",
                "bullet",
            "build",
                "walls",
                "reinforcement",
                "traps",
                "bed",
            "consume",
                "medicine",
                "bandade",
                "sushi",
                "meat",
                "vegetable",
                "water",
            "scavange",
                "yard",
                "building",
                "city",
                "river",
                "forest",
                "desert",
            "get",
                "wood",
                "metal",
                "scraps",
                "fish",
                "hunt",
                "pick",
                "water",
            "fight",
                "dash",
                "shoot",
                "attack",
            "flee", //goes to bunker
            "talk", //human interaction
                "buy",   //will offer a random item at a random cost dependant on sanity
                "sell",  //will offer a random price at a random 
                "trade", //will offer a random item for another random item
            "exit"
    };

    public HashSet<string> ConstantActions = new HashSet<string>
    {
        "help",    //prints all constant actions
        "settings",
        "up",
        "down",
        "faster",
        "slower",
        "actions", //possible actions
        "restart"
    };


    #region Transforming Actions
    public Queue<string> CreateQueueFromActions(List<string> possibleActions)
    {
        Queue<string> actionQueue = new Queue<string>();

        actionQueue.Enqueue("\n Please choose one of the following actions (type word or action): \n");
        for (int i = 0; i < possibleActions.Count; i++)
        {
            actionQueue.Enqueue((i + 1).ToString() + " " + possibleActions[i] + "   ");
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
        return CreateQueueFromActions(ProcessPossibleActions());
    }

    #endregion

    #region ProcessingPossibleChoices
    //we return a list because that way it can have numbers when we create queue from actions
    public List<string> ProcessPossibleActions()
    {
        List<string> list = new List<string>();

        if (Player.Instance.currentState == Player.PlayerStates.idle)
        {
            if (Player.Instance.currentLocation == LocationList.Locations.Bunker)
            {
                if ((GameManager.Instance.time > 19 && GameManager.Instance.time < 4) || Player.Instance.insanity > 16)
                    list.Add("sleep");
                if (Player.Instance.canCraftAnyItem())
                    list.Add("craft");
                if (Player.Instance.canUpgradeAnyItem())
                    list.Add("upgrade");
                if (Player.Instance.canBuildAnyItem())
                    list.Add("build");
            }
            else
            {
               list.Add("bunker");
            }
                
            list.Add("scavenge");

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
            if (Player.Instance.canUpgradeItem(InventoryItems.Items.gloves))
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

            if (list.Count == 0)
            {
                Debug.Log("No Actions");
            }

        return list;
    }
    #endregion


    #region Action Function
    public void Bunker() // GO to bunker if not in it
    {
    
    }

    public void Sleep() //Sleep if in bunker
    {
    
    }

    public void Upgrade() //if in bunker, has materials for at least one item upgrade
    {
        Player.Instance.currentState = Player.PlayerStates.upgrading;
    }

    public void Craft() //if in bunker has materials for at least one item upgrade
    {
        Player.Instance.currentState = Player.PlayerStates.crafting;
    }

        public void Axe() //has materials for at least one item upgrade
        {

        }

        public void Gloves() //
        {

        }

        public void Knife()
        {

        }

        public void Picker()
        {

        }

        public void Rod()
        {

        }

        public void Wrench()
        {

        }

        public void Gun()
        {

        }

        public void Bullet()
        {

        }

    public void Build()
    {
        Player.Instance.currentState = Player.PlayerStates.building;
    }

        public void Walls()
        {

        }

        public void Reinforcement()
        {

        }

        public void Traps()
        {

        }

        public void Bed()
        {

        }

    public void Consume()
    {
        Player.Instance.currentState = Player.PlayerStates.consuming;
    }

        public void Medicine()
        {

        }

        public void Bandade()
        {

        }

        public void Sushi()
        {

        }

        public void Meat()
        {

        }

        public void Vegetable()
        {

        }

        public void Water()
        {

        }

    public void Scavange()
    {
        Player.Instance.currentState = Player.PlayerStates.scavenging;
    }

        public void Yard()
        {

        }

        public void Building()
        {

        }

        public void City()
        {

        }

        public void River()
        {

        }
        public void Forest()
        {

        }

        public void Desert()
        {

        }

    public void Get()
    {

    }

        public void Wood()
        {

        }

        public void Metal()
        {

        }

        public void Scraps()
        {

        }

        public void Fish()
        {

        }

        public void Hunt()
        {

        }

        public void Pick()
        {

        }

    public void Fight()
    {

    }

        public void Shoot()
        {

        }

        public void Attack()
        {

        }

        public void Dash()
        {

        }

    public void Flee()
    {

    }

    public void Talk()
    {

    }

        public void Buy()
        {

        }

        public void Sell()
        {

        }

        public void Trade()
        {

        }

    public void Exit()
    {
        if(Player.Instance.currentState != Player.PlayerStates.getting)
            Player.Instance.currentState = Player.PlayerStates.idle;
        else
            Player.Instance.currentState = Player.PlayerStates.idle;
    }
#endregion
}
