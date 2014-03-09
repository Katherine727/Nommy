using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollisionChecker : MonoBehaviour {
    public enum ColliderCheckerType {
        RectangleArea,
        CircleArea
    }
    public ColliderCheckerType colliderCheckerType;

    public Vector3 PreviousPosition {
        get {
            return _prevPosition;
        }
        private set {
            _prevPosition = value;
        }
    }

    public bool IsOn { get; set; }

    private Vector3 _prevPosition;
    private AbilityToStartScript _scriptStartInterface;

    private Vector2 _UpperLeftCorner {
        get {
            return new Vector2(collider2D.renderer.bounds.center.x - collider2D.renderer.bounds.size.x / 2f,
                collider2D.renderer.bounds.center.y + collider2D.renderer.bounds.size.y / 2f);
        }
    }

    private Vector2 _LowerRightCorner {
        get {
            return new Vector2(collider2D.renderer.bounds.center.x + collider2D.renderer.bounds.size.x / 2f,
                            collider2D.renderer.bounds.center.y - collider2D.renderer.bounds.size.y / 2f);
        }
    }
    void Start() {
        IsOn = true;
        PreviousPosition = transform.position;
        _scriptStartInterface = GetComponent<AbilityToStartScript>();
    }

    // Update is called once per frame
    void Update() {
        if (IsOn) {
            int isAnyCollision = 0;
            Collider2D[] collider = new Collider2D[2];
            switch (colliderCheckerType) {
                case ColliderCheckerType.RectangleArea:

                    //metoda nie alokuje pamieci, bierze tylko to co jej dalismy (wychodzi na to, ze zwracany wynik tez jest zalezny od tej tablicy
                    //jaka mu przekazemy, pewnie zwraca cos w rodzaju array.Length
                    isAnyCollision = Physics2D.OverlapAreaNonAlloc(_UpperLeftCorner, _LowerRightCorner, collider, 1 << this.gameObject.layer);

                    break;
                case ColliderCheckerType.CircleArea:
                    isAnyCollision = Physics2D.OverlapCircleNonAlloc(collider2D.renderer.bounds.center, collider2D.renderer.bounds.size.x / 2, null, 1 << this.gameObject.layer);
                    break;
                default:
                    return;
            }
            //na to wyglada, ze on sam siebie tez bierze pod uwage O.o
            if (isAnyCollision > 1) {
                isAnyCollision = 0;
                _scriptStartInterface.StartInterface();
            }
            PreviousPosition = transform.position; //rozwiazanie troche na skroty, ale w tym momencie chyba najlepsze :D
                                                   //inaczej bym musial sie dostawac do wszystkich elementow, z ktorym obiekt ma kolizje
                                                   //i wyznaczac dokladniejsza pozycje
        }
    }
}
