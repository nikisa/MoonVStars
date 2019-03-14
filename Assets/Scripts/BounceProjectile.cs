using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceProjectile : MonoBehaviour {

    public GameObject projectile;
    Vector3 position;
    Quaternion rotation;
    public Transform earth;


    void OnTriggerEnter(Collider other) {
        int rnd = Random.Range(-15, 15);

        Vector2 direction = earth.position - other.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x + rnd) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle + 270, Vector3.forward);
        other.transform.rotation = rotation;
        Destroy(other.gameObject);
        Instantiate(projectile, other.transform.position, other.transform.rotation);
    }
}
