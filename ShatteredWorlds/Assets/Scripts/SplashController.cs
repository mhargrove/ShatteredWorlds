using UnityEngine;
using System.Collections;

public class SplashController : MonoBehaviour {
	public float timer = 3f;
	public string levelToLoad = "Level1";
	// Use this for initialization
	void Start () {
		StartCoroutine ("DisplayScene");
	}
	IEnumerator DisplayScene() {
		yield return new WaitForSeconds(timer);
		Application.LoadLevel (levelToLoad);
	}
}
