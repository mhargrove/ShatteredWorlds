using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

	public int rotationSpeed = 30; 
	// Update is called once per frame
	void Update () {
		transform.Rotate (new Vector3 (0, rotationSpeed, 0) * Time.deltaTime); 
	}
}
