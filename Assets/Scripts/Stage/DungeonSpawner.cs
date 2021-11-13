using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonSpawner : MonoBehaviour
{
    public GameObject[] doors;
    public GameObject[] rooms;
    public GameObject[] corris;
    public GameObject room;
    public GameObject corri;
    public GameObject roomClone;
    public GameObject corriClone;

    int random,random2;

    private void Awake()
    {
        random = Random.Range(0, rooms.Length);
        room = rooms[random];
        roomClone = (GameObject)Instantiate(room, transform.position, transform.rotation);
        Invoke("DoorCheck", 0f);
    }


    void DoorCheck()
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
