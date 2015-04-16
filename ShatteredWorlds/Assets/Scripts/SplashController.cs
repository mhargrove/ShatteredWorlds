using UnityEngine;
using System.Collections;

public class SplashController : MonoBehaviour {
	public float timer = 25f;
	public string levelToLoad = "Menu";
	// Use this for initialization
	void Start () {
		StartCoroutine ("DisplayScene");
	}
	IEnumerator DisplayScene() {
		yield return new WaitForSeconds(timer);
		Application.LoadLevel (levelToLoad);
	}
}
