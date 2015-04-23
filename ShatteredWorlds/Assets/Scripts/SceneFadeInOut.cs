using UnityEngine;
using System.Collections;

public class SceneFadeInOut : MonoBehaviour {

	public float fadeSpeed = 0.15f;          // Speed that the screen fades to and from black.
	
	public CanvasGroup fadeCanvas;
	public bool sceneStarting = true; 
	private bool sceneEnding = false;// Whether or not the scene is still fading in.
	void Awake ()
	{

	}
	void Start()
	{

	}
	
	
	void Update ()
	{

		// If the scene is starting...
		if(sceneStarting)
			// ... call the StartScene function.
			StartScene();
		if (sceneEnding)
			EndScene ();
	}

	public void SuddenClear ()
	{
		while (fadeCanvas.alpha >= 0.05) 
		{
			fadeCanvas.alpha -= fadeSpeed * Time.deltaTime;
		}
	}


	public void FadeToClear ()
	{
		fadeCanvas.alpha -= fadeSpeed * Time.deltaTime;
	}
	
	void FadeToBlack ()
	{
		fadeCanvas.alpha += fadeSpeed * Time.deltaTime;
	}
	
	
	void StartScene ()
	{
		// Fade the texture to clear.
		FadeToClear();

		// If the texture is almost clear...
		if(fadeCanvas.alpha <= 0.05f)
		{
			sceneStarting = false;
		}
	}
	
	
	public void EndScene ()
	{
		// Make sure the texture is enabled.

		// Start fading towards black.
		FadeToBlack();
		
		// If the screen is almost black.
			// ... reload the level.
		if(fadeCanvas.alpha >= 0.95f)
		{
			Application.LoadLevel(4);
		}
	
	}

	public void LoadScene(int level)
	{
		sceneStarting = true;
		while (fadeCanvas.alpha < 0.95) {
			FadeToBlack ();
		}
		
		// If the screen is almost black.
		// ... reload the level.
		Application.LoadLevel (level);

	}

}
