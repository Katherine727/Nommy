using UnityEngine;
using System.Collections;

public class MouseScript : MonoBehaviour {

	private int cursorWidth = 32;
	private int cursorHeight = 32;

	public Texture2D CursorDefaultTexture;
	public Texture2D CursorHoverTexture;
	public bool DrawCursor = true;

	public bool isCursorHover = false;

	void Start()
	{
		Screen.showCursor = false; //wylaczamy domyslny kursor myszy w okienku gry
	}
	
	
	void OnGUI()
	{
		if(DrawCursor) {
			GUI.DrawTexture(new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, cursorWidth, cursorHeight), isCursorHover ? CursorHoverTexture : CursorDefaultTexture);
		}
	}
}
