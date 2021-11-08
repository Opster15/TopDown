using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WeaponSetting")]
public class GunObject : ScriptableObject
{
    //bullet 
    public GameObject bullet;

    //bullet force
    public float shootForce;
    public float recoilForce;

    //Gun stats
    public float timeBetweenShooting, timeBetweenShots,spread;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold, constantShooting,randomBullet;

}
