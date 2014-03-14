using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CandySpittingBehaviour : MonoBehaviour {

    private bool _isUsing;
    private float _timerCounter;
    private SpriteRenderer _spriteRenderer;
    private PlayerInputHandler _playerInputHandler;
    private CharacterController2D _CC2D;


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
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _CC2D = GetComponent<CharacterController2D>();
    }

    void Start() {
        if (_playerInputHandler != null) {
            if (_playerInputHandler.GoingLeft) {
                Velocity = new Vector2(Velocity.x * (-1), Velocity.y);
            }
        }
    }

    void Update() {
        if (_isUsing) {


            _CC2D.move(new Vector3(Velocity.x * Time.deltaTime * 5, Velocity.y, 0));
        }

    }
    public void Use(PlayerInputHandler playerInputHandler) {
        _playerInputHandler = playerInputHandler;

        _isUsing = true;
    }

    void OnTriggerEnter2D(Collider2D c) {
        if (tags.Count > 0 && tags.Contains(c.tag)) {
            Velocity = Vector2.zero;
            _CC2D.velocity = Velocity;
            collider2D.rigidbody2D.gravityScale = 0;
            
        }
        Destroy(this.gameObject);
    }
    void OnCollisionEnter2D(Collision2D c) {
        if (tags.Count > 0 && !tags.Contains(c.gameObject.tag)) {
            Destroy(this.gameObject);
        }
    }
}
