using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{

    public HatObject hatObject;
    public Gun gun;
    public PlayerStats playerStats;
    Manager manager;

    public MeshRenderer mesh;
    public GameObject bubbleShield;

    InputManager inputManager;

    GameObject[] bullets;

    private float nextAbilityTime;

    void Start()
    {
        hatObject = playerStats.hatObject;
        manager = FindObjectOfType<Manager>();
        GetComponent<MeshRenderer>();
        inputManager = FindObjectOfType<InputManager>();
    }
    // Update is called once per frame
    void Update()
    {
        CheckAbilities();
        CheckCooldowns();
    }


    public void Reset()
    {
        hatObject = playerStats.hatObject;

    }

    void CheckCooldowns()
    {

    }
    void CheckAbilities()
    {

        switch (hatObject.abilityNumber)
        {
            default:
                break;
            case 1:
                if(Time.time > nextAbilityTime)
                {
                    if (inputManager.Interact)
                    {
                        mesh.enabled = false;
                        nextAbilityTime = Time.time + hatObject.abilityCooldown;
                        Invoke("resetAbility", hatObject.abilityDuration);
                    }
                }
                break;
            case 2:
                if(Time.time > nextAbilityTime)
                {
                    if (inputManager.Interact)
                    {
                        GameObject clone = (GameObject)Instantiate(bubbleShield, transform.position, transform.rotation);
                        nextAbilityTime = Time.time + hatObject.abilityCooldown;
                        Destroy(clone, hatObject.abilityDuration);
                    }
                }
                    break;
            case 3:
                if(Time.time > nextAbilityTime)
                {
                    if (inputManager.Interact)
                    {
                        bullets = GameObject.FindGameObjectsWithTag("Bullet");
                        for (int i = 0; i < bullets.Length; i++)
                        {
                            Destroy(bullets[i].gameObject);
                        }
                        nextAbilityTime = Time.time + hatObject.abilityCooldown;
                    }
                }
                break;
            case 4:
                if(Time.time > nextAbilityTime)
                {
                    if (inputManager.Interact)
                    {
                        transform.position = manager.cursor.transform.position;
                        nextAbilityTime = Time.time + hatObject.abilityCooldown;
                    }
                }
                break;

        }
            

    }

    public void resetAbility()
    {
        mesh.enabled = true;
    }
}
