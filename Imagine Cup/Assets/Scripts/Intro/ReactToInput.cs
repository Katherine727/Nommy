using UnityEngine;
using System.Collections;

public class ReactToInput : MonoBehaviour {

	private SplashScreen ss = null;

	public Texture2D SplashAfter;
	AudioSource asrc;
	bool playedOnce=false;

	// Update is called once per frame
	void Update () {		
		if(ss == null || asrc == null) {
			ss = Camera.current.GetComponent<SplashScreen>();
			asrc = this.gameObject.GetComponent<AudioSource>();
		}
		else {
			if(ss.CurrentStatus == SplashScreen.FadeStatus.FadeOut) {
				ss.splashLogo = SplashAfter;
				if(!playedOnce) {
					asrc.Play ();
					playedOnce=true;
				}
			}
		}
	}
}
