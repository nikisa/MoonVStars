using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public GameManager gm;
    EarthManager _earthManager;

    public bool playerInputEnabled = false;

    private float xMin = -4.5f;
    private float xMax = 4.5f;
    public float speed;
    public Rigidbody rb;

    private float chargeTimer = 0;

    public GameObject[] projectiles;
    
    public Transform shotSpawn;
    
    void Start() {
        _earthManager = Object.FindObjectOfType<EarthManager>().GetComponent<EarthManager>();
        rb = GetComponent<Rigidbody>();
        gm = Object.FindObjectOfType<GameManager>().GetComponent<GameManager>();
    }

    void Update() {
        playerMovement();
    }

    void OnMouseDrag() {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = new Vector3(Mathf.Clamp(objPosition.x, xMin, xMax), -1.6f, 10f);
    }

    private void OnTriggerEnter(Collider other) {        
        Destroy(other.gameObject);
    }

    void playerMovement() {
        if (playerInputEnabled) {
            if (Input.GetKey(KeyCode.Mouse0)) {
                chargeTimer += Time.deltaTime;
            }
            if (Input.GetKeyUp(KeyCode.Mouse0) && chargeTimer < 2) {
                Debug.Log("nada");
                chargeTimer = 0;
            }
            if (Input.GetKeyUp(KeyCode.Mouse0) && chargeTimer > 2 && chargeTimer < 4) {
                Instantiate(projectiles[0], shotSpawn.position, Quaternion.identity);
                Debug.Log("lvl 1");
                chargeTimer = 0;
            }

            if (Input.GetKeyUp(KeyCode.Mouse0) && chargeTimer > 4 && chargeTimer < 6) {
                Instantiate(projectiles[1], shotSpawn.position, Quaternion.identity);
                Debug.Log("lvl 2");
                chargeTimer = 0;
            }

            if (Input.GetKeyUp(KeyCode.Mouse0) && chargeTimer > 6) {
                Instantiate(projectiles[2], shotSpawn.position, Quaternion.identity);
                Debug.Log("lvl 3");
                chargeTimer = 0;
            }
        }
    }  
}﻿

