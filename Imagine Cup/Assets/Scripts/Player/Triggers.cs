using UnityEngine;
using System.Collections;

public class Triggers : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D c) {
        var trig = c.GetComponent<AbilityToStartScript>();
        if (trig != null) {
            trig.StartScript();
        }
    }
}
