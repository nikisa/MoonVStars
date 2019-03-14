using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour {

    public GameManager gm;
  
    public bool EnemyActivated = false;

    private float xMin = -4.5f;
    private float xMax = 4.5f;

    public float speed;
    public float delta = 4.5f;

    
    public int life;

    public Rigidbody rb;

    public GameObject projectileLevel1;
    public GameObject projectileLevel2;
    public GameObject projectileLevel3;

    public GameObject[] projectiles = new GameObject[3];

    private Vector3 startPosition;
    
    public GameObject shotSpawn;
    
    public GameObject player;

    private float attackTime;
    public float timeBetweenAttacks;


    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        gm = Object.FindObjectOfType<GameManager>().GetComponent<GameManager>();
        projectileLevel1 = GameObject.FindGameObjectWithTag("ProjectileLevel1");
        projectileLevel2 = GameObject.FindGameObjectWithTag("ProjectileLevel2");
        projectileLevel3 = GameObject.FindGameObjectWithTag("ProjectileLevel3");

        projectiles[0] = projectileLevel1;
        projectiles[1] = projectileLevel2;
        projectiles[2] = projectileLevel3;

        player = GameObject.FindGameObjectWithTag("Player");

        startPosition = transform.position;
        life = 1 * gm.getWave();
        Debug.Log("Enemy life:" + life);
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 v = startPosition;
        v.x += delta * Mathf.Sin(Time.time * speed);
        transform.position = v;

        if (Time.time >= attackTime) {
            Shot();
            attackTime = Time.time + timeBetweenAttacks;
        }

        if (life <= 0) {
            gm.finishWave();
            gm.nextWave();
            Destroy(gameObject);
        }
    }

    virtual public void Shot() {
        if (EnemyActivated) {
            int rnd = Random.Range(-10, 10);
            int rndProjectile = Random.Range(0, 2);

            Vector2 direction = player.transform.position - shotSpawn.transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x + rnd) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle + 270, Vector3.forward);
            shotSpawn.transform.rotation = rotation;

            if (this.gameObject.tag=="Boss") {
                Instantiate(projectiles[rndProjectile], shotSpawn.transform.position, shotSpawn.transform.rotation);
            }
            else if(this.gameObject.tag == "Enemy") {
                Instantiate(projectiles[0], shotSpawn.transform.position, shotSpawn.transform.rotation);
            }
            
        }       
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "ProjectileLevel1") {
            life -= 1;
            gm.points += 10 * gm.combo;
            gm.combo++;
        }
        else if (other.gameObject.tag == "ProjectileLevel2") {
            life -= 5;
            gm.points += 10 * gm.combo;
            gm.combo++;
        }
        else if (other.gameObject.tag == "ProjectileLevel3") {
            life -= 10;
            gm.points += 10 * gm.combo;
            gm.combo++;
        }

        Destroy(other.gameObject);
    }

    public void InitEnemy() {
        EnemyActivated = true;
    }
}
