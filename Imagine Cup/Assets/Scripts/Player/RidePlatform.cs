using UnityEngine;
using System.Collections;

public class RidePlatform : MonoBehaviour {

    void Update() {
		if(this.transform.parent != null) {
			if(this.transform.parent.gameObject.tag == "Platforms" && !this.collider2D.renderer.bounds.Intersects(this.transform.parent.collider2D.renderer.bounds))
				this.transform.parent=null;
		}
	}

    void OnTriggerEnter2D(Collider2D c) {
        //player musi byc w 'gornej' warstwie boxcollidera aby sie przyczepic do platformy
        if (c.gameObject.tag == "Platforms" && this.transform.position.y - this.collider2D.renderer.bounds.size.y / 2 >= c.transform.position.y + c.collider2D.renderer.bounds.size.y / 3) {
            transform.parent = c.transform;
        }
    }
}
