using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController : MonoBehaviour {

	[SerializeField] private Text treeCount;
	[SerializeField] private Text stepCount;
	[SerializeField] private Text timeCount;

	[SerializeField] private Text treeCount1;
	[SerializeField] private Text stepCount1;
	[SerializeField] private Text timeCount1;
	[SerializeField] private Text treeCount2;
	[SerializeField] private Text stepCount2;
	[SerializeField] private Text timeCount2;

	public int treesDestroyed = 0;
	public int treesTotal; 
	public int stepsTaken = 0;
	public float timer;
	public bool allTreesDestroyed = false;
	public GameObject mainCamera;
	public GameObject gameController;

	// Use this for initialization
	void Start () {
		mainCamera = GameObject.FindGameObjectWithTag ("MainCamera");
		gameController = GameObject.FindGameObjectWithTag ("GameController");
		if (Application.loadedLevelName == "Test") {
			treesTotal = GameObject.FindGameObjectsWithTag ("Trees").Length;
		} else if (Application.loadedLevelName == "Level2") {
			treesTotal = GameObject.FindGameObjectsWithTag ("Trees2").Length;
		}
		treeCount.text = treesDestroyed + " / " + treesTotal;

	
	}

	public void updateTreesDestroyed()
	{
		treesDestroyed++;
		treeCount.text = treesDestroyed + " / " + treesTotal;
	}
	
	public void updateStepsTaken ()
	{
		stepsTaken++;
		stepCount.text = stepsTaken + "";
	}

	public void levelCompleted ()
	{
		if (mainCamera) {	
			if (mainCamera.GetComponent<Camera> ().fieldOfView <= 178)
				mainCamera.GetComponent<Camera> ().fieldOfView++;	
			else {
				Application.LoadLevel(3);
			} 
		}
	}

	private int minutes;
	private int seconds;
	private int millis;
	public string niceTime;
	private bool sentStats = false;
	// Update is called once per frame
	void Update ()
	{
		if (treesDestroyed == treesTotal) {
			if (!sentStats)
			{
				int level = Application.loadedLevel;
				gameController.GetComponent<GameController> ().SetStatistics(stepsTaken, niceTime, 0, level);
				//gameController.GetComponent<GameController> ().CalculateScore++;
				sentStats = true;
			}
			levelCompleted ();
		}

		timer += Time.deltaTime;
		minutes = Mathf.FloorToInt (timer / 60F);
		seconds = Mathf.FloorToInt (timer - minutes * 60F);
		millis = Mathf.FloorToInt ((timer * 1000) % 1000F);
		
		niceTime = string.Format ("{0:00}:{1:00}:{2:00}", minutes, seconds, millis);
		timeCount.text = niceTime;
	}
}
