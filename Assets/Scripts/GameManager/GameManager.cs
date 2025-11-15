using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    #region Singleton Pattern
    public static GameManager Instance { get; private set; }
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

    public int GlobalZombieDensity = 1;
    public List<Location> Locations;

    #region ProcessingPossibleChoices

    Queue<string> ProcessPossibleActions() 
    {
        Queue<string> queue = new Queue<string>();

        queue.Enqueue("scavange");
        
        if(Player.Instance.currentLocation != LocationList.Locations.Bunker) 
        {
            queue.Enqueue("bunker");
        }

        return queue;
    }

    #endregion

    #region Valid Input
    public void ValidInput(string input)
    {
        switch (input)
        {
            case "bunker":
                Bunker();
                break;

            case "sleep":
                Sleep();
                break;

            case "upgrade":
                Upgrade();
                break;

                case "craft":
                    Craft();
                    break;

                case "axe":
                    Axe();
                    break;

                case "gloves":
                    Gloves();
                    break;

                case "knife":
                    Knife();
                    break;

                case "picker":
                    Picker();
                    break;

                case "rod":
                    Rod();
                    break;

                case "wrench":
                    Wrench();
                    break;

                case "gun":
                    Gun();
                    break;

                case "bullet":
                    Bullet();
                    break;

            case "build":
                Build();
                break;

                case "walls":
                    Walls();
                    break;

                case "reinforcement":
                    Reinforcement();
                    break;

                case "traps":
                    Traps();
                    break;

                case "bed":
                    Bed();
                    break;

            case "consume":
                Consume();
                break;

                case "medicine":
                    Medicine();
                    break;

                case "bandade":
                    Bandade();
                    break;

                case "sushi":
                    Sushi();
                    break;

                case "meat":
                    Meat();
                    break;

                case "vegetable":
                    Vegetable();
                    break;

                case "water":
                    Water();
                    break;

            case "scavange":
                Scavange();
                break;

                case "yard":
                    Yard();
                    break;

                case "building":
                    Building();
                    break;

                case "city":
                    City();
                    break;

                case "river":
                    River();
                    break;

                case "forest":
                    Forest();
                    break;

                case "desert":
                    Desert();
                    break;

            case "get":
                Get();
                break;

                case "wood":
                    Wood();
                    break;

                case "metal":
                    Metal();
                    break;

                case "scraps":
                    Scraps();
                    break;

                case "fish":
                    Fish();
                    break;

                case "hunt":
                    Hunt();
                    break;

                case "pick":
                    Pick();
                    break;

            case "fight":
                Fight();
                break;

            case "flee":
                Flee();
                break;

            case "talk":
                Talk();
                break;

                case "buy":
                    Buy();
                    break;

                case "sell":
                    Sell();
                    break;

                case "trade":
                    Trade();
                    break;

            case "exit":
                Exit();
                break;

            default:
                return;
        }
    }

    #endregion

    #region Action Functions

    void Bunker() // GO to bunker if not in it
    {

    }

    void Sleep() //Sleep if in bunker
    {

    }

    void Upgrade() //if in bunker, has materials for at least one item upgrade
    {

    }

        void Craft() //if in bunker has materials for at least one item upgrade
        {

        }

        void Axe() //has materials for at least one item upgrade
        {

        }

        void Gloves() //
        {

        }

        void Knife()
        {

        }

        void Picker()
        {

        }

        void Rod()
        {

        }

        void Wrench() 
        {

        }

        void Gun()
        {

        }

        void Bullet() 
        {

        }

    void Build() 
    {

    }

        void Walls() 
        {

        }

        void Reinforcement()
        {

        }

        void Traps()    
        {

        }

        void Bed()      
        {

        }

    void Consume()  
    {

    }

        void Medicine() 
        {

        }

        void Bandade()  
        {

        }

        void Sushi()    
        {

        }

        void Meat()     
        {

        }

        void Vegetable()
        {

        }

        void Water()    
        {

        }

    void Scavange() 
    {

    }

    void Yard()     
    {

    }

    void Building() 
    {

    }

        void City()     
        {

        }

        void River()    
        {

        }
        void Forest()   
        {

        }

        void Desert()   
        {

        }

    void Get()      
    {

    }

        void Wood()     
        {

        }

        void Metal()    
        {

        }

        void Scraps()   
        {

        }

        void Fish()     
        {

        }

        void Hunt()     
        {

        }

        void Pick()     
        {

        }

    void Fight()    
    {

    }

    void Flee()     
    {

    }

    void Talk()     
    {

    }

        void Buy()      
        {

        }

        void Sell()     
        {

        }

        void Trade()    
        {

        }

    void Exit()     
    {

    }
    #endregion

    #region ProcessingPossibleChoices

    #endregion

}
