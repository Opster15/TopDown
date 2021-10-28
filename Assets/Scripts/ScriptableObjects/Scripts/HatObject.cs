using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "HatSetting")]
public class HatObject : ScriptableObject
{
    public int maxHealth = 100;


    public float moveSpeed,maxSpeed;

    public float dashCooldown,dashForce;
 
    public Vector3 playerScale; 

    public float abilityCooldown,abilityDuration;

    public int abilityNumber;
    #region
    //0 is no ability
    //1 is Invis
    //2 is Bubble Shield
    #endregion
}
