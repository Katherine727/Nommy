using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour, Assets.Utils.ISwitchable {

    public enum WhereIsHeadingEnum {
        NOWHERE,
        Origin,
        Destintaion
    }

    private bool _isSwitched;

    public Transform origin;
    public Transform destination;
    public float speed;
    public bool isOneWayTickiet;

    public WhereIsHeadingEnum WhereIsHeading { get; private set; }

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
        WhereIsHeading = WhereIsHeadingEnum.NOWHERE;
	}
	
	void Update () {
		if(Time.deltaTime == 0.0f)
			return;

        if (IsSwitched) {
            if (isOneWayTickiet && ((transform.position == origin.position && WhereIsHeading == WhereIsHeadingEnum.Origin)
                                 || (transform.position == destination.position && WhereIsHeading == WhereIsHeadingEnum.Destintaion)) ) {
                IsSwitched = false;
                return;
            }
            if (transform.position == origin.position) {
                WhereIsHeading = WhereIsHeadingEnum.Destintaion;
            } else if (transform.position == destination.position) {
                WhereIsHeading = WhereIsHeadingEnum.Origin;
            }


            if (WhereIsHeading == WhereIsHeadingEnum.Destintaion) {
                transform.position = Vector3.MoveTowards(transform.position, destination.position, speed);
            } else {
                transform.position = Vector3.MoveTowards(transform.position, origin.position, speed);
            }
        }
	}

    void Assets.Utils.ISwitchable.SwitchOn() {
        IsSwitched = true;
        if (isOneWayTickiet && (transform.position == origin.position ||transform.position == destination.position)) {
            WhereIsHeading = WhereIsHeadingEnum.NOWHERE; 
        }
    }

    void Assets.Utils.ISwitchable.SwitchOff() {
        IsSwitched = false;
    }
}
