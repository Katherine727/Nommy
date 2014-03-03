using UnityEngine;
using System.Collections;

public class RidePlatform : MonoBehaviour {

    void Update() {
		if(this.transform.parent != null) {
			if(!this.collider2D.renderer.bounds.Intersects(this.transform.parent.collider2D.renderer.bounds))
				this.transform.parent=null;
		}
	}

    void OnTriggerEnter2D(Collider2D c) {
        if (c.gameObject.tag == "Platforms") {
            transform.parent = c.transform;
        }
    }
}
