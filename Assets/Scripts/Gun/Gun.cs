using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour
{
    //bullet 

    public GunObject[] gunObjects;
    public GunObject gunObject;
    public GameObject[] bulletObjects;
    public GameObject bullet;

    InputManager inputManager;

    int bulletsLeft, bulletsShot;

    public Transform player;
    private Rigidbody playerRb;

    private int random, random2;

    //bools
    bool readyToShoot, reloading;

    public bool shooting;
    public Transform attackPoint;

    //Graphics
    public GameObject muzzleFlash;

    //bug fixing :D
    private bool allowInvoke = true;


    public void Start()
    {
        bulletsLeft = gunObject.magazineSize;
        readyToShoot = true;
        inputManager = FindObjectOfType<InputManager>();
    }
    private void Awake()
    {
        playerRb = GetComponentInParent<Rigidbody>();
        random2 = Random.Range(0, bulletObjects.Length);
        bullet = bulletObjects[random2];
        random = Random.Range(0, gunObjects.Length);
        gunObject = gunObjects[random];
        
    }

    public void Reset()
    {
        playerRb = GetComponentInParent<Rigidbody>();
        random2 = Random.Range(0, bulletObjects.Length);
        bullet = bulletObjects[random2];
        random = Random.Range(0, gunObjects.Length);
        gunObject = gunObjects[random];
    }

    private void Update()
    {
        MyInput();
    }
    private void MyInput()
    {
        //Check if allowed to hold down button and take corresponding input
        if (gunObject.allowButtonHold) shooting = inputManager.LeftClick;
        else shooting = inputManager.LeftClickPressed;



        if (gunObject.constantShooting == true && readyToShoot)
        {
            Shoot();
        }

        //Shooting
        if (readyToShoot && shooting)
        {
            
            //Set bullets shot to 0
            bulletsShot = 0;

            Shoot();
        }


    }

    private void Shoot()
    {
        readyToShoot = false;


        float x = Random.Range(-gunObject.spread, gunObject.spread);
        float y = Random.Range(-gunObject.spread, gunObject.spread);

        Vector3 direction = attackPoint.transform.forward + new Vector3(x, 0, y);

        //Instantiate bullet/projectile
        GameObject currentBullet = Instantiate(bullet, attackPoint.position, attackPoint.rotation); //store instantiated bullet in currentBullet

        currentBullet.GetComponent<Rigidbody>().AddForce(direction * gunObject.shootForce, ForceMode.Impulse);

        //GameObject flash = (GameObject)Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);
        //Destroy(flash, 1.0f);

        bulletsLeft--;
        bulletsShot++;

        //Invoke resetShot function (if not already invoked), with your timeBetweenShooting
        if (allowInvoke)
        {
            Invoke("ResetShot", gunObject.timeBetweenShooting);
            allowInvoke = false;

            playerRb.AddForce(-player.transform.forward * gunObject.recoilForce, ForceMode.Impulse);
        }

        //if more than one bulletsPerTap make sure to repeat shoot function
        if (bulletsShot < gunObject.bulletsPerTap && bulletsLeft > 0)
            Invoke("Shoot", gunObject.timeBetweenShots);
        
    }
    private void ResetShot()
    {
        //Allow shooting and invoking again
        readyToShoot = true;
        allowInvoke = true;
    }


}
