using UnityEngine;
using System.Collections;
using Uniduino;

public class GameController : MonoBehaviour {
	//level 1
	public GameObject blackScreen;



	public GameObject player;

	void Awake () 
	{
		DontDestroyOnLoad (gameObject);
	}

	void Start()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
	
	}

	void Update()
	{
		if (player == null)
			player = GameObject.FindGameObjectWithTag ("Player");

	}	
	void showPauseScreen()
	{

	}

	public void loadRandomLevel()
	{
		int x = Random.Range (2, 3);
		Application.LoadLevel (x);
	}
	public void movePlayerToRandomSpot()
	{
		var x = Random.Range (0, 50);
		var z = Random.Range (0, 50);
		player.GetComponent<Rigidbody> ().transform.position = new Vector3 (x, 1.0f, z);
	}




}
