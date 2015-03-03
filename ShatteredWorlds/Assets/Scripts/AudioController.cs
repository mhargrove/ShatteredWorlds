using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour {

	//This controller hosts all of our SFX and Music.
	//Declare a GameObject for a SFX and then create a public function to play the sound
	//REMEMBER, you must drag the SFX onto the AudioController in your scene to hydrate the variable

	public GameObject treeExplosionSfx;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void playTreeExplosionSfx()
	{
		treeExplosionSfx.GetComponent<AudioSource> ().Play ();
	}

}
