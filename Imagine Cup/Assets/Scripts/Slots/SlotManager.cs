using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Assets.Utils;


public class SlotManager : MonoBehaviour {
    public enum SlotsPosition {
        Unknown,
        Horizontal,
        Vertical
    }

    private int _indexOfActiveSlot;
    private List<Slot> _slots;
    private SlotsPosition previousSlotsPosition;
    private bool _addedNewSlot;

    public SlotsPosition slotsPosition;
    public float timeToEndInSec;
    //public float startValueInProc;
    public float usingMultiplayer;
    //public float maxNumberOfSlots;
    public GameObject slotPrefab;
    public float offSet;


    public int IndexOfActivatedSlot {
        get {
            return _indexOfActiveSlot;
        }
        set {
            if (Slots.Count > 0 && value < Slots.Count && value >= 0) {
                Slots[_indexOfActiveSlot].IsActivated = false;
                _indexOfActiveSlot = value;
                Slots[_indexOfActiveSlot].IsActivated = true;
            }
        }
    }

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
        slotsPosition = SlotsPosition.Horizontal;
        previousSlotsPosition = SlotsPosition.Unknown;
        AddSlot(PowerEnum.Balls);
    }

    // Update is called once per frame
    void Update() {

        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            IndexOfActivatedSlot = 0;
        } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            IndexOfActivatedSlot = 1;
        } else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            IndexOfActivatedSlot = 2;
        } else if (Input.GetKeyDown(KeyCode.Alpha4)) {
            IndexOfActivatedSlot = 3;
        }

        if (slotsPosition != previousSlotsPosition || _addedNewSlot) {
            SetPosition();
            previousSlotsPosition = slotsPosition;
            _addedNewSlot = false;
        }
    }

    private Slot AddEmptySlot() {
        var slot = ((GameObject)Instantiate(slotPrefab)).GetComponent<Slot>();
        slot.transform.parent = this.transform;
        Slots.Add(slot);
        _addedNewSlot = true;
        return slot;
    }

    public void AddSlot(PowerEnum powerType) {
        var slot = FindSlotByPower(powerType);
        if (slot == null) {
            slot = AddEmptySlot();
            slot.Power = powerType;
        }
        slot.IsActive = true;
        slot.IsFull = true;
        slot.timeToEndInSec = timeToEndInSec;
        slot.usingMuliplayer = usingMultiplayer;


    }

    public void DeleteSlot() {
        if (Slots.Count > 0) {
            var inActiveSlots = Slots.Where(s => s.IsActive == false) as List<Slot>;
            Slot slot;
            if (inActiveSlots.Count > 0) {
                slot = inActiveSlots.First();
                DestroySlot(slot);
            } else {
                var inActivatedSlots = Slots.Where(s => s.IsActivated == false) as List<Slot>;
                if (inActivatedSlots.Count > 0) {
                    slot = inActivatedSlots.First();
                    DestroySlot(slot);
                } else {
                    slot = Slots.Last();
                    DestroySlot(slot);
                }
            }
        }
    }
    private void DestroySlot(Slot slot) {
        if (Slots.Remove(slot)) {
            Destroy(slot.gameObject);
        }
    }
    private Slot FindSlotByPower(PowerEnum powerType) {
        var slot = Slots.Where(s => s.Power == powerType).FirstOrDefault();
        return slot;
    }
    private void SetPosition() {
        if (Slots.Count > 0) {
            Slots[0].transform.localPosition = Vector3.zero;
            if (slotsPosition == SlotsPosition.Horizontal) {
                for (int i = 1; i < Slots.Count; i++) {
                    Vector3 prevPos = Slots[i - 1].transform.localPosition;
                    Slots[i].transform.localPosition = new Vector3(prevPos.x + offSet + Slots[i - 1].Width / 2, prevPos.y, 0);
                }
            } else {
                for (int i = 1; i < Slots.Count; i++) {
                    Vector3 prevPos = Slots[i - 1].transform.localPosition;
                    Slots[i].transform.localPosition = new Vector3(prevPos.x, prevPos.y + offSet + Slots[i - 1].Height / 2, 0);
                }
            }
        }
    }
}
