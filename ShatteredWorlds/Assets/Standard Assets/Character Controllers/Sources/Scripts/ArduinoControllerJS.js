#pragma strict
import System.IO.Ports;
import System.Collections;
public var inputStream : SerialPort;
var leftFootpad;
var rightFootpad; 
var arduinoID; 
var x;
var y; 
var z; 
var angleXZ; 
var angleYZ; 
var angleXY;

function Start () {

}

function Update () {

}
function Awake () {
	
}
function Setup(s) {
	inputStream = new SerialPort(s, 57600);  
	inputStream.Open (); 
}
