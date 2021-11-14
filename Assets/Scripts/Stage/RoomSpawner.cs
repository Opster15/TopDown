using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    private RoomTemplates templates;
    
    public int openingDirection;
    //1 = bottom
    //2 = top
    //3 = left
    //4 = right
    private int random;
    private bool spawned = false;

    void Start()
    {
        templates = FindObjectOfType<RoomTemplates>();
        Invoke("Spawn", 2f);
    }

    void Spawn()
    {
        if(spawned == false)
        {
            if (openingDirection == 1)
            {
                random = Random.Range(0, templates.bRooms.Length);
                Instantiate(templates.bRooms[random], transform.position, templates.bRooms[random].transform.rotation);
            }
            else if (openingDirection == 2)
            {
                random = Random.Range(0, templates.bRooms.Length);
                Instantiate(templates.tRooms[random], transform.position, templates.tRooms[random].transform.rotation);
            }
            else if (openingDirection == 3)
            {
                random = Random.Range(0, templates.bRooms.Length);
                Instantiate(templates.lRooms[random], transform.position, templates.lRooms[random].transform.rotation);
            }
            else if (openingDirection == 4)
            {
                random = Random.Range(0, templates.bRooms.Length);
                Instantiate(templates.rRooms[random], transform.position, templates.rRooms[random].transform.rotation);
            }
            spawned = true;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SpawnPoint"))
        {
            if(other.GetComponent<RoomSpawner>().spawned == false && spawned == false)
            {
                Invoke("ClosedRoom", 1f);
                Destroy(gameObject);
            }
            spawned = true;
        }
    }

    void ClosedRoom()
    {
        Instantiate(templates.closedRoom, transform.position, Quaternion.identity);

    }
}
