using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour, Assets.Utils.ISwitchable {

    private bool _isSwitched;
    private bool _isTowards;

    public Transform origin;
    public Transform destination;
    public float speed;

    public bool IsSwitched {//tutaj tez w razie co mozna dodac dodatkowa logike
        get {
            return _isSwitched;
        }
        private set{
            _isSwitched = value;
        }
    }
	void Start () {
        transform.position = origin.position;
	}
	
	void FixedUpdate () {
        if (IsSwitched) {
            if (transform.position == origin.position) {
                _isTowards = true;
            } else if (transform.position == destination.position) {
                _isTowards = false;
            }

            if (_isTowards) {
                transform.position = Vector3.MoveTowards(transform.position, destination.position, speed);
            } else {
                transform.position = Vector3.MoveTowards(transform.position, origin.position, speed);
            }
        }
	}

    void Assets.Utils.ISwitchable.SwitchOn() {
        IsSwitched = true;
    }

    void Assets.Utils.ISwitchable.SwitchOff() {
        IsSwitched = false;
    }
}
