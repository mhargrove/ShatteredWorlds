using UnityEngine;
using System.Collections;
using System.Linq;

public class Shoot : MonoBehaviour {

	public GameObject projectile;
	public float projectileSpeed = 1000.0f;
	public GameObject audioController;
	// Use this for initialization
	void Start () {
		audioController = GameObject.Find( "audioController" );
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.F))
		{
			Fire ();
		}
	}

	public void Fire()
	{
		audioController.GetComponent<AudioController> ().playMissileLaunch ();
		Vector3 spawnPos = new Vector3 (this.transform.position.x - 1.0f, this.transform.position.y, this.transform.position.z)+ this.transform.forward * 3;
		//Vector3 spawnPos = this.transform.position + this.transform.forward * 3;
		Vector3 forcePos = this.transform.forward + new Vector3 (0.15f, 1.0f, 0.15f);
		GameObject clone = Instantiate (projectile, spawnPos, Quaternion.identity) as GameObject;
		clone.rigidbody.AddForce((this.transform.forward + forcePos) * projectileSpeed);


	}
	public void DestroyProjectile()
	{

	}

}