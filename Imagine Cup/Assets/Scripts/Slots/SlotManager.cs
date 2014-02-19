using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Assets.Utils;

//[Serializable]
//public class SlotSpriteDictionary : Dictionary<PowerEnum,SpriteRenderer>{}
public class SlotManager : MonoBehaviour {

    public float maxValueInSlot;
    public float timeToEndInSec;
    public float startValueInProc;
    public float usingMultiplayer;
    public int numberOfSlots;

    private int _indexOfActiveSlot;
    public int IndexOfActiveSlot {
        get {
            return _indexOfActiveSlot;
        }
        set {
            if (value < Slots.Count && value >= 0) {
                _indexOfActiveSlot = value;
            }
        }
    }

    private List<Slot> _slots;
    //public SlotSpriteDictionary ssDictionary;

    public List<Slot> Slots {
        get {
            return _slots;
        }
        private set {
            _slots = value;
        }
    }
    // Use this for initialization
    void Awake() {
        Slots = new List<Slot>();
    }
    void Start() {
    }

    // Update is called once per frame
    void Update() {

        numberOfSlots = Mathf.Clamp(numberOfSlots, 0, 5);
        if (Slots.Count < numberOfSlots) {
            AddEmptySlot();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            SetFlagInCurrentDelInPrev(0, Slot.SlotState.Activated);
        } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            SetFlagInCurrentDelInPrev(1, Slot.SlotState.Activated);
        } else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            SetFlagInCurrentDelInPrev(2, Slot.SlotState.Activated);
        } else if (Input.GetKeyDown(KeyCode.Alpha4)) {
            SetFlagInCurrentDelInPrev(3, Slot.SlotState.Activated);
        } else if (Input.GetKeyDown(KeyCode.Alpha5)) {
            SetFlagInCurrentDelInPrev(4, Slot.SlotState.Activated);
        }

        if (Input.GetMouseButtonDown(1)) {
            var slot = FindInState(Slot.SlotState.Activated & Slot.SlotState.Active);
            if (slot != null) {
                SetFlagInCurrentDelInPrev(Slots.IndexOf(slot), Slot.SlotState.Using);
            }
        }
        foreach (var slot in Slots) {
            slot.Update();
        }
    }

    private void AddEmptySlot() {
        var gObj = new GameObject();
        var slot = new Slot(gObj, maxValueInSlot, timeToEndInSec, startValueInProc, usingMultiplayer);
        gObj.AddComponent("SpriteRenderer");
        gObj.transform.parent = this.transform;
        gObj.name = "Slot";
        slot.CurrentState = Slot.SlotState.Unknown;

        Slots.Add(slot);

    }

    private void SetFlagInCurrentDelInPrev(int index, Slot.SlotState state) {
        if (Slots.Count > index) {
            var slot = FindInState(state);
            if (slot != null) {
                slot.CurrentState ^= state;
            }
            Slots[index].CurrentState |= state;
        }
    }

    public void AddSlot(PowerEnum powerType) {
        var slot = FindSlotByPower(powerType);
        if (slot == null) {
            AddEmptySlot();
            slot = Slots.Last();
            slot.Power = powerType;
        }
        slot.CurrentState = Slot.SlotState.Activated | Slot.SlotState.Active;
        slot.SlotObj.name += slot.Power.ToString();
        if (startValueInProc == 100) {
            slot.CurrentState |= Slot.SlotState.Full;
        }
    }

    private Slot FindInState(Slot.SlotState state) {
        var slot = Slots.Where(s => (s.CurrentState & state) == state).FirstOrDefault();
        return slot;
    }
    private Slot FindSlotByPower(PowerEnum powerType) {
        var slot = Slots.Where(s => s.Power == powerType).FirstOrDefault();
        return slot;
    }
}
