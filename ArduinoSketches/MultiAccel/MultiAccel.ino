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

void setup(void)
{
  Wire.begin(); 
  Serial.begin(57600);
  pinMode(12, INPUT); 
  pinMode(13, INPUT); 
  accelerometer2.initialize(); 
}

void loop(void)
{
   // updates the x, y, and z axis readings and angle readings on the acceleromters
  accelerometer.read();
  accelerometer2.getMotion6(&ax, &ay, &az, &gx, &gy, &gz);

  // Left footpad
  Serial.print(digitalRead(12));
  Serial.print(",");

  // Right footpad
  Serial.print(digitalRead(13)); 
  Serial.print(",");   
    
  // Accelerometer 1 (RedBotAccel)
  Serial.print(accelerometer.x); Serial.print(",");  
  Serial.print(accelerometer.x); Serial.print(",");  
  Serial.print(accelerometer.z); Serial.print(",");  
  Serial.print(accelerometer.angleXZ); Serial.print(","); 
  Serial.print(accelerometer.angleYZ); Serial.print(","); 
  Serial.print(accelerometer.angleXY); Serial.print(",");
  
  // Accelerometer 2 (MPU6050)
  Serial.print(ax); Serial.print(",");
  Serial.print(ay); Serial.print(",");
  Serial.print(az); Serial.print(",");
  Serial.print(gx); Serial.print(",");
  Serial.print(gy); Serial.print(",");
  Serial.print(gz); Serial.println(","); 
}

