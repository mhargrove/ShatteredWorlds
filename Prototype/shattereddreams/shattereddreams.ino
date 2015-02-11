#include "I2Cdev.h"
#include "MPU6050.h"

// Arduino Wire library is required if I2Cdev I2CDEV_ARDUINO_WIRE implementation
// is used in I2Cdev.h
#if I2CDEV_IMPLEMENTATION == I2CDEV_ARDUINO_WIRE
    #include "Wire.h"
#endif

MPU6050 accelgyro;

int16_t ax, ay, az;
int16_t gx, gy, gz;

#define OUTPUT_READABLE_ACCELGYRO

#define LED_PIN 13
bool blinkState = false;

int initVal; 


void setup() {
    // join I2C bus (I2Cdev library doesn't do this automatically)
    #if I2CDEV_IMPLEMENTATION == I2CDEV_ARDUINO_WIRE
        Wire.begin();
    #elif I2CDEV_IMPLEMENTATION == I2CDEV_BUILTIN_FASTWIRE
        Fastwire::setup(400, true);
    #endif
    
    //recommended baud 
    Serial.begin(9600);

    // initialize acc device
    //Serial.println("Initializing I2C devices...");
    accelgyro.initialize();

    // verify accel connection
//    Serial.println("Testing device connections...");
//    Serial.println(accelgyro.testConnection() ? "MPU6050 connection successful" : "MPU6050 connection failed");
    
    accelgyro.getMotion6(&ax, &ay, &az, &gx, &gy, &gz);
    initVal = ax; 
    }
    
void loop() {
    // read raw accel/gyro measurements from device


    #ifdef OUTPUT_READABLE_ACCELGYRO
        // display tab-separated accel/gyro x/y/z values
        //Serial.print("a/g:\t");

          Serial.println(0);

//        Serial.print(ax); Serial.print("\t");
//        Serial.print(ay); Serial.print("\t");
//        Serial.print(az); Serial.print("\t");
//        Serial.print(gx); Serial.print("\t");
//        Serial.print(gy); Serial.print("\t");
//        Serial.println(gz);
    #endif
}
