using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthManager : MonoBehaviour {

    public GameManager gm;

    public int life;
    public bool gameOver = false;

    void Start() {
        life = 3;    
    }
    

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "ProjectileLevel1") {
            life -= 1;
            UpdateEarthStatus();
            gm.combo = 1;
        }
        else if (other.gameObject.tag == "ProjectileLevel2") {
            life -= 2;
            UpdateEarthStatus();
            gm.combo = 1;
        }
        else if (other.gameObject.tag == "ProjectileLevel3") {
            life -= 3;
            UpdateEarthStatus();
            gm.combo = 1;
        }
        Destroy(other.gameObject); 
    }

    public void UpdateEarthStatus() {
        
        switch (life) {
            case 3:
                transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
                break;
            case 2:
                transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.gray;
                break;
            case 1:
                transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, 255);
                break;
        }
    }
}
