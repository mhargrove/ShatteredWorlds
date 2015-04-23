using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour {

	//This controller hosts all of our SFX and Music.
	//Declare a GameObject for a SFX and then create a public function to play the sound
	//REMEMBER, you must drag the SFX onto the AudioController in your scene to hydrate the variable

	public GameObject treeExplosionSfx;
	public GameObject tree2ExplosionSfx;
	public GameObject ambience2;
	public GameObject ambience3;
	public GameObject glassBell;
	public GameObject missileLaunch;
	public GameObject missileLaunch2;
	public GameObject blackness;
	public GameObject mongolAmbience;
	// Use this for initialization

	public GameObject player;
	void Awake () 
	{
		DontDestroyOnLoad (gameObject);
	}

	void Start () {
		ambience2.GetComponent<AudioSource> ().Play ();
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (player == null)
			player = GameObject.FindGameObjectWithTag ("Player");
	
		if (player != null)
			this.transform.position = player.transform.position;
		else 
			this.transform.position = new Vector3 (0.0f, 0.0f, 0.0f);

		loopMusic ();

	}

	public void playTreeExplosionSfx()
	{
		treeExplosionSfx.GetComponent<AudioSource> ().Play ();
	}
	public void loopMusic()
	{
		if (Application.loadedLevel == 0 || Application.loadedLevel == 1 || Application.loadedLevel == 2 || Application.loadedLevel == 3) {
			if (mongolAmbience.GetComponent<AudioSource> ().isPlaying)
				mongolAmbience.GetComponent<AudioSource> ().Stop();
			if (!ambience2.GetComponent<AudioSource> ().isPlaying)
				ambience2.GetComponent<AudioSource> ().Play ();
		} 
		else if (Application.loadedLevel == 4) 
		{
			if (!mongolAmbience.GetComponent<AudioSource> ().isPlaying)
				mongolAmbience.GetComponent<AudioSource> ().Play ();
			if (ambience2.GetComponent<AudioSource> ().isPlaying)
				ambience2.GetComponent<AudioSource> ().Stop ();
		}
	}
	public void playGlassBell()
	{
		glassBell.GetComponent<AudioSource> ().Play ();
	}
	public void playMissileLaunch()
	{
		missileLaunch.GetComponent<AudioSource> ().Play ();
	}
	public void playMissileLaunch2()
	{
		missileLaunch2.GetComponent<AudioSource> ().Play ();
	}
	public void playBlackness()
	{
		if (!blackness.GetComponent<AudioSource> ().isPlaying)
			blackness.GetComponent<AudioSource> ().Play ();
	}
	public void stopBlackness()
	{
		if (blackness.GetComponent<AudioSource> ().isPlaying)
			blackness.GetComponent<AudioSource> ().Stop ();
	}

	public void playTree2ExplosionSfx()
	{
		Debug.Log ("playing tree2ExplosionSound");
		tree2ExplosionSfx.GetComponent<AudioSource> ().Play ();
	}


	/*TODO: Sound for:
	 * 					FOOTSTEPS
	 * 					Better sound for tree explosion
	 * 					dreamstate sound
	 * 	*/				


}
