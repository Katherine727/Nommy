using UnityEngine;
using System.Collections;
using Assets.Utils;
public class PopUp : MonoBehaviour, ISwitchable {
    public enum PopUpDisapearEffect {
        Transparency,
        CutOffEffect
    }
    public bool startWithEffect;
    public bool endWithEffect;
    public Sprite sprite;
    public Material material;
    public PopUpDisapearEffect disapearEffect;
    public float fadingTimeInSec;
    public float apearingTimeInSec;

    private float maxTime; // aktualny czas efektu (fading albo apearing) w zaleznosci od flagi _isApearing
    private bool _isApearing; //tryb aktualnego pokazyania, czy sie wylania, czy chowa 
    private float _actualValue;
    private bool _applyEffect;
    private SpriteRenderer _popUpSpriteRenderer;
    private GameObject _popUp;
    private SpriteRenderer PopUpSpriteRenderer {
        get {
            if (_popUpSpriteRenderer == null && _popUp != null) {
                _popUpSpriteRenderer = _popUp.GetComponent<SpriteRenderer>();
            }
            return _popUpSpriteRenderer;
        }
    }
    [Range(-30,30)]
    public float positionX;
    [Range(-30, 30)]
    public float positionY;
	// Use this for initialization
	void Start () {
        _popUp = new GameObject();
        _popUp.name = "PopUpChild";
        _popUp.transform.parent = transform;
        _popUp.AddComponent<SpriteRenderer>();
        PopUpSpriteRenderer.sprite = sprite;
        PopUpSpriteRenderer.material = material;
        _popUp.transform.localPosition = new Vector3(positionX, positionY, transform.position.z);

        _applyEffect = false;
        _isApearing = true;
        UpdateEffect(0);
	}
	
	void Update () {
        maxTime = _isApearing ? apearingTimeInSec : fadingTimeInSec;
        _popUp.transform.localPosition = new Vector3(positionX,positionY,transform.position.z);

        if (_applyEffect) {
            if (_actualValue >= 0) {
                if (endWithEffect && !_isApearing) {
                    _actualValue -= Time.deltaTime; 
                }
                if (startWithEffect && _isApearing) {
                    _actualValue += Time.deltaTime;
                }
                if (_actualValue <= 0 || _actualValue > maxTime) {
                    _applyEffect = false;
                }
            }
        }
        PopUpSpriteRenderer.sprite = sprite;
        PopUpSpriteRenderer.material = material;
        _actualValue = Mathf.Clamp(_actualValue, 0, maxTime);
        UpdateEffect(Mathf.InverseLerp(0, maxTime, _actualValue));
	}
    private void UpdateEffect(float value) {
        if (disapearEffect == PopUpDisapearEffect.CutOffEffect) {
            PopUpSpriteRenderer.material.SetFloat("_CutOff", 1-value);
            PopUpSpriteRenderer.material.color = new Color(1, 1, 1, 1);
        } else {
            PopUpSpriteRenderer.material.SetFloat("_CutOff", 0);
            PopUpSpriteRenderer.material.color = new Color(1, 1, 1, value);
        }
    }

    void ISwitchable.SwitchOn() {
        _isApearing = true;
        if (startWithEffect) {
            var proc = Mathf.InverseLerp(0, maxTime, _actualValue);
            _actualValue = apearingTimeInSec * proc;
            _applyEffect = true;
        } else {
            _applyEffect = false;
            _actualValue = apearingTimeInSec;
        }
    }

    void ISwitchable.SwitchOff() {
        _isApearing = false;
        if (endWithEffect) {
            var proc = Mathf.InverseLerp(0, maxTime, _actualValue);
            _actualValue = fadingTimeInSec * proc;
            _applyEffect = true;
        } else {
            _applyEffect = false;
            _actualValue = 0;
        }
    }
}
