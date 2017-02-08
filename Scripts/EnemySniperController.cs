using UnityEngine;
using System.Collections;

public class EnemySniperController : MonoBehaviour {

    public int health;
    public float shootDelay;
    public float shotCount;
    public float speed;
    public float fireRate;
    public float turnSpeed;
    public GameObject shot;

    public Transform shotSpawn1;
    public Transform shotSpawn2;

    public GameObject explosion;

    private Quaternion startingRotation;
    private Quaternion targetRotation;

	// Use this for initialization
	void Start () {
        startingRotation = gameObject.transform.rotation;
        StartCoroutine(FireRoutine());
	}

    private int i;

    IEnumerator FireRoutine()
    {
        Random.seed = System.Environment.TickCount;
        while(true)
        {
            GetComponent<Rigidbody>().velocity = -Vector3.forward * speed;
            yield return new WaitForSeconds(Random.Range(shootDelay -1, shootDelay + 1));
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            targetRotation = Quaternion.LookRotation(-(PlayerController.current.transform.position - transform.position).normalized);
            for (i = 0; i < shotCount; i++)
            {
                yield return new WaitForSeconds(fireRate);
                Instantiate(shot, shotSpawn1.position, shotSpawn1.rotation);
                yield return new WaitForSeconds(fireRate);
                Instantiate(shot, shotSpawn2.position, shotSpawn2.rotation);
                yield return new WaitForSeconds(fireRate);
            }
            targetRotation = startingRotation;
            yield return new WaitForSeconds(shootDelay);
        }
        


    }

    void Update()
    {
        gameObject.transform.rotation = Quaternion.RotateTowards(gameObject.transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
    }

	
	void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PlayerShot"))
        {
            health = health - other.gameObject.GetComponent<BoltMover>().damage;
            Destroy(other.gameObject);
            if (health < 1)
            {
                Instantiate(explosion, transform.position, transform.rotation);
                AndroidAudioController.current.EnemyExplosion();
                Destroy(gameObject);
                GameController.current.ReduceEnemies(1);
            }
        }
        if(other.CompareTag("Boundary"))
        {
            Destroy(gameObject);
            GameController.current.ReduceEnemies(1);
        }
        
    }
}
