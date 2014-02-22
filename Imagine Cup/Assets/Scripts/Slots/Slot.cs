using UnityEngine;
using System;
using System.Linq;

using Assets.Utils;

public class Slot : MonoBehaviour {


    private bool _isActive;
    private bool _isFull;
    private bool _isActivated;
    private float _actualValue;
    private float _actualValueProc;
    private SpriteRenderer spriteRenderer;
    private SpriteRenderer childSpriteRenderer;

    private SlotModel _model;

    private SpriteRenderer Sprite_Renderer {
        get {
            if (spriteRenderer == null) {
                spriteRenderer = transform.GetComponent<SpriteRenderer>();
            }
            return spriteRenderer;
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
                var proc = ActualValueProc / 100;
                ActualValue = value.timeToEndInSec * proc;
            }
            _model = value;
            Sprite_Renderer.sprite = _model.spriteRing;
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
            if (ActualValue != Model.timeToEndInSec && _isFull == true) {
                ActualValue = Model.timeToEndInSec;
            }
        }
    }
    [HideInInspector]
    public bool IsActivated {
        get { return _isActivated; }
        set { 
            _isActivated = value;
            if (_isActivated) {
                childSpriteRenderer.sprite = Model.spriteFaceActivated;
            } else {
                childSpriteRenderer.sprite = Model.spriteFaceDeactivated;
            }
        }
    }

    [HideInInspector]
    public float ActualValue {
        get {
            return _actualValue;
        }
        private set {

            if (Model.timeToEndInSec > 0) {
                _actualValue = Mathf.Clamp(value, 0, Model.timeToEndInSec);
            } else {
                throw new Exception("Time mustn't be equal to 0!");
            }
            IsFull = (_actualValue == Model.timeToEndInSec) ? true : false;
            IsActive = (_actualValue > 0) ? true : false;
        }
    }

    [HideInInspector]
    public float ActualValueProc {
        get {
            if (Model.timeToEndInSec > 0) {
                _actualValueProc = Mathf.InverseLerp(0, Model.timeToEndInSec, ActualValue) * 100;
                return _actualValueProc;
            } else {
                throw new Exception("Time mustn't be equal to 0!");
            }
        }
        set {
            ActualValue = Model.timeToEndInSec * value / 100f;
        }
    }

    [HideInInspector]
    public PowerEnum Power {
        get { return Model.power; }
        set {
            if (transform.parent != null) {
                ChangeModel(value, transform.parent);
            } else {
                ChangeModel(value, transform);
            }
            _model.power = value;
            Model = _model;
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
        //'Wierzch' slotu 
        GameObject childObjectFace = new GameObject();
        childObjectFace.name = "TheFace";
        childObjectFace.transform.parent = transform;
        childObjectFace.transform.localPosition = Vector3.zero;
        childSpriteRenderer = childObjectFace.AddComponent<SpriteRenderer>();
        childSpriteRenderer.sprite = Model.spriteFaceDeactivated;
        childSpriteRenderer.sortingLayerName = "Slots";
        childSpriteRenderer.sortingOrder = 1;

        //Tlo slotu
        GameObject childObjectBg = new GameObject();
        childObjectBg.name = "SlotBackgroud";
        childObjectBg.transform.parent = transform;
        childObjectBg.transform.localPosition = Vector3.zero;
        var childSpriteBgRenderer = childObjectBg.AddComponent<SpriteRenderer>();
        childSpriteBgRenderer.sprite = Model.background;
        childSpriteBgRenderer.sortingLayerName = "Slots";
        childSpriteBgRenderer.sortingOrder = 0;
    }

    void Update() {
        if (IsActive) {
            double deltaValue = Time.deltaTime * Model.usingMultiPlayer;
            ActualValue -= (float)deltaValue;
        }
        Sprite_Renderer.material.SetFloat("_CutOff", 1 - Mathf.InverseLerp(0, Model.timeToEndInSec, ActualValue));
    }
    public void ChangeModel(PowerEnum power, Transform obj) {
        SlotModelProvider sm = obj.GetComponent<SlotModelProvider>();
        if (sm != null && sm.models.Count > 0) {
            Model = sm.models.Where(m => m.power == power).FirstOrDefault();
        }
    }
    public void ChangeModel(PowerEnum power, string name, Transform obj) {
        SlotModelProvider sm = obj.GetComponent<SlotModelProvider>();
        if (sm != null && sm.models.Count > 0) {
            Model = sm.models.Where(m => m.power == power && m.name == name).FirstOrDefault();

        }
    }

    public void ChangeModel(SlotModel model) {
        Model = model;
    }

}
