using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageOrderGenerator : MonoBehaviour
{

    public GameObject[] stages;
    public GameObject stage;
    public int random;

    private void Awake()
    {
        random = Random.Range(0, stages.Length);
        stage = stages[random];
    }

    private void Start()
    {
        GameObject clone = (GameObject)Instantiate(stage, transform.position, transform.rotation);
    }
}
