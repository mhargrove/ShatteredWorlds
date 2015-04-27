using UnityEngine;
using System.Collections;
using System.IO.Ports; 
using System;

public class ArduinoController : MonoBehaviour {
	
	SerialPort inputStream; 
	
	public int leftFootpad;
	public int rightFootpad; 
	
	public float x_left; public float x_right; 
	public float y_left; public float y_right; 
	public float z_left; public float z_right; 

	public int leftBump; public int rightBump;

	private string[] str;
	
//	public float angleXZ_left; public float angleXZ_right; 
//	public float angleYZ_left; public float angleYZ_right; 
//	public float angleXY_left; public float angleXY_right; 
	
	/*
	 * Sets up serial port; You may need to change this to match your arduino port  
	 */
	public void Setup(string s){
		try{
			inputStream = new SerialPort(s, 57600);  
			inputStream.Open (); 
		} catch{
			print ("Error communicating with Arduino. Check connection or use automatic setup."); 
		}
	}
	
	public void Setup(){
		try{
			inputStream = new SerialPort(guessPortName(), 57600);
			inputStream.Open(); 
		}catch{
			print ("Error communicating with Arduino. Check connection or manually set up the port."); 
		}
		
	}
	
	void Awake () 
	{
		DontDestroyOnLoad (gameObject);
	}
	
	void Start(){
		Setup (); 
		//Setup('PORT NAME HERE', 57600); 
	}
	
	/*
	 * Parses serial data and stores to appropriate global variables 
	 */
	void Update()
	{
		readArduinoData ();
	}
	
	public void readArduinoData()
	{
		try{
			print (inputStream.ReadLine ());
			string[] str = inputStream.ReadLine ().Split(',');; 
			
			leftFootpad = Convert.ToInt32 (str[0]); 
			rightFootpad = Convert.ToInt32  (str[1]); 

			x_left = Convert.ToSingle (str[2]);
			y_left = Convert.ToSingle (str[3]);
			z_left = Convert.ToSingle (str[4]); 

			leftBump = Convert.ToInt32 (str[5]);

			x_right = Convert.ToSingle (str[6]);
			y_right = Convert.ToSingle (str[7]);
			z_right = Convert.ToSingle (str[8]);

			rightBump = Convert.ToInt32 (str[5]);

//			x_left = float.Parse (str[2]);
//			y_left = float.Parse (str[3]);
//			z_left = float.Parse (str[4]); 
//			
//			angleXZ_left = float.Parse(str[5]);
//			angleYZ_left = float.Parse (str[6]);
//			angleXY_left = float.Parse (str[7]); 
//			
//			x_right = float.Parse (str[8]);
//			y_right = float.Parse (str[9]);
//			z_right = float.Parse (str[10]); 
//			
//			angleXZ_right = float.Parse(str[11]);
//			angleYZ_right = float.Parse (str[12]);
//			angleXY_right = float.Parse (str[13]); 
			
		}catch{}
	}
	
	
	/*
	 * Returns left accelerometer readings as a vector. Data will need to be normalized
	 */
	public Vector3 getLeftAccelData(){
		//Update (); 
		return new Vector3 (x_left, y_left, z_left); 
	}
	
	/*
	 * Returns right accelerometer readings as a vector. Data will need to be normalized
	 */
	public Vector3 getRightAccelData(){
		//Update (); 
		return new Vector3 (x_right, y_right, z_right); 
	}
	
//	public float getLeftAngleXZ(){
//		return angleXZ_left; 
//	}
//	
//	public float getLeftAngleYZ(){
//		return angleYZ_left; 
//	}
//	
//	public float getLeftAngleXY(){
//		return angleXY_left; 
//	}
//	
//	
//	public float getRightAngleXZ(){
//		return angleXZ_right; 
//	}
//	
//	public float getRightAngleYZ(){
//		return angleYZ_right; 
//	}
//	
//	public float getRightAngleXY(){
//		return angleXY_right; 
//	}
//	
	
	/*
	 * Returns if left foot pad is pressed (released)
	 */
	public int readLeftFootpad(){
		return leftFootpad; 
	}
	
	/*
	 * Returns if right foot pad is pressed (released)
	 */ 
	public int readRightFootpad(){
		return rightFootpad;
	}

	public int getLeftBump(){
		return leftBump;
	}

	public int getRightBump(){
		return rightBump;
	}
	
	public void print(){
		Update (); 
		print ("LF: "+leftFootpad+" RF: " + rightFootpad + "\t LAccel: ( "+x_left + ", " + y_left + ", " + z_left + ")\t RAccel: ("+x_right + ", " + y_right + ", " + z_right + ")"); 
	}
	
	/*
	 *	Guesses the port name for unix based machines. Taken from Uniduino library 
	 */
	
	public static string guessPortName ()
	{	

		if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsWebPlayer) {
			var devices = System.IO.Ports.SerialPort.GetPortNames ();
			
			if (devices.Length == 0) { //
				return "COM3"; 	
			} else
				return devices [0];	

		} else {
			var devices = System.IO.Ports.SerialPort.GetPortNames ();
		
			if (devices.Length == 0) {
				devices = System.IO.Directory.GetFiles ("/dev/");		
			}
		
			string dev = "";
			;			
			foreach (var d in devices) {				
				if (d.StartsWith ("/dev/tty.usb") || d.StartsWith ("/dev/ttyUSB")) {
					dev = d;
					break;
				}
			}
			return dev;		
		}
	}
	
	void OnApplicationQuit() 
	{
		inputStream.Close();
	}
	
	//	void Sleep(){
	//		inputStream.Write (0x01);
	//	}
}
