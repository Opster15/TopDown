using UnityEngine;

public class Bullet : MonoBehaviour
{
    //Assignables
    public Rigidbody rb;
    public GameObject explosion;
    public LayerMask whatIsEnemies, whatIsPlayers;
    public Manager manager;



    //Stats
    [Range(0f, 1f)]
    public float bounciness;
    public bool useGravity;

    //Damage
    public int explosionDamage;
    public float explosionRange;
    public float explosionForce;
    public float maxLifetime;
    public float shootForce;

    public float mass;

    public bool doesCollide;
    public bool followingCursor;


    //Lifetime
    public int maxCollisions;
    public bool explodeOnTouch = true;

    //public float maxLifetime;

    int collisions;
    PhysicMaterial physics_mat;
    public int random;

    public void Start()
    {
        Setup();
        rb = GetComponent<Rigidbody>();

        rb.mass = mass;

        manager = FindObjectOfType<Manager>();
    }

    private void Update()
    {


        //When to explode:
        if (collisions > maxCollisions) Explode();

        //Count down lifetime
        maxLifetime -= Time.deltaTime;
        if (maxLifetime <= 0) Explode();

        if (followingCursor == true)
        {
            transform.position = Vector3.Lerp(transform.position, manager.cursor.transform.position, 0.025f);
        }
    }

    private void Explode()
    {
        GameObject flash = (GameObject)Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(flash, 1.0f);

        //Check for enemies 
        Collider[] enemies = Physics.OverlapSphere(transform.position, explosionRange, whatIsEnemies);
        for (int i = 0; i < enemies.Length; i++)
        {
            //Get component of enemy and call Take Damage

            //Just an example!
            enemies[i].GetComponent<PlayerStats>().TakeDamage(explosionDamage);

            if (enemies[i].GetComponent<Rigidbody>())
                enemies[i].GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRange);
        }

        Collider[] players = Physics.OverlapSphere(transform.position, explosionRange, whatIsPlayers);
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].GetComponent<Rigidbody>())
                players[i].GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRange);
        }
       
        Destroy(gameObject);
    }
    private void Delay()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Don't count collisions with other bullets
        //if (collision.GetComponent<Collider>().CompareTag("Bullet")) return;


        collisions++;
        
        
        if (collision.collider.CompareTag("Enemy") && explodeOnTouch) Explode();
    }

    private void OnColliderEnter(Collider other)
    {
        if (other.CompareTag("Shield") && explodeOnTouch) Delay();
    }



    private void Setup()
    {
        //Create a new Physic material
        physics_mat = new PhysicMaterial();
        physics_mat.bounciness = bounciness;
        physics_mat.frictionCombine = PhysicMaterialCombine.Minimum;
        physics_mat.bounceCombine = PhysicMaterialCombine.Maximum;
        //Assign material to collider
        GetComponent<SphereCollider>().material = physics_mat;

        //Set gravity
        rb.useGravity = useGravity;
    }

    /// Just to visualize the explosion range
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }

}
