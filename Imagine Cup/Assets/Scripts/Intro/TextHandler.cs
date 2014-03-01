using UnityEngine;
using System.Collections;

public class TextHandler : MonoBehaviour {

	Animator a;
	SplashScreen ss;
	bool playedOnce = false;

	void Start() {

	}

	// Update is called once per frame
	void Update () {
		if( a == null || ss == null) {
			a = this.gameObject.GetComponent<Animator>();
			ss = Camera.current.GetComponent<SplashScreen>();
		} else {
			if(!playedOnce) {
				if(ss.CurrentStatus == SplashScreen.FadeStatus.FadeOut) {
					a.Play(Animator.StringToHash("Fade out"));
					playedOnce=true;
				}
			}
		}
	}
}
