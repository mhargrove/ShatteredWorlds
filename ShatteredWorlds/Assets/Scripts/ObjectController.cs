using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ObjectController : MonoBehaviour {

	[SerializeField] private Text treeCount;

	// Use this for initialization
	public Transform lightPrefab;
	public GameObject audioController;
	public int treesDestroyed = 0;
	public int treesTotal = 7; // TODO: automate this count instead of hard-code it

	void Start () {
		treeCount = GetComponent<Text> ();

		audioController = GameObject.Find( "audioController" );
		treeCount.text = treesDestroyed + " / " + treesTotal;
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
