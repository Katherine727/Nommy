using UnityEngine;
using System;
using System.Linq;

using Assets.Utils;

public class Slot : MonoBehaviour {


    private bool _isActive;
    private bool _isFull;
    private bool _isActivated;
    private bool _isUsing;
    private ProgressBar _progressBar;
    private SpriteRenderer _foregroundSpriteRenderer;
    private PowerEnum _power;
    private SlotModel _model;

    private ProgressBar Progress_Bar {
        get {
            if (_progressBar == null) {
                _progressBar = transform.GetComponent<ProgressBar>();
                if (_progressBar == null) { //komponent musi istniec
                    throw new MissingComponentException("You have to add ProgressBar component to use Slots.");
                }
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
            if (_model == null) { //niesli nie ma gotowego modelu, tworzymy domyślny
                _model = new SlotModel();
            }
            return _model;
        }
        set {
            //Jesli zmieniamy model, mozemy zmienic tez maksymalna wartosc, co moze powodowac zepsucie wartosci
            //aktualnej. Takze jesli wartosci maksymalne sa rozne, dostosowujemy wartosc aktualna.
            if (_model != null && value.timeToEndInSec != _model.timeToEndInSec) {
                ActualValue = Mathf.Clamp(ActualValue, 0,value.timeToEndInSec);
            }
            _model = value;
            Sprite_Renderer.sprite = _model.spriteProgressBar; //podczepienie 'licznika'
            Progress_Bar.maxValue = _model.timeToEndInSec;
            Progress_Bar.ReFill(); //wypelnienie licznika
        }
    }

    [Range(0,1)]
    public float opacityDeactivatedSlot;

    /// <summary>
    /// Value gives a multiplayer which multiplays delta value during the update. Multiplayer depends on IsUsing flag.
    /// </summary>
    public float UsingMulitplayer{
        get {
            if (IsUsing) {
                return Model.usingMultiPlayer;
            } else {
                return 1;
            }
        }
    }
    /// <summary>
    /// It is a flag, saying that given slot is active (created and filled with data - ready to use).
    /// </summary>
    [HideInInspector]
    public bool IsActive {
        get { return _isActive; }
        private set { 
            _isActive = value;
        }
    }

    /// <summary>
    /// Flag saying, if progress bar is full or not
    /// </summary>
    [HideInInspector]
    public bool IsFull {
        get { return _isFull; }
        set {
            _isFull = value;
            //kiedy ustawimy te flage na true, zapleniamy licznik
            if (ActualValue !=Progress_Bar.maxValue && _isFull == true) {
                ActualValue = Progress_Bar.maxValue;
            }
        }
    }

    /// <summary>
    /// Flag saying if given slot is selected by user.
    /// </summary>
    [HideInInspector]
    public bool IsActivated {
        get { return _isActivated; }
        set { 
            _isActivated = value;
            if (_isActivated) {
                _foregroundSpriteRenderer.sprite = Model.foreground;
                _foregroundSpriteRenderer.material.color = new Color(1f, 1f, 1f, 0f);
            } else {
                _foregroundSpriteRenderer.sprite = Model.foreground;
                _foregroundSpriteRenderer.material.color = new Color(1f, 1f, 1f, opacityDeactivatedSlot);
            }
        }
    }

    /// <summary>
    /// Flag indicates if slot is in 'using' mode. It'll be set if only IsActivated flag is set. Otherwise it will not.
    /// </summary>
    public bool IsUsing {
        get {
            return _isUsing;
        }
        set{
            if (IsActivated) {
                _isUsing = value;
            }
        }
    }

    /// <summary>
    /// Actual value of slot. It also changes flags such as IsFull and IsActive.
    /// </summary>
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
        }
    }

    /// <summary>
    /// Actual value but as a percentage of max value.
    /// </summary>
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

    /// <summary>
    /// Power type assigned to slot. If you change power type, it will also change model.
    /// </summary>
    [HideInInspector]
    public PowerEnum Power {
        get { return Model.power; }
        set {
            try {
                if (transform.parent != null) {
                    ChangeModel(value, transform.parent);
                } else {
                    ChangeModel(value, transform); // jak nie ma parenta, to proba sprawdzenia samego siebie
                }
            } catch (ArgumentNullException) {
                throw new ArgumentNullException("There is no model for given power type!");
            }
        }
    }

    /// <summary>
    /// Width of slot, but given by width of sprite which shows progress bar by itself.
    /// </summary>
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

    /// <summary>
    /// Height of slot, but given by height of sprite which shows progress bar by itself.
    /// </summary>
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
        //Tlo slotu
        GameObject childObjectBg = new GameObject();
        childObjectBg.name = "SlotBackgroud";
        childObjectBg.transform.parent = transform;
        childObjectBg.transform.localPosition = Vector3.zero;
        var childSpriteBgRenderer = childObjectBg.AddComponent<SpriteRenderer>();
        childSpriteBgRenderer.sprite = Model.background;
        childSpriteBgRenderer.sortingLayerName = "Slots";
        childSpriteBgRenderer.sortingOrder = 0;
        
        //Progress Bar
        Progress_Bar.maxValue = Model.timeToEndInSec;
        Progress_Bar.Sprite_Renderer.sprite = Model.spriteProgressBar;
        Progress_Bar.transform.parent = transform;
        Progress_Bar.transform.localPosition = Vector3.zero;
        Progress_Bar.Sprite_Renderer.sortingLayerName = "Slots";
        Progress_Bar.Sprite_Renderer.sortingOrder = 1;

        //Ikona
        GameObject childObjectIcon = new GameObject();
        childObjectIcon.name = "SlotIcon";
        childObjectIcon.transform.parent = transform;
        childObjectIcon.transform.localPosition = Vector3.zero;
        var childSpriteIconRenderer = childObjectIcon.AddComponent<SpriteRenderer>();
        childSpriteIconRenderer.sprite = Model.icon;
        childSpriteIconRenderer.sortingLayerName = "Slots";
        childSpriteIconRenderer.sortingOrder = 2;
       
        //'Wierzch' slotu 
        GameObject childObjectForeground = new GameObject();
        childObjectForeground.name = "TheFace";
        childObjectForeground.transform.parent = transform;
        childObjectForeground.transform.localPosition = Vector3.zero;
        _foregroundSpriteRenderer = childObjectForeground.AddComponent<SpriteRenderer>();
        _foregroundSpriteRenderer.sprite = Model.foreground;
        _foregroundSpriteRenderer.material.color = new Color(1f, 1f, 1f, opacityDeactivatedSlot);
        _foregroundSpriteRenderer.sortingLayerName = "Slots";
        _foregroundSpriteRenderer.sortingOrder = 3;

        IsActivated = false;
        IsActive = true;

    }

    void Update() {
        if (IsActive) {
            double deltaValue = Time.deltaTime * UsingMulitplayer;
            ActualValue -= (float)deltaValue;
        }
        if (!IsActivated) {
            _foregroundSpriteRenderer.material.color = new Color(1f, 1f, 1f, opacityDeactivatedSlot); 
        }
    }

    /// <summary>
    /// Method to change slot's model
    /// </summary>
    /// <param name="power">Power type</param>
    /// <param name="obj">Object Transform of component which has SlotModelProvider component</param>
    public void ChangeModel(PowerEnum power, Transform obj) {
        SlotModelProvider sm = obj.GetComponent<SlotModelProvider>();
        if (sm != null && sm.models.Count > 0) {
            Model = sm.models.Where(m => m.power == power).First();
        }
    }

    /// <summary>
    /// Method to change slot's model
    /// </summary>
    /// <param name="power">Power type</param>
    /// <param name="name">Name of slot - not important at this time</param>
    /// <param name="obj">Object Transform of component which has SlotModelProvider component</param>
    public void ChangeModel(PowerEnum power, string name, Transform obj) {
        SlotModelProvider sm = obj.GetComponent<SlotModelProvider>();
        if (sm != null && sm.models.Count > 0) {
            Model = sm.models.Where(m => m.power == power && m.name == name).First();

        }
    }

    /// <summary>
    /// Method to change slot's model
    /// </summary>
    /// <param name="model">Ready model</param>
    public void ChangeModel(SlotModel model) {
        Model = model;
    }

}
