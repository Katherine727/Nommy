using UnityEngine;
using System.Collections;

public class SlotAdder : MonoBehaviour {
	private SlotManager sm;

	void Awake() {
		sm = FindObjectOfType<SlotManager>();
	}

	// Use this for initialization
	void OnCollisionEnter2D(Collision2D c) {
		sm.AddSlot(PowerEnum.None);
		Destroy(this.gameObject);
	}
}
