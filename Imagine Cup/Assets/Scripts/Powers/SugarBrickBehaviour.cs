using UnityEngine;
using System.Collections;
using Assets.Utils;

public class SugarBrickBehaviour : MonoBehaviour, ISwitchable {

    private bool _isUsing;
    private float _timerCounter;
    private SpriteRenderer _spriteRenderer;
    private CollisionChecker _collider;
    private PlayerInputHandler _playerInputHandler;

    private ParticleSystem _sbPS;

    public Vector3 startingRelativePosition;
    public GameObject sugarBrickParticlePrefab;

    private SpriteRenderer Sprite_Renderer {
        get {
            if (_spriteRenderer == null) {
                _spriteRenderer = GetComponent<SpriteRenderer>();
            }
            return _spriteRenderer;
        }
    }

    [SerializeField]
    private Vector2 _velocity;
    public Vector2 Velocity {
        get {
            return _velocity;
        }
        set {
            _velocity = value;
        }
    }

    void Awake() {
        _sbPS = ((GameObject)Instantiate(sugarBrickParticlePrefab)).GetComponent<ParticleSystem>();
        _sbPS.transform.parent = transform;
        _sbPS.transform.localPosition = new Vector3(0, 0, transform.position.z);

        _sbPS.Stop();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<CollisionChecker>();
    }

    void Start() {
        transform.position += startingRelativePosition;
        if (_playerInputHandler != null) {
            if (_playerInputHandler.GoingLeft) {
                Velocity = new Vector2(Velocity.x * (-1), Velocity.y);
                transform.position -= new Vector3(2 * startingRelativePosition.x,0,0);
            }
        }
    }

    void Update() {
        if (_isUsing) {

            if (_timerCounter <= _sbPS.duration && _sbPS.isPlaying) {
                _timerCounter = Mathf.Clamp(_timerCounter, 0.0f, _sbPS.duration);
                Sprite_Renderer.material.color = new Color(1, 1, 1, 1 - Mathf.InverseLerp(0, _sbPS.duration, _timerCounter));
                _timerCounter += Time.deltaTime;
            }

            //Debug.Log((_sbPS.isPlaying && Sprite_Renderer.material.color.a < 0.02).ToString() + " " + Sprite_Renderer.material.color.a.ToString());
            if (!_sbPS.isPlaying && Sprite_Renderer.material.color.a < 0.02) {
                transform.DetachChildren(); //jesli player jest przyczepiony do bricka to go odczepia
                                            //obecnie brick nie dziala jak platforma (czyli nie dodaje go jako dziecka
                                            //ale istnieje taka mozliwosc
                Destroy(_sbPS.gameObject); //usuniecie particli
                Destroy(this.gameObject);
            }
            if (_collider.IsOn) {
                collider2D.rigidbody2D.velocity = Velocity / Time.deltaTime;
            }
        }

    }
    public void Use(PlayerInputHandler playerInputHandler) {
        _playerInputHandler = playerInputHandler;
        
        _isUsing = true;
    }

    void ISwitchable.SwitchOn() {
        transform.position = _collider.PreviousPosition;
        Velocity = Vector2.zero;
        collider2D.rigidbody2D.velocity = Velocity;
        collider2D.rigidbody2D.gravityScale = 0;
        _collider.IsOn = false;

        _sbPS.Play();
    }

    void ISwitchable.SwitchOff() {
    }
}
