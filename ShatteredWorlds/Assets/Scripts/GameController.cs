using UnityEngine;
using System.Collections;
using Uniduino;

public class GameController : MonoBehaviour {

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
		int x = Random.Range (1, 2);
		Application.LoadLevel (2);
	}



}
