﻿using UnityEngine;
using System.Collections;

public class ProgressBar : MonoBehaviour {

    public enum BarType { //not working yet
        HorizontalFromLeftToRight,
        HorizontalFromRightToLeft,
        VerticalFromUpToDown,
        VerticalFromDownToUp
    }

    private float _actualValue;
    private GameObject _bgObj;
    private GameObject _fgObj;
    private SpriteRenderer _spriteRendererBG;
    private SpriteRenderer _spriteRendererFG;

    public float maxValue;
    public BarType barType;
    public Sprite spriteProgressBarFG;
    public Sprite spriteProgressBarBG;

    public SpriteRenderer SpriteRendererBG {
        get {
            if (_spriteRendererBG == null) {
                _spriteRendererBG = _bgObj.GetComponent<SpriteRenderer>();
            }
            return _spriteRendererBG;
        }
    }

    public SpriteRenderer SpriteRendererFG {
        get {
            if (_spriteRendererFG == null) {
                _spriteRendererFG = _fgObj.GetComponent<SpriteRenderer>();
            }
            return _spriteRendererFG;
        }
    }

    public float ActualValue {
        get {
            return _actualValue;
        }
        private set {
            _actualValue = Mathf.Clamp(value, 0, maxValue);
        }
    }

    public float ActualValueInProc {
        get {
            return Mathf.InverseLerp(0,maxValue,ActualValue)*100;
        }
        set {
            ActualValue = Mathf.Lerp(0, maxValue, value * 0.01f);
        }
    }
    void Awake() {
        //bg
        transform.localPosition = Vector3.zero;
        _bgObj = new GameObject();
        _spriteRendererBG = _bgObj.AddComponent<SpriteRenderer>();
        SpriteRendererBG.sprite = spriteProgressBarBG;
        _bgObj.transform.localPosition = new Vector3(0, 0, transform.position.z);
        _bgObj.transform.parent = transform;
        _bgObj.name = "ProgressBarBG";
        _bgObj.layer = LayerMask.NameToLayer("GUI");
        SpriteRendererBG.sortingLayerName = "ProgressBar";
        SpriteRendererBG.sortingOrder = 0;
        
        //fg
        transform.localPosition = Vector3.zero;
        _fgObj = new GameObject();
        _spriteRendererFG = _fgObj.AddComponent<SpriteRenderer>();
        SpriteRendererFG.sprite = spriteProgressBarFG;
        SpriteRendererFG.material = new Material(Resources.Load("Materials/ProgressBar") as Material);
        _fgObj.transform.parent = transform;
        _fgObj.transform.localPosition = new Vector3(0, 0, transform.position.z);
        _fgObj.name = "ProgressBarFG";
        _bgObj.layer = LayerMask.NameToLayer("GUI");
        SpriteRendererFG.sortingLayerName = "ProgressBar";
        SpriteRendererFG.sortingOrder = 1;
        
        
    }
    void Start () {
        ReFill();
	}

    void Update() {
        _bgObj.transform.rotation = Quaternion.identity;
        _fgObj.transform.rotation = Quaternion.identity;
        _bgObj.transform.Rotate(Vector3.forward, RotationValue());
        _fgObj.transform.Rotate(Vector3.forward, RotationValue());
        SpriteRendererFG.material.SetFloat("_Progress", Mathf.InverseLerp(0, maxValue, ActualValue));
    }
    public void UseBar(float deltaValue) {
        ActualValue -= deltaValue;
    }

    public void ReFill() {
        ActualValue = maxValue;
    }

    public void ChangeLocalPosition(Vector3 newPos) {
        _bgObj.transform.localPosition = newPos;
        _fgObj.transform.localPosition = newPos;
    }

    private float RotationValue() {
        switch (barType) {
            case BarType.HorizontalFromLeftToRight:
                return 0;
            case BarType.HorizontalFromRightToLeft:
                return 180;
            case BarType.VerticalFromUpToDown:
                return 90;
            case BarType.VerticalFromDownToUp:
                return 270;
            default:
                return 0;
        }
    }

}
