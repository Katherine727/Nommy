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

    private int _indexOfActivatedSlot = 0;
    private List<Slot> _slots;
    private SlotsPosition previousSlotsPosition;
    private bool _addedNewSlot;
    private float prevOffSet;

    public SlotsPosition slotsPosition;
    public GameObject slotPrefab;
    

    [Range(0,25)]
    public float offSet;

    
    public Vector3 position;

    /// <summary>
    /// It gives activated slot if there is one
    /// </summary>
    public Slot ActivatedSlot {
        get {
            if (Slots.Count > 0) {
                return Slots[IndexOfActivatedSlot];
            } else {
                return null;
            }
        }
    }
    public int IndexOfActivatedSlot {
        get {
            return _indexOfActivatedSlot;
        }
        private set {
            _indexOfActivatedSlot = Mathf.Clamp(value,0,Slots.Count);
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
        //AddSlot(PowerEnum.BubbleJump);
        //AddSlot(PowerEnum.SugarBrick);
    }

    void Update() {

		if(Slots.Count > 0) {
       		//aby domyslnie byl jakis aktywowany slot; w start nie moge, bo nie ma pewnosci, 
        	//ze w slocie byla uruchomiona metoda Start, ktora potrzebuje do tych zmian
	        if (!ActivatedSlot.IsActivated) {
	            ActivateSlot(IndexOfActivatedSlot);
	        }

	        transform.position = Camera.main.ViewportToWorldPoint(position);

	        //jesli zmienila sie wartosc offset, rodzaj pozycjonowania lub dodany nowy slot to zaktualizuj polozenie
	        if (offSet != prevOffSet || slotsPosition != previousSlotsPosition || _addedNewSlot) {
	            SetPosition();
	            previousSlotsPosition = slotsPosition;
	            prevOffSet = offSet;
	            _addedNewSlot = false;
	        }
		}
        
    }

    /// <summary>
    /// Activate a slot with given index.
    /// </summary>
    /// <param name="slotIndex">Indicates if method activated a slot (then returns true, otherwise it returns false).</param>
    /// <returns>If activation goes well, method will return true, otherwise it will return false</returns>
    public bool ActivateSlot(int slotIndex) {
        if (Slots.Count > 0 && slotIndex < Slots.Count && slotIndex >= 0) {
            Slots[IndexOfActivatedSlot].IsActivated = false;
            IndexOfActivatedSlot = slotIndex;
            Slots[IndexOfActivatedSlot].IsActivated = true;
        }

        return Slots[IndexOfActivatedSlot].IsActivated;
    }

    /// <summary>
    /// Method activates next slot. If there is no more slots (current index is the last one)
    /// it activates the first slot.
    /// If Slots list is empty, nothing happens.
    /// </summary>
    public void ActivateNext() {
        int indexOfNextSlot = (IndexOfActivatedSlot + 1) % Slots.Count;
        ActivateSlot(indexOfNextSlot);
    }

    /// <summary>
    /// Method activates previous slot. If there is no previous slot (current index is the first one)
    /// it activates the last one.
    /// If Slots list is empty, nothing happens.
    /// </summary>
    public void ActivatePrev() {

        int indexOfNextSlot = IndexOfActivatedSlot - 1;
        if (indexOfNextSlot < 0) {
            indexOfNextSlot += Slots.Count;
        }

        ActivateSlot(indexOfNextSlot);
    }
    /// <summary>
    /// Add new slot with given power type. First of all, it's looking for already added slot with given powertype.
    /// If there is a none, it adds new, based on slot model.
    /// </summary>
    /// <param name="powerType">Power type</param>
    public void AddSlot(PowerEnum powerType) {
        var slot = FindSlotByPower(powerType);
        if (slot == null) {
            slot = ((GameObject)Instantiate(slotPrefab)).GetComponent<Slot>();
            slot.transform.parent = this.transform;
            Slots.Add(slot);
            _addedNewSlot = true;
            slot.ChangeModel(powerType,transform);
        }
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

    public void RefillActivatedSlot(PowerEnum powerType) {
        if (ActivatedSlot != null) {
            ActivatedSlot.Power = powerType;
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
                    Slots[i].transform.localPosition = new Vector3(prevPos.x + offSet + Slots[i - 1].Width/2 + Slots[i].Width/2, prevPos.y, 0);
                }
            } else if (slotsPosition == SlotsPosition.Vertical) {
                for (int i = 1; i < Slots.Count; i++) {
                    Vector3 prevPos = Slots[i - 1].transform.localPosition;
                    Slots[i].transform.localPosition = new Vector3(prevPos.x, prevPos.y - offSet - Slots[i - 1].Height/2 - Slots[i].Height/2, 0);
                }
            }
        }
    }
}
