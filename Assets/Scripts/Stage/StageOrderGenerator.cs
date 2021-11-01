using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageOrderGenerator : MonoBehaviour
{

    public GameObject[] stages;
    public GameObject stage;
    public GameObject clone;
    
    int random;

    private void Awake()
    {
        random = Random.Range(0, stages.Length);
        stage = stages[random];
        clone = (GameObject)Instantiate(stage, transform.position, transform.rotation);
    }

    public void Reset()
    {
        Destroy(clone);
        random = Random.Range(0, stages.Length);
        stage = stages[random];
        clone = (GameObject)Instantiate(stage, transform.position, transform.rotation);
    }
}
