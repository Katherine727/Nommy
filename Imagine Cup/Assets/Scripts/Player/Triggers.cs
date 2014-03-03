using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Triggers : MonoBehaviour {
    public List<string> tags;
    void OnTriggerExit2D(Collider2D c) {
        if (tags.Count > 0 && tags.Exists(el => el == c.tag)) {
            var trig = c.GetComponent<AbilityToStartScript>();
            if (trig != null) {
                trig.StopInterface();
            }
        }
    }
    void OnTriggerEnter2D(Collider2D c) {
        if (tags.Count > 0 && tags.Exists(el => el == c.tag)) {
            var trig = c.GetComponent<AbilityToStartScript>();
            if (trig != null) {
                trig.StartInterface();
            }
        }
    }
}
