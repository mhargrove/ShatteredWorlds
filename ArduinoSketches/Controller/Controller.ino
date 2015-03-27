#include <RedBot.h>
RedBotMotors motors;

RedBotAccel accelerometer;

// Selection Pins
int s0 = 7;
int s1 = 8;
int s2 = 9;
int s3 = 10;

// Footpad Pins
int LPad = 12; 
int RPad = 13; 

void setup(void)
{
  Serial.begin(57600);
  
  // Footpads
  pinMode(LPad, INPUT);
  pinMode(RPad, INPUT);

  // Multiplexer Control
  pinMode(s0, OUTPUT);
  pinMode(s1, OUTPUT);
  pinMode(s2, OUTPUT);
  pinMode(s3, OUTPUT);
  
  digitalWrite(s1, LOW); 
  digitalWrite(s2, LOW); 
  digitalWrite(s3, LOW); 
}

void loop(void)
{
  readFootPads(); 
  readDataL();
  readDataR(); 
  
}

void readDataL() {
  digitalWrite(s0, LOW); 
  
  accelerometer.read();

  // Accelerometer X read
  Serial.print(accelerometer.x);
  Serial.print(",");  // tab

  // Accelerometer Y read
  Serial.print(accelerometer.y);
  Serial.print(",");  // tab

  // Accelerometer Z read
  Serial.print(accelerometer.z);
  Serial.print(",");  // tab

  // Angle read XZ
  Serial.print(accelerometer.angleXZ);
  Serial.print(",");

  // Angle read YZ
  Serial.print(accelerometer.angleYZ);
  Serial.print(",");

  // Angle read XY
  Serial.print(accelerometer.angleXY);
  Serial.print(",");
  
}

void readDataR() {
  digitalWrite(s0, HIGH); 
  
  accelerometer.read();

  // Accelerometer X read
  Serial.print(accelerometer.x);
  Serial.print(",");  // tab

  // Accelerometer Y read
  Serial.print(accelerometer.y);
  Serial.print(",");  // tab

  // Accelerometer Z read
  Serial.print(accelerometer.z);
  Serial.print(",");  // tab

  // Angle read XZ
  Serial.print(accelerometer.angleXZ);
  Serial.print(",");

  // Angle read YZ
  Serial.print(accelerometer.angleYZ);
  Serial.print(",");

  // Angle read XY
  Serial.print(accelerometer.angleXY);
  Serial.println(",");
}

void readFootPads(){
    // Left footpad
  Serial.print(digitalRead(LPad));
  Serial.print(",");

  // Right footpad
  Serial.print(digitalRead(RPad));
  Serial.print(",");

}

