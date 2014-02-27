using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(AudioSource))]
public class ImageButton : MonoBehaviour {
	
	public Sprite HoverSprite;
	public Sprite OnClickSprite;

	private SpriteRenderer sr;
	private AudioSource asrc;
	private Sprite defaultSprite;

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
		}
	}
}
