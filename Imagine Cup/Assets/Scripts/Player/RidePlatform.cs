using UnityEngine;
using System.Collections;

public class RidePlatform : MonoBehaviour {

    void OnTriggerExit2D(Collider2D c) {
        if (c.gameObject.tag == "Platforms") {
            transform.parent = null;
        }
    }
    void OnTriggerEnter2D(Collider2D c) {
        if (c.gameObject.tag == "Platforms") {
            transform.parent = c.transform;
        }
    }
}
