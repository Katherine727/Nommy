using UnityEngine;
using System.Collections;
using System;

public class Slot {

    [Flags]
    public enum SlotState {
        Unknown = 0,
        Active = 1,
        Full = 2,
        Activated = 4,
        Using = 8
    }

    private float _minValue = 0;
    private double _actualValue;
    public SlotState CurrentState { get; set; }
    public float MaxValue { get; set; }
    public double ActualValue {
        get {
            return _actualValue;
        }
        set {
            if (MaxValue > 0) {
                _actualValue = Mathf.Clamp((float)value, _minValue, MaxValue);
            } else {
                throw new Exception("MaxValue mustn't be equal to 0!");
            }

            if (_actualValue == MaxValue) {
                CurrentState |= SlotState.Full;
            } else {
                CurrentState = SlotState.Active;
            }
        }
    }
    public float ActualValueProc {
        get {
            if (MaxValue > 0) {
                return Mathf.Round(((float)ActualValue / MaxValue) * 10000) / 100f;
            } else {
                throw new Exception("MaxValue mustn't be equal to 0!");
            }
        }
        set {
            ActualValue = MaxValue * value / 100f;
        }
    }
    public float UsingMuliplayer { get; set; }

    public GameObject SlotObj { get; set; }
    public PowerEnum Power { get; set; }

    public float TimeToEndInSec;

    public Slot(GameObject gameObj, float maxValue = 100, float timeToEndInSec = 120, float startValueInProc = 100, float usingMulitplayer = 5) {
        SlotObj = gameObj;
        MaxValue = maxValue;
        ActualValueProc = startValueInProc;
        TimeToEndInSec = timeToEndInSec;
        UsingMuliplayer = usingMulitplayer;
    }
    //public Slot(Vector3 position, float maxValue = 100, float timeToEndInSec = 120, float startValueInProc = 100)
    //    : this(maxValue, timeToEndInSec, startValueInProc) {
    //        Position = position;
    //}

    public void Update() {
        if ((CurrentState & SlotState.Active) == SlotState.Active) {
            double deltaValue = MaxValue * (((CurrentState & SlotState.Using) == SlotState.Using) ? Time.deltaTime * UsingMuliplayer : Time.deltaTime) / TimeToEndInSec;
            ActualValue -= deltaValue;
            CurrentState ^= SlotState.Using;
        }
    }

}
