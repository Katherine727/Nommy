using UnityEngine;
using System.Collections;

public class ProgressBar : MonoBehaviour {

    private float _actualValue;
    private SpriteRenderer _spriteRenderer;

    public float maxValue;
    //public float deltaValue;

    public SpriteRenderer Sprite_Renderer {
        get {
            if (_spriteRenderer == null) {
                _spriteRenderer = transform.GetComponent<SpriteRenderer>();
            }
            return _spriteRenderer;
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
            ActualValue = Mathf.Lerp(0, maxValue, value / 100.0f);
        }
    }
    
    void Start () {
        ReFill();
        transform.localPosition = Vector3.zero;
	}

    void Update() {
        Sprite_Renderer.material.SetFloat("_CutOff", 1 - Mathf.InverseLerp(0, maxValue, ActualValue));
    }
    public void UseBar(float deltaValue) {
        ActualValue -= deltaValue;
    }

    public void ReFill() {
        ActualValue = maxValue;
    }

}
