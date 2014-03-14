using UnityEngine;
using System.Collections;

public class DonutParticler : MonoBehaviour {
	ParticleSystem ps;
	SplashScreen ss;
	void Awake () {
		ps = this.gameObject.GetComponent<ParticleSystem> ();
		ss = Camera.main.GetComponent<SplashScreen> ();
		ps.Stop ();
	}
	
	// Update is called once per frame
	void Update () {
		if (ss.CurrentStatus == SplashScreen.FadeStatus.FadeWaiting && Input.anyKey) {
			ps.Emit(5);
		}
	}
}
