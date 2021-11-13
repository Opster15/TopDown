using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorriSpawner : MonoBehaviour
{
    public GameObject[] doors;
    public GameObject[] corris;
    public GameObject corri;
    public GameObject corriClone;

    int random, random2;

    public void Awake()
    {
        doors = GameObject.FindGameObjectsWithTag("Door");

        foreach (GameObject door in doors)
        {
            random2 = Random.Range(0, corris.Length);
            corri = corris[random2];
            corriClone = (GameObject)Instantiate(corri, door.transform.position, door.transform.rotation);
            corriClone.transform.parent = door.transform;
        }
        
    }
}
