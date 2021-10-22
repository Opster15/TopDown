using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public HatObject[] hatObjects;
    public HatObject hatObject;
    public Manager manager;
 
    private int random;
    //public HealthBar healthBar;
    public int currentHealth;

    private bool inZone;


    public void Awake()
    {

        random = Random.Range(0, hatObjects.Length);
        hatObject = hatObjects[random];

        currentHealth = hatObject.maxHealth;
        //healthBar.SetMaxHealth(maxHealth);

        InvokeRepeating("ZoneDamage", 0f, 1f);

        transform.localScale = hatObject.playerScale;



    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            TakeDamage(20);
        }

        if(currentHealth <= 0)
        {
            manager.Reset();
        }

    }
    
    public void TakeDamage(int damage)
        {
        currentHealth -= damage;
        }

    #region Zone

    void ZoneDamage()
    {
        if(inZone == false)
        {
            currentHealth -= 5;
        }
        
    }

    

    void OnTriggerExit(Collider col)
    { 
    if (col.tag == "DeathCube")
        {
            inZone = false;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "DeathCube")
        {
            inZone = true;
        }
    }

    #endregion
}
