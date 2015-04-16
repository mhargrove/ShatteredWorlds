using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LoadScene(int level){
		Debug.Log ("Play button pressed");
		Application.LoadLevel (level);
	}
}
