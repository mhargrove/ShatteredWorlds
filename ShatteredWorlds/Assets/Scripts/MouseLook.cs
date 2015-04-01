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
	void Update ()
	{
		int turnHorizontal = 0;
		int turnVertical = 0;
		float moveHorizontal = arduinoController.GetComponent<ArduinoController> ().getLeftAccelData ().y;
		float moveVertical = arduinoController.GetComponent<ArduinoController> ().getLeftAccelData ().x;
		if (moveHorizontal > -5000.0f && moveHorizontal < 5000.0f) {
			turnHorizontal = 0;
		} else if (moveHorizontal > 5000.0f)
			turnHorizontal = 1;
		else if (moveHorizontal < -5000.0f)
			turnHorizontal = -1;
		if (moveVertical > -5000.0f && moveVertical < 5000.0f) 
			turnVertical = 0;
		else if (moveHorizontal > 5000.0f)
			turnVertical = 1;
		else if (moveHorizontal < -5000.0f)
			turnVertical = -1;
		if (axes == RotationAxes.MouseXAndY)
		{
			float rotationX = transform.localEulerAngles.y + turnHorizontal * sensitivityX;
			
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
			rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
			
			transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
		}
		else if (axes == RotationAxes.MouseX)
		{
			transform.Rotate(0, turnHorizontal * sensitivityX, 0);
		}
		else
		{
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
			rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
			
			transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
		}
	}
	
	void Start ()
	{
		arduinoController = GameObject.Find ("ArduinoData");
		// Make the rigid body not change rotation
		if (GetComponent<Rigidbody>())
			GetComponent<Rigidbody>().freezeRotation = true;
	}
}