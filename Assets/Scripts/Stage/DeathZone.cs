using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{

    public GameObject DeathCube;
    private Vector3 scaleChange, startingScale;

    public float startTime,scaleRate;

    private void Start()
    {
        InvokeRepeating("ZoneShrink", startTime, scaleRate);

        scaleChange = new Vector3(-0.1f, 0.0f, -0.1f);
        startingScale = new Vector3(11f, 90f, 19f);
    }


    public void ZoneShrink()
    {
        DeathCube.transform.localScale = Vector3.Lerp(transform.localScale, scaleChange, 0.3f * Time.deltaTime); 
    }

    public void Reset()
    {
        CancelInvoke();
        DeathCube.transform.localScale = startingScale;
        InvokeRepeating("ZoneShrink", startTime, scaleRate);
    }
}
