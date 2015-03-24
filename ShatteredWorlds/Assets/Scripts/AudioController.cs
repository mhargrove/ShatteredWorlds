using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour {

	//This controller hosts all of our SFX and Music.
	//Declare a GameObject for a SFX and then create a public function to play the sound
	//REMEMBER, you must drag the SFX onto the AudioController in your scene to hydrate the variable

	public GameObject treeExplosionSfx;
	public GameObject ambience2;
	public GameObject ambience3;
	public GameObject glassBell;
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
		if (!ambience2.GetComponent<AudioSource> ().isPlaying)
			 ambience2.GetComponent<AudioSource> ().Play ();
	}
	public void playGlassBell()
	{
		glassBell.GetComponent<AudioSource> ().Play ();
	}

	/*TODO: Sound for:
	 * 					FOOTSTEPS
	 * 					Better sound for tree explosion
	 * 					dreamstate sound
	 * 	*/				


}
