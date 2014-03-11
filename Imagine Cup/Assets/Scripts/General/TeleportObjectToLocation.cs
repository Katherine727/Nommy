using UnityEngine;
using System.Collections;
using Assets.Utils;

[RequireComponent(typeof(BoxCollider2D))]
public class TeleportObjectToLocation : MonoBehaviour {

	public Transform TeleportLocation;

	void OnCollisionEnter2D(Collision2D c) {
		c.transform.position = TeleportLocation.position;
	}
}
