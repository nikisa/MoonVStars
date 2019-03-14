using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swagBackground : MonoBehaviour {
    void Update() {
        transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime);
    }
}
