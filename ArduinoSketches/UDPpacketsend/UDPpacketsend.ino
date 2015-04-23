#include <SPI.h>
#include <WiFi.h>
#include <WiFiUdp.h>
#include "I2Cdev.h"
#include "MPU6050.h"
#include <RedBot.h>
#include <SFE_CC3000.h>
RedBotMotors motors;

RedBotAccel accelerometer;
MPU6050 accelerometer2;
WiFiUDP Udp;

#if I2CDEV_IMPLEMENTATION == I2CDEV_ARDUINO_WIRE
    #include "Wire.h"
#endif

int status = WL_IDLE_STATUS;
char ssid[] = "MorgansMacBookPro"; //  your network SSID (name)
char pass[] = "testing123";    // your network password (use for WPA, or use as key for WEP)
int keyIndex = 0;            // your network key Index number (needed only for WEP)
unsigned int ap_security = WLAN_SEC_WPA2; // Security of network
unsigned int timeout = 30000;             // Milliseconds
IPAddress hostIP(130,39,239,113); //IP address of network being connected to

/* Selection Pins
int s0 = 7;
int s1 = 8;
int s2 = 9;
int s3 = 10;*/

// Global Variables
SFE_CC3000 wifi = SFE_CC3000(CC3000_INT, CC3000_EN, CC3000_CS);
SFE_CC3000_Client client = SFE_CC3000_Client(wifi);

// Pins
#define CC3000_INT      2   // Needs to be an interrupt pin (D2/D3)
#define CC3000_EN       7   // Can be any digital pin
#define CC3000_CS       10  // Preferred is pin 10 on Uno

// Connection info data lengths
#define IP_ADDR_LEN     4   // Length of IP address in bytes


// Footpad Pins
int LPad = 12; 
int RPad = 13; 

// Accelerometer 2 (MPU6050 will save data to these addresses)
int16_t ax, ay, az;
int16_t gx, gy, gz;

//Wifi Sending information for UDP Packets
unsigned int localPort = 8000;      // local port to listen on


void setup() {
  //Initialize serial and wait for port to open:
  
  ConnectionInfo connection_info;
  int i;
  
  // Initialize Serial port
  Serial.begin(115200);
  Serial.println();
  Serial.println("---------------------------");
  Serial.println("SparkFun CC3000 - WebClient");
  Serial.println("---------------------------");
  
  // Initialize CC3000 (configure SPI communications)
  if ( wifi.init() ) {
    Serial.println("CC3000 initialization complete");
  } else {
    Serial.println("Something went wrong during CC3000 init!");
  }
  
  // Connect using DHCP
  Serial.print("Connecting to SSID: ");
  Serial.println(ssid);
  if(!wifi.connect(ssid, ap_security, pass, timeout)) {
    Serial.println("Error: Could not connect to AP");
  }
  
  // Gather connection details and print IP address
  if ( !wifi.getConnectionInfo(connection_info) ) {
    Serial.println("Error: Could not obtain connection details");
  } else {
    Serial.print("IP Address: ");
    for (i = 0; i < IP_ADDR_LEN; i++) {
      Serial.print(connection_info.ip_address[i]);
      if ( i < IP_ADDR_LEN - 1 ) {
        Serial.print(".");
        
  /*while (!Serial) {
    ; // wait for serial port to connect. Needed for Leonardo only
  }

  // check for the presence of the shield:
  if (WiFi.status() == WL_NO_SHIELD) {
    Serial.println("WiFi shield not present");
    // don't continue:
    while (true);*/
    
    
    
   // Footpads
  pinMode(LPad, INPUT);
  pinMode(RPad, INPUT);
  accelerometer2.initialize();

  /* Multiplexer Control
  pinMode(s0, OUTPUT);
  pinMode(s1, OUTPUT);
  pinMode(s2, OUTPUT);
  pinMode(s3, OUTPUT);
  
  digitalWrite(s1, LOW); 
  digitalWrite(s2, LOW); 
  digitalWrite(s3, LOW); 
  }*/

  /*String fv = WiFi.firmwareVersion();
  if ( fv != "1.1.0" )
    Serial.println("Please upgrade the firmware");

  // attempt to connect to Wifi network:
  while ( status != WL_CONNECTED) {
    Serial.print("Attempting to connect to SSID: ");
    Serial.println(ssid);
    // Connect to WPA/WPA2 network. Change this line if using open or WEP network:
    status = WiFi.begin(ssid,pass);

    // wait 10 seconds for connection:
    delay(10000);
  }
  Serial.println("Connected to wifi");
  printWifiStatus();

  Serial.println("\nStarting connection to server...");
  // if you get a connection, report back via serial:*/
  Udp.begin(localPort);
  }
}

void loop() {
  
  //int packetSize = Udp.parsePacket();
  
  readFootPads(); 
  readDataL();
  readDataR();
}


void printWifiStatus() {
  // print the SSID of the network you're attached to:
  Serial.print("SSID: ");
  Serial.println(WiFi.SSID());

  // print your WiFi shield's IP address:
  IPAddress ip = WiFi.localIP();
  Serial.print("IP Address: ");
  Serial.println(ip);

  // print the received signal strength:
  long rssi = WiFi.RSSI();
  Serial.print("signal strength (RSSI):");
  Serial.print(rssi);
  Serial.println(" dBm");
}

void readDataL() {
  
  //digitalWrite(s0, LOW); 
  
  // accelerometer.read();
  accelerometer2.getMotion6(&ax, &ay, &az, &gx, &gy, &gz);
  char Lbuffer[3];

  /* // Accelerometer X read
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
  Serial.print(",");*/
  
  Lbuffer[0] = ax; //storing the x data from the L accelerometer into the array
  Lbuffer[1] = ay; //storing the y data from the L accelerometer into the array
  Lbuffer[2] = az; //storing the z data from the L accelerometer into the array
  
    // Accelerometer 2 (MPU6050)
  Serial.print(ax); Serial.print(",");
  Serial.print(ay); Serial.print(",");
  Serial.print(az); Serial.print(",");
  Serial.print(gx); Serial.print(",");
  Serial.print(gy); Serial.print(",");
  Serial.print(gz); Serial.println(","); 
  
  // send a reply, to the IP address and port that sent us the packet we received
    Udp.beginPacket(hostIP, localPort);
    Udp.write(Lbuffer,3);
    Udp.endPacket();
}

void readDataR() {
  
  //digitalWrite(s0, HIGH); 
  
  accelerometer.read();
  char Rbuffer[3];

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
  
  Rbuffer[0] = accelerometer.x; //storing the x data from the R accelerometer into the array
  Rbuffer[1] = accelerometer.y; //storing the x data from the R accelerometer into the array
  Rbuffer[2] = accelerometer.z; //storing the x data from the R accelerometer into the array
  
  // send a reply, to the IP address and port that sent us the packet we received
    Udp.beginPacket(hostIP, localPort);
    Udp.write(Rbuffer,3);
    Udp.endPacket();
    
}

void readFootPads(){
    // Left footpad
  Serial.print(digitalRead(LPad));
  Serial.print(",");

  // Right footpad
  Serial.print(digitalRead(RPad));
  Serial.print(",");

}


/*
void SendUDPArray(void) {
  
  Udp.beginPacket(Udp.remoteIP(), Udp.remotePort());
  Udp.write(SendBuffer);
  Udp.endPacket;

}


void send_state(void) {
    sprintf((char*)uip_appdata, "state %ld %ld %ld %c %d", 
    clock_time(), 
    state.sensors.ping[0].cm,
    state.sensors.ping[1].cm,
    state.actuators.chassis.direction,
    state.actuators.chassis.speed);
    uip_send(uip_appdata, strlen((char*)uip_appdata));
}

void send_beacon(void) {
    if(timer_expired(&beacon_timer)) {
        timer_reset(&beacon_timer);
        sprintf((char*)uip_appdata, "beacon %ld", clock_time());
        uip_send(uip_appdata, strlen((char*)uip_appdata));
        uip_log("beacon sent");
    }
}

boolean data_or_poll(void) {
    return (uip_newdata() || uip_poll());
}

static PT_THREAD(handle_connection(void)) {
    PT_BEGIN(&s.pt);
    PT_WAIT_UNTIL(&s.pt, data_or_poll());
    if(uip_newdata()) {
        uip_flags &= (~UIP_NEWDATA);
        send_state();
    } else if (uip_poll()) {
        uip_flags &= (~UIP_POLL);
        send_beacon();
    }

    PT_END(&s.pt);
}*/
