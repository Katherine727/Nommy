using UnityEngine;
using System.Collections;
using Assets.Utils;
using Assets.Utils.PowerCommand;

public class PowerUser : MonoBehaviour {
	private PowerCommandFactory pcf;
	private SlotManager sm;

	// Use this for initialization
	void Start () {
		pcf = new PowerCommandFactory(this.gameObject);	
	}

	void Awake() {
		sm = FindObjectOfType<SlotManager>();
	}
	
	// Update is called once per frame
	void Update () {
		if(sm.Slots.Count>0) {
			if(Input.GetKey(KeyCode.Space)){ //The "Use Power" Key
				UsePower(sm.ActivatedSlot.Power);
				sm.Slots[sm.IndexOfActivatedSlot].IsUsing=true;
			} else {
				sm.Slots[sm.IndexOfActivatedSlot].IsUsing=false;
			}
		}
	}

	void UsePower(PowerEnum PowerType) {
		ICommand PowerCommand = pcf.CreatePowerCommand(PowerType);

		PowerCommand.Execute();
	}
}
