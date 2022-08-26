using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Creature : MonoBehaviour
{
    protected bool changedDesire = true;

    protected float update_desire = 2;
    protected float update_desire_count;
    //PROFILE
    protected string my_name = "Meowzy";
    protected string current_desire = "No Desire";
    [SerializeField]
    protected float hunger_state = 100;
    [SerializeField]
    public float thirst_state = 100;

    //ENVIRONMENT
    protected string my_biome = "Grass";
    protected Vector2 home_location;

    public Creature(Vector2 home_location)
    {
        this.home_location = home_location;


    }
    public void Move()
    {
        if(current_desire != "No Desire")
        {
            SearchForSomething(current_desire);
        }
        else
        {
            WanderAround();
        }
    }
    public abstract void SearchForSomething(string desire);
    public abstract void WanderAround();
    public void UpdateCurrentDesire()
    {
        string currentDesire = current_desire;
        if (hunger_state > 50 && thirst_state > 50)                 {current_desire = "No Desire";}
        else if(hunger_state < 50 && hunger_state < thirst_state)  { current_desire = "Food";    }
        else if (thirst_state < 50 && thirst_state < hunger_state) { current_desire = "Water";   }
        if(currentDesire != current_desire)
        {
            changedDesire = true;
        }
   
    }






}
