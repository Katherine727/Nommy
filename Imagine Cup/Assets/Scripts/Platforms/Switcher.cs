using UnityEngine;
using System.Collections;

using Assets.Utils;
using System.Collections.Generic;

public class Switcher : MonoBehaviour, ISwitchable {

	private AudioSource asrc;
    private bool _isCollisionWithTrigger;
    private bool _isSwitched;
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
            if (value) {
                _SpriteRenderer.sprite = spriteOn;
                SwitchObjects();
            } else {
                _SpriteRenderer.sprite = spriteOff;
                SwitchObjects();
            }
            
            _isSwitched = value;
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
    public bool isSwitchedOnStart;
    public List<Transform> switchableElements;
	public AudioClip SwitchOnSound;
	public AudioClip SwitchOffSound;

    void Start() {
        IsSwitched = isSwitchedOnStart;
    }

	void Awake() {
		if(SwitchOnSound!= null || SwitchOffSound != null)
			asrc = this.gameObject.GetComponent<AudioSource>();
	}

    void Update() {

        if (IsCollisionWithTrigger) {
            if (Input.GetKeyDown(KeyCode.F)) {
                IsSwitched = !IsSwitched;
				if(asrc!= null) {
					asrc.clip = IsSwitched ? SwitchOffSound : SwitchOnSound;
					asrc.Play();
				}
            }
        }

    }

    void ISwitchable.SwitchOn() {
        IsCollisionWithTrigger = true;
    }

    void ISwitchable.SwitchOff() {
        IsCollisionWithTrigger = false;
    }
}
