using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{

    public GameObject cursor;
    public StageOrderGenerator stageOrderGen;
    Gun gun;
    PlayerStats playerStats;
    PlayerAbilities playerAbilities;
    DeathZone deathZone;
    PlayerMovement playerMovement;


    GameObject[] bullets;

    public Vector3 resetPosition;

    private void Awake()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        gun = FindObjectOfType<Gun>();
        deathZone = FindObjectOfType<DeathZone>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        playerAbilities = FindObjectOfType<PlayerAbilities>();
    }


    public void Reset()
    {
        stageOrderGen.Reset();
        gun.Reset();
        playerStats.Reset();
        deathZone.Reset();
        playerMovement.Reset();
        playerAbilities.Reset();

        bullets = GameObject.FindGameObjectsWithTag("Bullet");
        for(int i = 0; i < bullets.Length; i++)
        {
            Destroy(bullets[i].gameObject);
        }

    }


}
