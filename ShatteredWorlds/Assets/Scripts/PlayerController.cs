using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

public class PlayerController : MonoBehaviour {
	
	public GameObject gameController;
	public GameObject UIcontroller;
	void Start()
	{
		gameController = GameObject.Find ("GameController");
		UIcontroller = GameObject.Find ("UI");
	}

	void Update()
	{  
		if (this.transform.position.y < -100.0f)
			this.transform.position = (new Vector3(105.0f, 100.0f, 95.0f));
	}

}
