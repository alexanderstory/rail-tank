using UnityEngine;
using System.Collections;

public class EnemyHoverController : MonoBehaviour {

    public int health;
    public float shootDelay;
    public float shotCount;
    public float speed;
    public float fireRate;
    public GameObject shot;

    public Transform shotSpawn1;
    public Transform shotSpawn2;

    public GameObject explosion;

	// Use this for initialization
	void Start () {
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
            for (i = 0; i < shotCount; i++)
            {
                yield return new WaitForSeconds(fireRate);
                Instantiate(shot, shotSpawn1.position, shotSpawn1.rotation);
                yield return new WaitForSeconds(fireRate);
                Instantiate(shot, shotSpawn2.position, shotSpawn2.rotation);
                yield return new WaitForSeconds(fireRate);
            }
        }
        


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
