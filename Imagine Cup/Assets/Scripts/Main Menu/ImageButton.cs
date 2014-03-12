using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(AudioSource))]
public class ImageButton : MonoBehaviour {

	public enum OnClickActionType { NewGame=0, Options, Exit, Credits, Default };

	public Sprite HoverSprite;
	public Sprite OnClickSprite;
	public OnClickActionType ActionType;

	private SpriteRenderer sr;
	private AudioSource asrc;
	private Sprite defaultSprite;
	private float delay=0.8f;
	private float currentDelay=0;
	private bool launchingLevel = false;

	// Use this for initialization
	void Start () {
		sr = this.GetComponent<SpriteRenderer>();
		asrc = this.GetComponent<AudioSource>();
		defaultSprite = sr.sprite;
	}

	void OnMouseEnter()
	{
		sr.sprite = HoverSprite;
	}

	void OnMouseExit()
	{
		sr.sprite = defaultSprite;
	}

	void OnMouseOver()
	{
		if(Input.GetMouseButtonDown(0)){
			sr.sprite = OnClickSprite;
			asrc.Play();

			switch(ActionType) {
				case OnClickActionType.NewGame:
					launchingLevel=true;
					break;

				case OnClickActionType.Exit:
					Application.Quit();
					break;

				case OnClickActionType.Options:
					break;
				case OnClickActionType.Credits:
					break;
				case OnClickActionType.Default:
					break;
				default:
					break;
			}
		}
	}

	void Update() {
		if(launchingLevel) {
			currentDelay += Time.deltaTime;
			if(currentDelay >= delay) {
				Application.LoadLevel("Tutorial_MAIN");
			}
		}
	}
}
