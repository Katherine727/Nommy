﻿using UnityEngine;
using System.Collections;

public class FoodEater : MonoBehaviour {

	public float SatiationRate = 0f;//stan nasycenia naszego pozeracza
    public GameObject slotManagerObj;

    private SlotManager slotManager;
	// Use this for initialization
	void Start () {
        slotManager = slotManagerObj.GetComponent<SlotManager>();
        if (slotManager == null) {
            Debug.LogError("SlotManager component is required for game to work properly.");
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/// <summary>
	/// Zdarzenie po kolizji z jakims triggerem(triggerami beda np smakolyki)
	/// </summary>
	/// <param name="c">c to collider tego triggera(np. boxcollider2d itp)</param>
	void OnTriggerEnter2D(Collider2D c)	{
		if (c.gameObject.layer == LayerMask.NameToLayer("Food")) { //jesli smakolyk
            var powerType = c.GetComponent<PowerContainer>().Power;
            slotManager.RefillActivatedSlot(powerType);
			Destroy(c.gameObject); //to niszcz go

			//KOD DAJACY POWER UP I NASYCENIE TODO
		}
	}
}
