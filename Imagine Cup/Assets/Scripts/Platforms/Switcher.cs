using UnityEngine;
using System.Collections;

using Assets.Utils;
using System.Collections.Generic;
public class Switcher : MonoBehaviour, ISwitchable {

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
            _isSwitched = value;
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
    public bool isSwitchedOnStart;
    public bool isOneWay;
	public AudioClip SwitchOnSound;
	public AudioClip SwitchOffSound;
    public List<Transform> switchableElements;

    void Start() {
        IsSwitched = isSwitchedOnStart;
        _wasUsed = false;
    }

	void Awake() {
		if(SwitchOffSound!=null || SwitchOnSound!= null) {
			asrc = this.gameObject.GetComponent<AudioSource>();
		}
	}

    void Update() {
        if (IsCollisionWithTrigger) {
            if (Input.GetKeyDown(KeyCode.F) && ((isOneWay && !_wasUsed) || !isOneWay)) {
                IsSwitched = !IsSwitched;
                _wasUsed = true;
				if(asrc!=null) {
					asrc.clip = IsSwitched ? SwitchOnSound : SwitchOffSound;
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
