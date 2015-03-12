using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController : MonoBehaviour {

	[SerializeField] private Text treeCount;
	[SerializeField] private Text stepCount;
	[SerializeField] private Text timer;

	public int treesDestroyed = 0;
	public int treesTotal = 7; // TODO: automate this count instead of hard-code it
	public int stepsTaken = 0;

	// Use this for initialization
	void Start () {
		treeCount.text = treesDestroyed + " / " + treesTotal;
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void updateTreesDestroyed()
	{
		treesDestroyed++;
		treeCount.text = treesDestroyed + " / " + treesTotal;
	}
}
