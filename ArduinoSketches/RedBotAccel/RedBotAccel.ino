#include <RedBot.h>
RedBotMotors motors;

RedBotAccel accelerometer;

void setup(void)
{
  Serial.begin(57600);
  pinMode(12, INPUT); 
  pinMode(13, INPUT); 
}

void loop(void)
{
   // updates the x, y, and z axis readings and angle readings on the acceleromter
  accelerometer.read();

  // Arduino ID
  Serial.print(0);
  Serial.print(",");
  
  // Left footpad
  Serial.print(digitalRead(12));
  Serial.print(",");

  // Right footpad
  Serial.print(digitalRead(13)); 
  Serial.print(",");   
  
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

  // short delay in between readings/
  delay(10); 
}

