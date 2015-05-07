using UnityEngine;
using System.Collections;
using Uniduino;



public class GameController : MonoBehaviour {
	//level 1
	public GameObject blackScreen;
	public int LastStepsTaken1;
	public string TimeCompleted1 = "";
	public int TreesDestroyed1;
	public int LastStepsTaken2;
	public string TimeCompleted2 = "";
	public int TreesDestroyed2;


	public GameObject player;

	void Awake () 
	{
		DontDestroyOnLoad (gameObject);
	}

	void Start()
	{
		Application.targetFrameRate = 30;
		player = GameObject.FindGameObjectWithTag ("Player");
		TimeCompleted1 = "00:00:00";
		TimeCompleted2 = "00:00:00";
	
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
		Application.LoadLevel (3);
	}
	public void movePlayerToRandomSpot()
	{
		var x = Random.Range (0, 50);
		var z = Random.Range (0, 50);
		player.GetComponent<Rigidbody> ().transform.position = new Vector3 (x, 1.0f, z);
	}

	public void SetStatistics(int steps, string time, int trees, int level)
	{
		if (level == 2) 
			TimeCompleted1 = time;
		else if (level == 4)
			TimeCompleted2 = time;

		}
	public string GetLevel1Statistics()
	{
		return TimeCompleted1;
	}

	public string GetLevel2Statistics()
	{
		return TimeCompleted2;
	}

	/*public void CalculateScore(int total, int level, int Level1Score, int Level2Score)
	{
		Level1Score = 1500;
		Level2Score = 2500;
		
		if (level == 2) {
			total = Level1Score - (((float)TimeCompleted1/30)*100) + ((LastStepsTaken1/50)*100);
		}
		else if (level == 4) {
			total = Level2Score - (((float)TimeCompleted2/30)*100) + ((LastStepsTaken2/50)*100);
		}
		
		
	}*/
}
