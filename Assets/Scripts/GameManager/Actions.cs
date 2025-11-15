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

    #region Action Function
    public void Bunker() // GO to bunker if not in it
    {
    
    }

    public void Sleep() //Sleep if in bunker
    {
    
    }

    public void Upgrade() //if in bunker, has materials for at least one item upgrade
    {

    }

    public void Craft() //if in bunker has materials for at least one item upgrade
    {

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

    }
#endregion
}
