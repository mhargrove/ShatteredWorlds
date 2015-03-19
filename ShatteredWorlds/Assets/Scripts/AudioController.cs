using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour {

	//This controller hosts all of our SFX and Music.
	//Declare a GameObject for a SFX and then create a public function to play the sound
	//REMEMBER, you must drag the SFX onto the AudioController in your scene to hydrate the variable

	public GameObject treeExplosionSfx;
	public GameObject ambience2;
	public GameObject ambience3;
	// Use this for initialization
	void Start () {
		ambience2.GetComponent<AudioSource> ().Play ();
	}
	
	// Update is called once per frame
	void Update () {
		loopMusic ();
	}

	public void playTreeExplosionSfx()
	{
		treeExplosionSfx.GetComponent<AudioSource> ().Play ();
	}
	public void loopMusic()
	{
		if (!ambience2.GetComponent<AudioSource> ().isPlaying)
			 ambience2.GetComponent<AudioSource> ().Play ();
	}

}
