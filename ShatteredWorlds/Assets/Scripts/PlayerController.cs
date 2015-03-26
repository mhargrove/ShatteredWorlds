using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

public class PlayerController : MonoBehaviour {
	
	public GameObject gameController;
	public Transform fadeToBlack;
	public GameObject blackScreen;
	public GameObject UIcontroller;
	private Vector3 moveUp;
	public bool fadingToBlack = false;
	public bool initialSceneBlack= true;
	public bool moving = false;
	public float startBlackness = 0;
	public float endBlackness = 6;
	void Start()
	{
		gameController = GameObject.Find ("GameController");
		UIcontroller = GameObject.Find ("UI");
	}

	void Update()
	{  

		if (this.rigidbody.IsSleeping()) {
			startBlackness+= Time.deltaTime;
			if (!fadingToBlack){
			    Instantiate (fadeToBlack, new Vector3 (this.transform.position.x,this.transform.position.y, this.transform.position.z + 4.0f) , Quaternion.identity);
			    Instantiate (fadeToBlack, new Vector3 (this.transform.position.x + 4.0f,this.transform.position.y + 2.0f, this.transform.position.z + 4.0f) , Quaternion.identity);
		     	Instantiate (fadeToBlack, new Vector3 (this.transform.position.x - 4.0f,this.transform.position.y + 2.0f, this.transform.position.z + 4.0f) , Quaternion.identity);
			    fadingToBlack = true;
				startBlackness = 0;
			}
			if (startBlackness >= endBlackness)
			{
				//TODO: Change the audio as you are fading into darkness

				//if the screen is black for 10 seconds, reload a random level, 
				//or we can change the players position in the map
				gameController.GetComponent<GameController>().loadRandomLevel();
			}
		}
		if (!this.rigidbody.IsSleeping ()) {
			DestroyClones ("DarkMist", 0.2f);
			fadingToBlack = false;
		}

		if (initialSceneBlack && !this.rigidbody.IsSleeping()) {
			//levels initially load black, once you move set initialSceneBlack to false and destroy the black screen
			Destroy(GameObject.FindGameObjectWithTag("BlackScreen"));
			initialSceneBlack = false;
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
