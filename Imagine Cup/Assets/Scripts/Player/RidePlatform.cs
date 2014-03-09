using UnityEngine;
using System.Collections;

public class RidePlatform : MonoBehaviour {

    private SpriteRenderer _legObj;

    void Start() {
        var noga = GameObject.Find("stopa lewa");
        _legObj = noga.GetComponent<SpriteRenderer>();
    }
    void Update() {
		if(this.transform.parent != null) {
            //dodac 'wiekszy box' by dzialalo to stabilniej? => nie, poprsotu trzeba unikac skalowania!!!
            if (this.transform.parent.gameObject.tag == "Platforms"
                && !_legObj.bounds.Intersects(this.transform.parent.collider2D.renderer.bounds))
				this.transform.parent=null;
		}
	}

    void OnTriggerEnter2D(Collider2D c) {
        //player musi byc w 'gornej' warstwie boxcollidera aby sie przyczepic do platformy
        if (c.gameObject.tag == "Platforms" && _legObj.transform.position.y - _legObj.bounds.size.y / 2 >= c.transform.position.y + c.collider2D.renderer.bounds.size.y / 3) {
            transform.parent = c.transform;
        }
    }
}
