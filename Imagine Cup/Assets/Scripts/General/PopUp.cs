using UnityEngine;
using System.Collections;

public class PopUp : MonoBehaviour {
    public enum PopUpDisapearEffect {
        Transparency,
        CutOffEffect
    }
    public Sprite sprite;
    public Material material;
    public bool isVisibleDebug;
    public PopUpDisapearEffect disapearEffect;
    public float fadingTimeInSec;

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
    [Range(-50,50)]
    public float positionX;
    [Range(-50, 50)]
    public float positionY;
	// Use this for initialization
	void Start () {
        isVisibleDebug = false;
        _popUp = new GameObject();
        _popUp.name = "PopUpChild";
        _popUp.transform.parent = transform;
        _popUp.AddComponent<SpriteRenderer>();
        PopUpSpriteRenderer.sprite = sprite;
        PopUpSpriteRenderer.material = material;
        _popUp.transform.localPosition = new Vector3(positionX, positionY, transform.position.z);

        _applyEffect = false;
	}
	
	// Update is called once per frame
	void Update () {
        _popUp.transform.localPosition = new Vector3(positionX,positionY,transform.position.z);

        if (PopUpSpriteRenderer.sprite != sprite || PopUpSpriteRenderer.material != material) {
            PopUpSpriteRenderer.sprite = sprite;
            PopUpSpriteRenderer.material = material;
        }

        if (!isVisibleDebug) {
            UpdateEffect(0);
        }

        if (_applyEffect) {
            if (_actualValue > 0) {
                _actualValue -= Time.deltaTime;
                UpdateEffect(Mathf.InverseLerp(0, fadingTimeInSec, _actualValue));
                if (_actualValue <=0) {
                    _applyEffect = false;
                }
            }
        }

        if (isVisibleDebug) {
            UpdateEffect(1);
        }
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
    void OnTriggerEnter2D(Collider2D c) {
        if (c.gameObject.layer == LayerMask.NameToLayer("Player")) {
            _applyEffect = true;
            _actualValue = fadingTimeInSec;
        }
    }


}
