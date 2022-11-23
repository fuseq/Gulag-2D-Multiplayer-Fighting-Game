using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private float health=1;
    private float maxHealth=100;
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 100;
        health = maxHealth;
    }

    
    void Update()
    {
        
    }

   public void damage(float dmg)
    {
        health = health - dmg;
        if (health<0)
        {
            health = 0;
        }
    }

   public void heal(float heal)
    {
        health = health + heal;
        if (health>maxHealth)
        {
            health = maxHealth;
        }
    }
   public float getHeal()
   {
       Debug.Log(health);
       return health;
   }
   
}
