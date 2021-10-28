using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{

    public GameObject cursor;


    public void Update()
    {
        
    }

    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}
