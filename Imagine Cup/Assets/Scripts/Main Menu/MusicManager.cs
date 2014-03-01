using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

	public AudioSource Music { get; private set; }

	// Use this for initialization
	void Start () {
		Invoke("PlayMusic", 0.2f);
	}

	void Awake() {
		Music = this.gameObject.GetComponent<AudioSource>();
	}

	void PlayMusic() {
		Music.Play();
	}
	
	// Update is called once per frame
	void Update () {

	}
}
