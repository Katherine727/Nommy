using UnityEngine;
using System;
using System.Linq;

using Assets.Utils;

public class Slot : MonoBehaviour {


    private bool _isActive;
    private bool _isFull;
    private bool _isActivated;
    private ProgressBar _progressBar;
    private SpriteRenderer _childSpriteRenderer;
    private PowerEnum _power;
    private SlotModel _model;

    private ProgressBar Progress_Bar {
        get {
            if (_progressBar == null) {
                _progressBar = transform.GetComponent<ProgressBar>();
            }
            return _progressBar;
        }
    }
    private SpriteRenderer Sprite_Renderer {
        get {
            return Progress_Bar.Sprite_Renderer;
        }
    }

    private SlotModel Model {
        get {
            if (_model == null) {
                _model = new SlotModel();
            }
            return _model;
        }
        set {
            if (_model != null && value.timeToEndInSec != _model.timeToEndInSec) {
                ActualValue = Mathf.Lerp(0,value.timeToEndInSec,ActualValueProc/100);
            }
            _model = value;
            Sprite_Renderer.sprite = _model.spriteRing;
            Progress_Bar.maxValue = _model.timeToEndInSec;
            Progress_Bar.ReFill();
        }
    }


    [HideInInspector]
    public bool IsActive {
        get { return _isActive; }
        set { _isActive = value; }
    }
    [HideInInspector]
    public bool IsFull {
        get { return _isFull; }
        set {
            _isFull = value;
            if (ActualValue !=Progress_Bar.maxValue && _isFull == true) {
                ActualValue = Progress_Bar.maxValue;
            }
        }
    }
    [HideInInspector]
    public bool IsActivated {
        get { return _isActivated; }
        set { 
            _isActivated = value;
            if (_isActivated) {
                _childSpriteRenderer.sprite = Model.spriteFaceActivated;
            } else {
                _childSpriteRenderer.sprite = Model.spriteFaceDeactivated;
            }
        }
    }

    [HideInInspector]
    public float ActualValue {
        get {
            return Progress_Bar.ActualValue;
        }
        private set {

            if (Progress_Bar.maxValue > 0) {
                Progress_Bar.UseBar(Progress_Bar.ActualValue - value);
            } else {
                throw new Exception("Time mustn't be equal to 0!");
            }
            IsFull = (Progress_Bar.ActualValue == Progress_Bar.maxValue) ? true : false;
            IsActive = (Progress_Bar.ActualValue > 0) ? true : false;
        }
    }

    [HideInInspector]
    public float ActualValueProc {
        get {
            if (Model.timeToEndInSec > 0) {
                return Progress_Bar.ActualValueInProc;
            } else {
                throw new Exception("Time mustn't be equal to 0!");
            }
        }
        set {
            ActualValue = Model.timeToEndInSec * value / 100f;
        }
    }

    [HideInInspector]
    public PowerEnum Power { //nie powinien korzystac z modelu
        get { return Model.power; }
        set {
            try {
                if (transform.parent != null) {
                    ChangeModel(value, transform.parent);
                } else {
                    ChangeModel(value, transform);
                }
            } catch (ArgumentNullException) {
                throw new ArgumentNullException("There is no model for given power type!");
            }
        }
    }

    [HideInInspector]
    public float Width {
        get {
            if (Sprite_Renderer != null) {
                return Sprite_Renderer.bounds.size.x;
            } else {
                return 0;
            }
        }
    }

    [HideInInspector]
    public float Height {
        get {
            if (Sprite_Renderer != null) {
                return Sprite_Renderer.bounds.size.y;
            } else {
                return 0;
            }
        }
    }
    void Start() {
        //Progress Bar
        Progress_Bar.maxValue = Model.timeToEndInSec;
        Progress_Bar.Sprite_Renderer.sprite = Model.spriteRing;
        Progress_Bar.transform.parent = transform;
        Progress_Bar.transform.localPosition = Vector3.zero;
        Progress_Bar.Sprite_Renderer.sortingLayerName = "Slots";
        Progress_Bar.Sprite_Renderer.sortingOrder = 1;


        //'Wierzch' slotu 
        GameObject childObjectFace = new GameObject();
        childObjectFace.name = "TheFace";
        childObjectFace.transform.parent = transform;
        childObjectFace.transform.localPosition = Vector3.zero;
        _childSpriteRenderer = childObjectFace.AddComponent<SpriteRenderer>();
        _childSpriteRenderer.sprite = Model.spriteFaceDeactivated;
        _childSpriteRenderer.sortingLayerName = "Slots";
        _childSpriteRenderer.sortingOrder = 1;

        //Tlo slotu
        GameObject childObjectBg = new GameObject();
        childObjectBg.name = "SlotBackgroud";
        childObjectBg.transform.parent = transform;
        childObjectBg.transform.localPosition = Vector3.zero;
        var childSpriteBgRenderer = childObjectBg.AddComponent<SpriteRenderer>();
        childSpriteBgRenderer.sprite = Model.background;
        childSpriteBgRenderer.sortingLayerName = "Slots";
        childSpriteBgRenderer.sortingOrder = 0;

        Debug.Log(Progress_Bar.ActualValue);
    }

    void Update() {
        if (IsActive) {
            double deltaValue = Time.deltaTime * Model.usingMultiPlayer;
            ActualValue -= (float)deltaValue;
        }
        
        //Sprite_Renderer.material.SetFloat("_CutOff", 1 - Mathf.InverseLerp(0, Model.timeToEndInSec, ActualValue));
    }
    public void ChangeModel(PowerEnum power, Transform obj) {
        SlotModelProvider sm = obj.GetComponent<SlotModelProvider>();
        if (sm != null && sm.models.Count > 0) {
            Model = sm.models.Where(m => m.power == power).First();
        }
    }
    public void ChangeModel(PowerEnum power, string name, Transform obj) {
        SlotModelProvider sm = obj.GetComponent<SlotModelProvider>();
        if (sm != null && sm.models.Count > 0) {
            Model = sm.models.Where(m => m.power == power && m.name == name).First();

        }
    }

    public void ChangeModel(SlotModel model) {
        Model = model;
    }

}
