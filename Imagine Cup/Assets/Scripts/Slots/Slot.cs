using UnityEngine;
using System.Collections;
using System;
using System.Linq;

public class Slot : MonoBehaviour{


    private bool _isActive;
    private bool _isFull;
    private bool _isActivated;
    private float _actualValue;
    private float _actualValueProc;
    private PowerEnum _power;
    private float _minValue = 0;
    private SpriteRenderer spriteRenderer;


    public float timeToEndInSec;
    public float usingMuliplayer;

    [HideInInspector]
    public bool IsActive {
        get { return _isActive; }
        set { _isActive = value; }
    }
    [HideInInspector]
    public bool IsFull {
        get { return _isFull; }
        set {  
            _isFull = value;
            if (ActualValue != timeToEndInSec && _isFull == true) {
                ActualValue = timeToEndInSec; 
            }
        }
    }
    [HideInInspector]
    public bool IsActivated {
        get { return _isActivated; }
        set { _isActivated = value; }
    }

    [HideInInspector]
    public float ActualValue {
        get {
            return _actualValue;
        }
        private set {

            if (timeToEndInSec > 0) {
                _actualValue = Mathf.Clamp(value, _minValue, timeToEndInSec);
            } else {
                throw new Exception("Time mustn't be equal to 0!");
            }
            IsFull = (_actualValue == timeToEndInSec) ? true : false;
            IsActive = (_actualValue > 0) ? true : false;
        }
    }

    [HideInInspector]
    public float ActualValueProc {
        get {
            if (timeToEndInSec > 0) {
                _actualValueProc = Mathf.Round(((float)ActualValue / timeToEndInSec) * 10000) / 100f;
                return _actualValueProc;
            } else {
                throw new Exception("Time mustn't be equal to 0!");
            }
        }
        set {
            ActualValue = timeToEndInSec * value / 100f;
        }
    }

    [HideInInspector]
    public PowerEnum Power {
        get { return _power; }
        set{
            if (transform.parent != null) {
                ChangeSpriteByPower(value,transform.parent);
            } else {
                ChangeSpriteByPower(value, transform);
            }
            _power = value;
        }
    }

    [HideInInspector]
    public float Width {
        get {
            if (spriteRenderer != null) {
                return spriteRenderer.bounds.size.x;
            } else {
                return 0;
            }
        }
    }

    [HideInInspector]
    public float Height {
        get {
            if (spriteRenderer != null) {
                return spriteRenderer.bounds.size.y;
            } else {
                return 0;
            }
        }
    }
    void Start() {
        spriteRenderer = transform.GetComponent<SpriteRenderer>();
    }
    void Update() {
        if (IsActive) {
            double deltaValue = Time.deltaTime * usingMuliplayer;
            ActualValue -= (float)deltaValue;
            //Debug.Log(ActualValue.ToString()+" IsActive: " + IsActive.ToString()); --test
        }
            
        //metoda rysowania...
    }

    private void ChangeSpriteByPower(PowerEnum value, Transform obj) {
        PowerSlotSpriteProvider pssProvider = obj.GetComponent<PowerSlotSpriteProvider>();
        if (pssProvider != null && spriteRenderer != null) {
            spriteRenderer.sprite = pssProvider.powerSpritePairs.Where(pair => pair.power == value).First().sprite;
        }
    }

}
