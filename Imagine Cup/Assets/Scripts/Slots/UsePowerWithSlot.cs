using UnityEngine;
using System.Collections;
using Assets.Utils.PowerCommand;
using Assets.Utils;

public class UsePowerWithSlot : MonoBehaviour {

    public GameObject slotManagerObject;

    private PowerCommandFactory pcf;
    private SlotManager slotManager;

    void Awake() {
        pcf = new PowerCommandFactory(this.gameObject);
        slotManager = slotManagerObject.GetComponent<SlotManager>();
        if (slotManager == null) {
            Debug.LogError("SlotManager component is required in given object.");
        }
    }
    void Start() {

    }

    void Update() {
		if(slotManager.Slots.Count >0 ) {
	        if (Input.GetKeyDown(KeyCode.Alpha1)) {
	            slotManager.ActivateSlot(0);
	        } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
	            slotManager.ActivateSlot(1);
	        } else if (Input.GetKeyDown(KeyCode.Alpha3)) {
	            slotManager.ActivateSlot(2);
	        } else if (Input.GetKeyDown(KeyCode.Alpha4)) {
	            slotManager.ActivateSlot(3);
	        } else if (Input.GetKeyDown(KeyCode.Q)) {
	            slotManager.ActivatePrev();
	        } else if (Input.GetKeyDown(KeyCode.E)) {
	            slotManager.ActivateNext();
	        }

			if (Input.GetKeyDown(KeyCode.Space) && slotManager.Slots[slotManager.IndexOfActivatedSlot].Use()) {
				UsePower(slotManager.ActivatedSlot.Power);
			}	        
		}
    }
    void UsePower(PowerEnum PowerType) {
        ICommand PowerCommand = pcf.CreatePowerCommand(PowerType);

        PowerCommand.Execute();
    }
}
