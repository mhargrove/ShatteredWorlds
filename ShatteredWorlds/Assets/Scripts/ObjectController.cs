using UnityEngine;
using System.Collections;
using System.Linq;

public class ObjectController : MonoBehaviour {

	// Use this for initialization
	public Transform lightPrefab;
	public GameObject treeRemains;
	public GameObject audioController;
	public GameObject UIcontroller;
	public GameObject fadeInOut;

	void Start () {
		// All interactions with objects that have sound can have the sounds played throguh the audioController
		audioController = GameObject.Find( "audioController" );
		UIcontroller = GameObject.Find ("UI");
		fadeInOut = GameObject.FindGameObjectWithTag ("Fader");

	}
	
	// Update is called once per frame
	void Update () {

	}

	//Detect Collision Between attached object and the colliding gameojbect
	void OnTriggerEnter(Collider collider)
	{
		Debug.Log (gameObject.tag + ": Collision Detected with " + collider.gameObject.tag);
		if (collider.gameObject.tag == "Player" || collider.gameObject.tag == "Projectile") {
			Vector3 pos = collider.gameObject.transform.position + (collider.gameObject.transform.forward * 2);
			pos.y = pos.y + 1f;
			Instantiate(lightPrefab, pos, Quaternion.identity);
			//Instantiate(treeRemains, this.transform.position, Quaternion.identity);
		    Destroy (gameObject);
			if (gameObject.tag == "Trees")
			    audioController.GetComponent<AudioController> ().playTreeExplosionSfx ();
			else if (gameObject.tag == "Trees2")
				audioController.GetComponent<AudioController> ().playTree2ExplosionSfx ();
			if (UIcontroller)
				UIcontroller.GetComponent<UIController>().updateTreesDestroyed();
			DestroyClones("Clone", 6.0f);
			if (collider.gameObject.tag != "Player")
			    Destroy(collider.gameObject);
	//		DestroyClones("TreeRemains", 3.0f);
		}

		if (gameObject.tag == "Level1Selector" && collider.gameObject.tag == "Projectile")
		{	
			fadeInOut.GetComponent<SceneFadeInOut> ().LoadScene(2);
		}
		else if (gameObject.tag == "Level2Selector" && collider.gameObject.tag == "Projectile")
		{
			fadeInOut.GetComponent<SceneFadeInOut> ().LoadScene(4);
		}
	}
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
			Vector3 pos = contact.point  + collision.gameObject.transform.forward*3;;
			pos.y = pos.y + 1f;

			Instantiate (lightPrefab, pos, rot);
			Destroy(gameObject);
			audioController.GetComponent<AudioController> ().playTreeExplosionSfx ();
			UIcontroller.GetComponent<UIController>().updateTreesDestroyed();
			DestroyClones("Clone", 3.0f);
		}
		if (collision.gameObject.tag == "Projectile") 
		{

		}
	}

	public void DestroyClones(string tag, float time) 
	{
		var clones = GameObject.FindGameObjectsWithTag (tag);
		if (clones.Any())
		{
			foreach (var clone in clones)
				Destroy(clone, time);
		}
	}
}
