using UnityEngine;
using System.Collections;

public class ObjectController : MonoBehaviour {

	// Use this for initialization
	public Transform lightPrefab;
	public GameObject audioController;
	public GameObject UIcontroller;

	void Start () {
		// All interactions with objects that have sound can have the sounds played throguh the audioController
		audioController = GameObject.Find( "audioController" );
		UIcontroller = GameObject.Find ("UI");
	}
	
	// Update is called once per frame
	void Update () {
	}

	//Detect Collision Between attached object and the colliding gameojbect
	void OnCollisionEnter(Collision collision)
	{

		//TODO: Create case statements for all object tags, ex; "Trees, Rocks, etc." 
		//TODO: Assign collision details to each case statement
		Debug.Log ("Collision Detected with " + collision.gameObject.tag);
		//Collision with the player
		if (collision.gameObject.tag == "Player") 
		{
			ContactPoint contact = collision.contacts[0];
			Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
			Vector3 pos = contact.point;
			pos.y = pos.y + 1f;
			Instantiate (lightPrefab, pos, rot);
			Destroy(gameObject);
			audioController.GetComponent<AudioController> ().playTreeExplosionSfx ();
			UIcontroller.GetComponent<UIController>().updateTreeDestroyed();

		}
	}
}
