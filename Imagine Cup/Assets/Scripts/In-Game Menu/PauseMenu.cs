using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour
{
	public GUISkin skin;
	
	public Material mat;

	private float savedTimeScale;

	public GameObject pauseFilter;
	private SpriteRenderer shadeSpriteRenderer;
	private MouseScript ms;

	public GameObject start;
	
	public Color statColor = Color.yellow;

	public enum Page {
		None,Main
	}
	
	private Page currentPage;

	void Start() {
		Time.timeScale = 1;
		shadeSpriteRenderer = pauseFilter.GetComponent<SpriteRenderer> ();
		ms = this.gameObject.GetComponent<MouseScript>();
		Texture2D texture = new Texture2D (1, 1);
		texture.SetPixel (0, 0, shadeSpriteRenderer.color);
		Sprite sprite = Sprite.Create (texture, new Rect (0, 0, 2000, 2000), new Vector2 (0, 0));
		shadeSpriteRenderer.sprite = sprite;
		pauseFilter.renderer.enabled = false;
	}

	static bool IsBrowser() {
		return (Application.platform == RuntimePlatform.WindowsWebPlayer ||
		        Application.platform == RuntimePlatform.OSXWebPlayer);
	}
	
	void LateUpdate () {
		if (Input.GetKeyDown(KeyCode.Escape)) 
		{
			switch (currentPage) 
			{
			case Page.None:
				PauseGame(); 
				break;
				
			case Page.Main:
				UnPauseGame();
				break;
				
			default: 
				currentPage = Page.Main;
				break;
			}
		}
	}
	
	void OnGUI () {
		if (skin != null) {
			GUI.skin = skin;
		}
		if (IsGamePaused()) {
			GUI.color = statColor;
			switch (currentPage) {
			case Page.Main: MainPauseMenu(); break;
			}
		}   
	}
	
	void ShowBackButton() {
		if (GUI.Button(new Rect(20, Screen.height - 50, 50, 20),"Back")) {
			currentPage = Page.Main;
		}
	}
	
	void BeginPage(int width, int height) {
		GUILayout.BeginArea( new Rect((Screen.width - width) / 2, (Screen.height - height) / 2, width, height));
	}
	
	void EndPage() {
		GUILayout.EndArea();
		if (currentPage != Page.Main) {
			ShowBackButton();
		}
	}

	void MainPauseMenu() {
		BeginPage(200,200);
		if (GUILayout.Button ("Continue")) {
			UnPauseGame();
		}
		if (GUILayout.Button ("Exit to main menu")) {
			Application.LoadLevel("Menu_MAIN");
			UnPauseGame();
		}
		if (!IsBrowser() && GUILayout.Button ("Exit game")) {
			Application.Quit();
		}
		EndPage();
	}
	
	void PauseGame() {
		ms.DrawCursor = true;
		savedTimeScale = Time.timeScale;
		Time.timeScale = 0;
		AudioListener.pause = true;
		if (pauseFilter)
			pauseFilter.renderer.enabled = true;
		currentPage = Page.Main;
	}
	
	void UnPauseGame() {
		ms.DrawCursor = false;
		Time.timeScale = savedTimeScale;
		AudioListener.pause = false;
		if (pauseFilter)
			pauseFilter.renderer.enabled = false;
		
		currentPage = Page.None;
		
		if (start != null) {
			start.SetActive(true);
		}
	}
	
	bool IsGamePaused() {
		return (Time.timeScale == 0);
	}
	
	void OnApplicationPause(bool pause) {
		if (IsGamePaused()) {
			AudioListener.pause = true;
		}
	}
}