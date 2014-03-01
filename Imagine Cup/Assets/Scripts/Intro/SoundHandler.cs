using UnityEngine;
using System.Collections;

public class SoundHandler : MonoBehaviour {

	AudioSource asrc;
	SplashScreen ss;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(asrc==null || ss == null){			
			asrc = Camera.current.GetComponent<AudioSource>();
			ss = Camera.current.GetComponent<SplashScreen>();
		} else {
			if(ss.CurrentStatus == SplashScreen.FadeStatus.FadeOut) {
				asrc.Play();
				this.enabled=false;
			}
		}
	}
}
