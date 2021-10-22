using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{

    public GameObject DeathCube;
    private Vector3 scaleChange;

    private void Start()
    {
        InvokeRepeating("ZoneShrink", 15f, 0.02f);

        scaleChange = new Vector3(-0.1f, 0.0f, -0.1f);
    }


    public void ZoneShrink()
    {
        DeathCube.transform.localScale = Vector3.Lerp(transform.localScale, scaleChange, 0.3f * Time.deltaTime); 
    }
}
