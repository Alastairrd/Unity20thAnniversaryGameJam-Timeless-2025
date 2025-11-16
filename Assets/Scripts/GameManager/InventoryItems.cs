using System.Collections.Generic;
using UnityEngine;

public class InventoryItems
{
    public enum Items 
    {
        //materials for crafting and building
        wood,       
        metal,
        scraps,
        leather,

        //utility
        axe,        //wood
        gloves,     //scrap
        knife,      //leather
        picker,     //vegtebale
        rod,        //fish
        wrench,     //metal
        gun,       //the more better hit accuracy, and meat
        bullet,    //needed per 1 attack

        //consumables
        water,      //heals 10
        vegetables, //heals 5
        meat,       //heals 5 but takes time
        fish,       //but catches plenty and takes time
        //you have to find these
        medicine, //heals compeletly
        bandade, //changes maz health
    }

    public enum BunkerItems
    {
        //materials for crafting and building
        wood,
        metal,
        scraps,
        leather,

        bed,
        walls,
        reinforcement,
        traps,
    }

    // one compact struct for your 4 materials
    public struct Recipe
    {
        public int wood;
        public int metal;
        public int scraps;
        public int leather;

        public Recipe(int wood, int metal, int scraps, int leather)
        {
            this.wood = wood;
            this.metal = metal;
            this.scraps = scraps;
            this.leather = leather;
        }
    }

    // static recipe table
    public static readonly Dictionary<Items, Recipe> UtilityRecipes =
        new Dictionary<Items, Recipe>
        {
            // utilities
            { Items.axe,    new Recipe(wood: 3, metal: 1, scraps: 0, leather: 0) },
            { Items.gloves, new Recipe(wood: 0, metal: 0, scraps: 2, leather: 1) },
            { Items.knife,  new Recipe(wood: 1, metal: 1, scraps: 0, leather: 1) },
            { Items.picker, new Recipe(wood: 2, metal: 0, scraps: 1, leather: 0) },
            { Items.rod,    new Recipe(wood: 2, metal: 1, scraps: 3, leather: 0) },
            { Items.wrench, new Recipe(wood: 0, metal: 3, scraps: 1, leather: 0) },
            { Items.gun,    new Recipe(wood: 1, metal: 5, scraps: 4, leather: 2) }, // more guns == more accuracy
            { Items.bullet, new Recipe(wood: 0, metal: 1, scraps: 1, leather: 0) }, //attacking uses bullets
        };

    public static readonly Dictionary<Items, Recipe> UtilityUpgrades =
        new Dictionary<Items, Recipe>
        {
            // utilities
            { Items.axe,    new Recipe(wood: 2, metal: 1, scraps: 0, leather: 0) },
            { Items.gloves, new Recipe(wood: 0, metal: 0, scraps: 1, leather: 1) },
            { Items.knife,  new Recipe(wood: 1, metal: 1, scraps: 0, leather: 1) },
            { Items.picker, new Recipe(wood: 2, metal: 0, scraps: 1, leather: 0) },
            { Items.rod,    new Recipe(wood: 2, metal: 1, scraps: 0, leather: 0) },
            { Items.wrench, new Recipe(wood: 0, metal: 3, scraps: 1, leather: 0) },
        };

    public static readonly Dictionary<BunkerItems, Recipe> BuildingRecipes =
        new Dictionary<BunkerItems, Recipe>
        {
            // utilities
            { BunkerItems.bed,    new Recipe(wood: 10, metal: 1, scraps: 5, leather: 10) },       //restores more sanity
            { BunkerItems.traps, new Recipe(wood: 0, metal: 2, scraps: 5, leather: 1) },          //strenght 
            { BunkerItems.walls,  new Recipe(wood: 1, metal: 1, scraps: 0, leather: 1) },         // heals
            { BunkerItems.reinforcement, new Recipe(wood: 0, metal: 10, scraps: 1, leather: 0) }, // increases MaxHP
        };


}

#region possible gun thingy
/*
   //Maybe can be used at some point for narrative choices, (doesnt impact gameplet)
   public enum Guns 
   {
       sniper,
       shotgun,
       rifle,
       pistol
   }
   */
#endregion