using UnityEngine;
using Assets.Utils;
using System.Collections.Generic;

[RequireComponent(typeof(AbilityToStartScriptByTag))]
public class Button : MonoBehaviour, ISwitchable {

    private bool _isCollisionWithTrigger;
    private bool _isSwitched;
    private bool _wasUsed;
    private AudioSource asrc;
    private SpriteRenderer _spriteRenderer;

    private SpriteRenderer _SpriteRenderer {
        get {
            if (_spriteRenderer == null) {
                _spriteRenderer = GetComponent<SpriteRenderer>();
            }
            return _spriteRenderer;
        }
    }

    private bool IsSwitched {
        get {
            return _isSwitched;
        }
        set {
            _isSwitched = value; //inaczej niz w switcherze
            if (_isSwitched) {
                _SpriteRenderer.sprite = spriteOn;
                SwitchObjects();
            } else {
                _SpriteRenderer.sprite = spriteOff;
                SwitchObjects();
            }

        }
    }

    private void SwitchObjects() {
        if (IsSwitched) {
            foreach (var el in switchableElements) {
                el.GetComponent<AbilityToStartScript>().StartInterface();
            }
        } else {
            foreach (var el in switchableElements) {
                el.GetComponent<AbilityToStartScript>().StopInterface();
            }
        }
    }

    private bool IsCollisionWithTrigger {//w razie co mozna dodac jakas logike
        get {
            return _isCollisionWithTrigger;
        }
        set {
            _isCollisionWithTrigger = value;
        }
    }

    public Sprite spriteOn;
    public Sprite spriteOff;
    public AudioClip SwitchOnSound;
    public AudioClip SwitchOffSound;
    public List<Transform> switchableElements;

    void Start() {
        IsSwitched = false;
        _wasUsed = false;
    }

    void Awake() {
        if (SwitchOffSound != null || SwitchOnSound != null) {
            asrc = this.gameObject.GetComponent<AudioSource>();
        }
    }

    void Update() {
        if (IsCollisionWithTrigger && !_wasUsed) {
            IsSwitched = !IsSwitched;
            _wasUsed = true;
            if (asrc != null) {
                asrc.clip = IsSwitched ? SwitchOnSound : SwitchOffSound;
                asrc.Play();
            }
        }

    }

    void OnTriggerEnter2D(Collider2D c) {

    }

    void ISwitchable.SwitchOn() {
        IsCollisionWithTrigger = true;
    }

    void ISwitchable.SwitchOff() {
        throw new System.NotImplementedException();
    }
}
