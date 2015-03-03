using UnityEngine;
using System.Collections;

public class ObjectController : MonoBehaviour {

	// Use this for initialization
	public Transform lightPrefab;
	public GameObject audioController;

	void Start () {
		audioController = GameObject.Find( "audioController" );
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collision)
	{
		Debug.Log ("Collision Detected with " + collision.gameObject.tag);
		if (collision.gameObject.tag == "Player") 
		{
			ContactPoint contact = collision.contacts[0];
			Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
			Vector3 pos = contact.point;
			pos.y = pos.y + 1f;
			Instantiate (lightPrefab, pos, rot);
			audioController.GetComponent<AudioController>().audio.Play ();
			Destroy(gameObject);
		}


	}

}
