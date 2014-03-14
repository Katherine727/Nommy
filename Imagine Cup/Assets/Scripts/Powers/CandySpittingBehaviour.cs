using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CandySpittingBehaviour : MonoBehaviour {

    private bool _isUsing;
    private bool _wasCollision;
    private SpriteRenderer _spriteRenderer;
    private PlayerInputHandler _playerInputHandler;
    private ParticleSystem _spPS;
    private CharacterController2D _CC2D;

    public GameObject candyParticlePrefab;
    private SpriteRenderer Sprite_Renderer {
        get {
            return _spriteRenderer;
        }
    }

    [SerializeField]
    private Vector2 _velocity;

    public List<string> tags;
    public Vector2 Velocity {
        get {
            return _velocity;
        }
        set {
            _velocity = value;
        }
    }

    void Awake() {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _spPS = ((GameObject)Instantiate(candyParticlePrefab)).GetComponent<ParticleSystem>();
        _spPS.transform.parent = transform;
        _spPS.transform.localPosition = new Vector3(0.15f, 0, transform.position.z + 0.1f);
        _spPS.Stop();
        _CC2D = GetComponent<CharacterController2D>();
    }

    void Start() {
        _wasCollision = false;
        if (_playerInputHandler != null) {
            if (_playerInputHandler.GoingLeft) {
                Velocity = new Vector2(Velocity.x * (-1), Velocity.y);
            }
        }
    }

    void Update() {
        if (_isUsing) {
            if (_wasCollision && !_spPS.isPlaying) {
                Destroy(_spPS.gameObject); //usuniecie particli
                Destroy(this.gameObject);
            }
            _CC2D.move(new Vector3(Velocity.x * Time.deltaTime * 5, Velocity.y, 0));
        }

    }
    public void Use(PlayerInputHandler playerInputHandler) {
        _playerInputHandler = playerInputHandler;

        _isUsing = true;
    }
    void OnTriggerEnter2D(Collider2D c) {
        var startScript = c.gameObject.GetComponent<AbilityToStartScriptByTag>();
        if (startScript != null) {
            startScript.StartInterface(this.tag);
        }
    }
    void OnCollisionEnter2D(Collision2D c) {
        Velocity = Vector2.zero;
        _CC2D.velocity = Velocity;
        collider2D.rigidbody2D.gravityScale = 0;
        Sprite_Renderer.material.color = new Color(1, 1, 1, 0);
        _wasCollision = true;
        _spPS.Play();
    }
}
