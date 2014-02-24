using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class SlotManager : MonoBehaviour {

    /// <summary>
    /// Descripes setting of slots.
    /// </summary>
    public enum SlotsPosition {
        Unknown,
        Horizontal,
        Vertical
    }

    private int _indexOfActiveSlot;
    private List<Slot> _slots;
    private SlotsPosition previousSlotsPosition;
    private bool _addedNewSlot;
    private float prevOffSet;

    public SlotsPosition slotsPosition;
    public GameObject slotPrefab;
    public float offSet;
    public Vector3 position;


    
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
        


        //-########-TEST - DO USUNIECIA POZNIEJ
        AddSlot(PowerEnum.Balls);
        AddSlot(PowerEnum.Fireballs);
        AddSlot(PowerEnum.test);
    }

    void Update() {
        transform.position = Camera.main.ViewportToWorldPoint(position);

        

        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            IndexOfActivatedSlot = 0;
        } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            IndexOfActivatedSlot = 1;
        } else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            IndexOfActivatedSlot = 2;
        } else if (Input.GetKeyDown(KeyCode.Alpha4)) {
            IndexOfActivatedSlot = 3;
        }

        if (Slots.Count > 0) {
            if (Input.GetMouseButton(0)) {
                Slots[IndexOfActivatedSlot].IsUsing = true;
            } else {
                Slots[IndexOfActivatedSlot].IsUsing = false;
            } 
        }

        //jesli zmienila sie wartosc offset, rodzaj pozycjonowania lub dodany nowy slot to zaktualizuj polozenie
        if (offSet != prevOffSet || slotsPosition != previousSlotsPosition || _addedNewSlot) {
            SetPosition();
            previousSlotsPosition = slotsPosition;
            prevOffSet = offSet;
            _addedNewSlot = false;
        }
        
    }

    /// <summary>
    /// Add new slot with given power type. First of all, it's looking for already added slot with given powertype.
    /// If there is a none, it adds new, based on slot model.
    /// </summary>
    /// <param name="powerType">Power type</param>
    public void AddSlot(PowerEnum powerType) {
        var slot = FindSlotByPower(powerType);
        if (slot == null) {
            slot = slot = ((GameObject)Instantiate(slotPrefab)).GetComponent<Slot>();
            slot.transform.parent = this.transform;
            Slots.Add(slot);
            _addedNewSlot = true;
            slot.ChangeModel(powerType,transform);
        }
        slot.IsActive = true;
        slot.IsFull = true;

    }

    /// <summary>
    /// Deletes a slot. First of all, it's looking for a inactive slot, if there is a none, it's looking for deactivated slot
    /// if there is also a none, it deletes last slot from the list.
    /// </summary>
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
                    Slots[i].transform.localPosition = new Vector3(prevPos.x + offSet + Slots[i - 1].Width + Slots[i].Width/2, prevPos.y, 0);
                }
            } else if (slotsPosition == SlotsPosition.Vertical) {
                for (int i = 1; i < Slots.Count; i++) {
                    Vector3 prevPos = Slots[i - 1].transform.localPosition;
                    Slots[i].transform.localPosition = new Vector3(prevPos.x, prevPos.y - offSet - Slots[i - 1].Height - Slots[i].Height/2, 0);
                }
            }
        }
    }
}
