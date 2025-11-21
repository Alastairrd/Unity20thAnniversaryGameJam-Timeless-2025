using System.Collections.Generic;
using UnityEditor.Sprites;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using static UnityEditor.IMGUI.Controls.PrimitiveBoundsHandle;
using static UnityEngine.Rendering.DebugUI.Table;

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

    public static readonly HashSet<Items> ResourceItems = new HashSet<Items>()
    {
        Items.wood,
        Items.metal,
        Items.scraps,
        Items.leather
    };

    public static readonly HashSet<Items> ConsumableItems = new HashSet<Items>() 
    {
        Items.water,      //heals 10
        Items.vegetables, //heals 5
        Items.meat,       //heals 5 but takes time
        Items.fish,       //but catches plenty and takes time
        Items.medicine, //heals compeletly
        Items.bandade, //changes maz health
    };

    public static readonly HashSet<Items> CraftableItems = new HashSet<Items>()
    {
        Items.axe,        //wood
        Items.gloves,     //scrap
        Items.knife,      //leather
        Items.picker,     //vegtebale
        Items.rod,        //fish
        Items.wrench,     //metal
        Items.gun,       //the more better hit accuracy, and meat
        Items.bullet,
    };

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

    // takes little itmes
    public static readonly Dictionary<Items, Recipe> UtilityRecipes =
        new Dictionary<Items, Recipe>
        {
            // utilities
            { Items.axe,    new Recipe(wood: 1, metal: 1, scraps: 0, leather: 1) },
            { Items.gloves, new Recipe(wood: 0, metal: 0, scraps: 1, leather: 1) },
            { Items.knife,  new Recipe(wood: 1, metal: 1, scraps: 0, leather: 1) },
            { Items.picker, new Recipe(wood: 1, metal: 0, scraps: 1, leather: 0) },
            { Items.rod,    new Recipe(wood: 1, metal: 1, scraps: 1, leather: 0) },
            { Items.wrench, new Recipe(wood: 0, metal: 1, scraps: 1, leather: 0) },
            { Items.gun,    new Recipe(wood: 1, metal: 2, scraps: 2, leather: 1) }, // more guns == more accuracy
            { Items.bullet, new Recipe(wood: 0, metal: 1, scraps: 1, leather: 0) }, //attacking uses bullets
        };

    //takes more items
    public static readonly Dictionary<Items, Recipe> UtilityUpgrades =
        new Dictionary<Items, Recipe>
        {
            // utilities
            { Items.axe,    new Recipe(wood: 2, metal: 1, scraps: 0, leather: 0) },
            { Items.gloves, new Recipe(wood: 0, metal: 0, scraps: 1, leather: 1) },
            { Items.knife,  new Recipe(wood: 1, metal: 1, scraps: 0, leather: 1) },
            { Items.picker, new Recipe(wood: 2, metal: 0, scraps: 1, leather: 0) },
            { Items.rod,    new Recipe(wood: 0, metal: 1, scraps: 2, leather: 0) },
            { Items.wrench, new Recipe(wood: 0, metal: 3, scraps: 1, leather: 0) },
        };

    //takes lots of items
    public static readonly Dictionary<BunkerItems, Recipe> BuildingRecipes =
        new Dictionary<BunkerItems, Recipe>
        {
            // utilities
            { BunkerItems.bed,           new Recipe(wood: 10, metal: 1, scraps: 5, leather: 10) },       //restores more sanity
            { BunkerItems.traps,         new Recipe(wood: 0, metal: 3, scraps: 3, leather: 1) },          //strenght 
            { BunkerItems.walls,         new Recipe(wood: 2, metal: 2, scraps: 0, leather: 2) },         // heals
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