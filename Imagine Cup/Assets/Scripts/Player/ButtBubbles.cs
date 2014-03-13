using UnityEngine;
using System.Collections;

public class ButtBubbles : MonoBehaviour {

	public Transform ParticleSystemObject;

	void Awake() {		
		ParticleSystemObject = transform.Find ("ButtBubbles");
		ParticleSystemObject.particleSystem.Stop ();
	}
}
