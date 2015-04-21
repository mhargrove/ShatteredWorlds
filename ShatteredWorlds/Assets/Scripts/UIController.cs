using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController : MonoBehaviour {

	[SerializeField] private Text treeCount;
	[SerializeField] private Text stepCount;
	[SerializeField] private Text timeCount;

	public int treesDestroyed = 0;
	public int treesTotal; 
	public int stepsTaken = 0;
	public float timer;

	// Use this for initialization
	void Start () {

		treesTotal = GameObject.FindGameObjectsWithTag ("Trees").Length;
		treeCount.text = treesDestroyed + " / " + treesTotal;

	
	}

	public void updateTreesDestroyed()
	{
		treesDestroyed++;
		treeCount.text = treesDestroyed + " / " + treesTotal;
	}

	// TODO: fix this to only add 1 step at a time (currently adding as long as pressed)
	public void updateStepsTaken ()
	{
		stepsTaken++;
		stepCount.text = stepsTaken + "";
	}

	private int minutes;
	private int seconds;
	private int millis;
	private string niceTime;
	// Update is called once per frame
	void Update ()
	{
		timer += Time.deltaTime;
		minutes = Mathf.FloorToInt (timer / 60F);
		seconds = Mathf.FloorToInt (timer - minutes * 60F);
		millis = Mathf.FloorToInt ((timer * 1000) % 1000F);
		
		niceTime = string.Format ("{0:00}:{1:00}:{2:00}", minutes, seconds, millis);
		timeCount.text = niceTime;
	}
}
