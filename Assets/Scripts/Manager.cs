using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{

    public GameObject cursor;
    StageOrderGenerator stageOrderGen;
    Gun gun;
    PlayerStats playerStats;
    PlayerAbilities playerAbilities;
    DeathZone deathZone;
    PlayerMovement playerMovement;


    GameObject[] bullets;

    public Vector3 resetPosition;
    public int gameMode;
    int p1 ,p2;
    
    #region gamemodes
    //1 is arcade(BO1)
    //2 is best of 3
    //3 is gungame
    #endregion

    private void Awake()
    {
        stageOrderGen = FindObjectOfType<StageOrderGenerator>();
        playerStats = FindObjectOfType<PlayerStats>();
        gun = FindObjectOfType<Gun>();
        deathZone = FindObjectOfType<DeathZone>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        playerAbilities = FindObjectOfType<PlayerAbilities>();
    }

    public void Reset(bool p1Dead)
    {
        switch (gameMode)
        {
            default:
                break;
            case 1:
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

                return;
            case 2:
                if(p1Dead == true)
                {
                    p2 += 1;
                }
                else if(p1Dead == false)
                {
                    p1 += 1;
                }
                playerStats.BO3Reset();
                deathZone.Reset();
                playerMovement.BO3Reset();
                bullets = GameObject.FindGameObjectsWithTag("Bullet");
                for (int i = 0; i < bullets.Length; i++)
                {
                    Destroy(bullets[i].gameObject);
                }

                if (p1 == 2 || p2 == 2)
                {
                    Invoke("pointReset", 0f);
                }
                return;
            case 3:
                return;
        }
        

        

    }

    void pointReset()
    {
        p1 = 0;
        p2 = 0;
        stageOrderGen.Reset();
        gun.Reset();
        playerStats.Reset();
        deathZone.Reset();
        playerMovement.Reset();
        playerAbilities.Reset();
        bullets = GameObject.FindGameObjectsWithTag("Bullet");
        for (int i = 0; i < bullets.Length; i++)
        {
            Destroy(bullets[i].gameObject);
        }
    }
}
