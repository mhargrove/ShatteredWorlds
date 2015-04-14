using UnityEngine;
using System.Collections;

public class DestroyObject : MonoBehaviour {

	// Use this for initialization
	public float timeTilDeath = 6.0f;
	void Start () {
		Destroy (this.transform.gameObject, timeTilDeath);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
