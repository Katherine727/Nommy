using UnityEngine;
using System.Collections;

public class Triggers : MonoBehaviour {
    void OnTriggerExit2D(Collider2D c) {
        var trig = c.GetComponent<AbilityToStartScript>();
        if (trig != null) {
            trig.StopInterface();
        }
    }
    void OnTriggerEnter2D(Collider2D c) {
        var trig = c.GetComponent<AbilityToStartScript>();
        if (trig != null) {
            trig.StartInterface();
        }
    }
}
