using UnityEngine;
using System.Collections;

/// MouseLook rotates the transform based on the mouse delta.
/// Minimum and Maximum values can be used to constrain the possible rotation

/// To make an FPS style character:
/// - Create a capsule.
/// - Add the MouseLook script to the capsule.
///   -> Set the mouse look to use LookX. (You want to only turn character but not tilt it)
/// - Add FPSInputController script to the capsule
///   -> A CharacterMotor and a CharacterController component will be automatically added.

/// - Create a camera. Make the camera a child of the capsule. Reset it's transform.
/// - Add a MouseLook script to the camera.
///   -> Set the mouse look to use LookY. (You want the camera to tilt up and down like a head. The character already turns.)
[AddComponentMenu("Camera-Control/Mouse Look")]
public class MouseLook : MonoBehaviour {

	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	public RotationAxes axes = RotationAxes.MouseXAndY;
	public float sensitivityX = 15F;
	public float sensitivityY = 15F;

	public float minimumX = -360F;
	public float maximumX = 360F;

	public float minimumY = -60F;
	public float maximumY = 60F;

	float rotationY = 0F;

	public GameObject arduinoController;

	private int turnHorizontal;
	private int turnVertical;
	private float moveRight;
	private float moveLeft;
	private int turn;
	private float rotationX;
	private bool fired = false;
	private Vector3 moveHorizontal;

	void Update ()
	{
		turnHorizontal = 0;
		turnVertical = 0;
		moveRight = arduinoController.GetComponent<ArduinoController> ().getRightAccelData ().y;
		moveLeft = arduinoController.GetComponent<ArduinoController> ().getLeftAccelData ().y;


		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			turn = -1; 
		} else if (Input.GetKeyDown (KeyCode.RightArrow)) {
			turn = 1; 
		} else {
			turn = 0; 
		}
//		if (moveHorizontal > -5000.0f && moveHorizontal < 5000.0f) {
//			turnHorizontal = 0;
//		} else if (moveHorizontal > 5000.0f)
//			turnHorizontal = 1;
//		else if (moveHorizontal < -5000.0f)
//			turnHorizontal = 1;

//		if (moveVertical > -5000.0f && moveVertical < 5000.0f) 
//			turnVertical = 0;
//		else if (moveVertical > 5000.0f)
//			turnVertical = -1;
//		else if (moveVertical < -5000.0f)
//			turnVertical = -1;


//		turn = 0;

//		if (turnHorizontal == 0 && turnVertical == 0)
//			turn = 0;
//		else if (turnHorizontal == 1 && turnVertical == 0)
//			turn = 1;
//		else if (turnHorizontal == 0 && turnVertical == -1)
//			turn = -1;
//		else if (turnHorizontal == 1 && turnVertical == -1 && !fired) {
//			//shoot/chop tree down gesture
//			StartCoroutine(Shoot ());
//		}

		if (axes == RotationAxes.MouseXAndY)
		{
			rotationX = transform.localEulerAngles.y + turn * sensitivityX;
			
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
			rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
			
			transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
		}
		else if (axes == RotationAxes.MouseX)
		{
			transform.Rotate(0, turn * sensitivityX, 0);
		}
		else
		{
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
			rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
			
			transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
		}
	}

	IEnumerator Shoot(){
		fired = true;
		yield return new WaitForSeconds (1f);
		if (Application.loadedLevel == 4)
			GetComponent<Shoot> ().Fire (2);
		else
			GetComponent<Shoot> ().Fire (1);
		fired = false;
	}


	void Start ()
	{
		arduinoController = GameObject.Find ("ArduinoData");
		// Make the rigid body not change rotation
		if (GetComponent<Rigidbody>())
			GetComponent<Rigidbody>().freezeRotation = true;
	}
}