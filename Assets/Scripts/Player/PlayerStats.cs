using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public HatObject[] hatObjects;
    public HatObject hatObject;
    //public GameObject[] hatPrefabs;
    //public GameObject hatPrefab;
    Manager manager;
    InputManager inputManager;
 
    private int random, random2;
    //public HealthBar healthBar;
    public int currentHealth;

    public Transform hatHolder;

    private bool inZone;

    public bool isP1;


    public void Awake()
    {
        manager = FindObjectOfType<Manager>();
        inputManager = FindObjectOfType<InputManager>();
        random = Random.Range(0, hatObjects.Length);
        hatObject = hatObjects[random];
        //random2 = Random.Range(0, hatPrefabs.Length);
        //hatPrefab = hatPrefabs[random2];

        currentHealth = hatObject.maxHealth;
        //healthBar.SetMaxHealth(maxHealth);

        InvokeRepeating("ZoneDamage", 0f, 1f);

        transform.localScale = hatObject.playerScale;

        //hatPrefab = Instantiate(hatPrefab, hatHolder.position, hatHolder.rotation);
        //hatPrefab.transform.parent = hatHolder;
    }


    public void Reset()
    {
        random = Random.Range(0, hatObjects.Length);
        hatObject = hatObjects[random];
        //random2 = Random.Range(0, hatPrefabs.Length);
        //hatPrefab = hatPrefabs[random2];

        currentHealth = hatObject.maxHealth;
        transform.localScale = hatObject.playerScale;
    }

    public void BO3Reset()
    {
        currentHealth = hatObject.maxHealth;
        transform.localScale = hatObject.playerScale;
    }

    void Update()
    {
        /*
        if (inputManager.spacePressed)
        {
            TakeDamage(150);
        }
        */
        if(currentHealth <= 0)
        {
            manager.Reset(isP1);
            currentHealth = hatObject.maxHealth;
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
