using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public GameObject[] rooms;
    public GameObject room;
    public GameObject roomClone;


    int random;

    private void Awake()
    {
        random = Random.Range(0, rooms.Length);
        room = rooms[random];
        roomClone = (GameObject)Instantiate(room, transform.position, transform.rotation);
    }
}
