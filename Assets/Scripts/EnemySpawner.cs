using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour {

    public Text points;

    public GameManager gm;
    EnemyController _enemyController;

    public int delay = 1;

    public GameObject enemy;
    public GameObject boss1;
    public GameObject boss2;
    public GameObject boss3;

    public Transform enemySpawn;

    private void Update() {
        spawnEnemy();
    }


    public void spawnEnemy() {
        if (gm.isWaveFinished()) {
            points.text = gm.getPoints().ToString();
            

            switch (gm.getWave()) {
                case 5:
                    Invoke("spawnBoss1", 2.0f);
                    break;
                case 10:
                    Invoke("spawnBoss2", 2.0f);
                    break;
                case 15:
                    Invoke("spawnBoss3", 2.0f);
                    break;
                default:
                    if (gm.getWave()<15) {
                        Invoke("baseEnemy", 2.0f);
                    }
                    break;
            }
            
            gm.startWave();
        }
    }

    void spawnBoss1() {
        Instantiate(boss1, enemySpawn.position, enemySpawn.rotation);
    }

    void spawnBoss2() {
        Instantiate(boss2, enemySpawn.position, enemySpawn.rotation);
    }

    void spawnBoss3() {
        Instantiate(boss3, enemySpawn.position, enemySpawn.rotation);
    }

    void baseEnemy() {
        Instantiate(enemy, enemySpawn.position, enemySpawn.rotation);
    }
}
