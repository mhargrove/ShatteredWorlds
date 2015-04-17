/*
 * Setup for MPU6050: 
 *    VCC -> 5V
 *    GND -> GND
 *    SCL -> A5
 *    SDA -> A4
 *    INT -> D2
 */
 
#include <RedBot.h>
#include "I2Cdev.h"
#include "MPU6050.h"

#if I2CDEV_IMPLEMENTATION == I2CDEV_ARDUINO_WIRE
    #include "Wire.h"
#endif


RedBotAccel accelerometer;
MPU6050 accelerometer2; 

// Accelerometer 2 (MPU6050 will save data to these addresses)
int16_t ax, ay, az;
int16_t gx, gy, gz;

// Footpad Pins
int LPad = 12; 
int RPad = 13; 

void setup(void)
{
  Wire.begin(); 
  Serial.begin(57600);
  pinMode(LPad, INPUT); 
  pinMode(RPad, INPUT); 
  accelerometer2.initialize(); 
}

void loop(void)
{
   readFootPads(); 
   readDataL(); 
   readDataR(); 
}

void readDataL() {

  accelerometer.read();

  // Accelerometer X read
  Serial.print(accelerometer.x); Serial.print(",");  

  // Accelerometer Y read
  Serial.print(accelerometer.y); Serial.print(",");  

  // Accelerometer Z read
  Serial.print(accelerometer.z); Serial.print(",");  

  // Angle read XZ
  Serial.print(accelerometer.angleXZ); Serial.print(",");

  // Angle read YZ
  Serial.print(accelerometer.angleYZ); Serial.print(",");

  // Angle read XY
  Serial.print(accelerometer.angleXY); Serial.print(","); 
}

void readDataR() {
  
  accelerometer2.getMotion6(&ax, &ay, &az, &gx, &gy, &gz);
  
  Serial.print(ax); Serial.print(",");
  Serial.print(ay); Serial.print(",");
  Serial.print(az); Serial.print(",");
  Serial.print(gx); Serial.print(",");
  Serial.print(gy); Serial.print(",");
  Serial.print(gz); Serial.println(","); 
}

/*
 * Prints
 */
void readFootPads(){
    // Left footpad
  Serial.print(digitalRead(LPad));
  Serial.print(",");

  // Right footpad
  Serial.print(digitalRead(RPad));
  Serial.print(",");

}

