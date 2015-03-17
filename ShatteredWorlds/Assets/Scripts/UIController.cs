using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Diagnostics;

public class UIController : MonoBehaviour {

	[SerializeField] private Text treeCount;
	[SerializeField] private Text stepCount;
	[SerializeField] private Text timer;

	public int treesDestroyed = 0;
	public int treesTotal; // TODO: automate this count instead of hard-code it
	public int stepsTaken = 0;
	public Stopwatch stopWatch = new Stopwatch ();

	// Use this for initialization
	void Start () {
		stopWatch.Start ();

		treesTotal = GameObject.FindGameObjectsWithTag ("Trees").Length;
		treeCount.text = treesDestroyed + " / " + treesTotal;

	
	}
	
	// Update is called once per frame
	void Update () {
		timer.text = stopWatch.Elapsed.ToString ();
	}

	public void updateTreesDestroyed()
	{
		treesDestroyed++;
		treeCount.text = treesDestroyed + " / " + treesTotal;
	}
}
