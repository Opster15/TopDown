using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{

    public HatObject hatObject;
    public Gun gun;
    public PlayerStats playerStats;

    public MeshRenderer mesh;
    public GameObject bubbleShield;


    private float nextAbilityTime;

    void Start()
    {
        hatObject = playerStats.hatObject;
        GetComponent<MeshRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        CheckAbilities();

        CheckCooldowns();

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
                if(gun.shooting == true)
                {
                    mesh.enabled = true;
                }
                else if (gun.shooting == false)
                {
                    mesh.enabled = false;
                }
                break;
            case 2:
                if(Time.time > nextAbilityTime)
                {
                    if (Input.GetKey("e"))
                    {
                        GameObject clone = (GameObject)Instantiate(bubbleShield, transform.position, transform.rotation);
                        nextAbilityTime = Time.time + hatObject.abilityCooldown;
                        Destroy(clone, hatObject.abilityDuration);
                    }
                }
                    break;

        }
            

    }
}
