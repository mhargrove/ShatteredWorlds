using UnityEngine;
using System.Collections;
using Uniduino;

public class Movement : MonoBehaviour {

	public int move;
	public Arduino Arduino;
	public Movement(Arduino arduino)
	{
		Arduino = arduino;
	}

}
