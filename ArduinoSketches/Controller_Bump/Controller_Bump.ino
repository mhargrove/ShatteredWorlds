#include <RedBot.h>
// Selection Pins
int s0 = 7;
//int s1 = 8;
//int s2 = 9;
//int s3 = 10;

// Footpad Pins
int LPad = 12; 
int RPad = 13; 

RedBotAccel *l_accel;
RedBotAccel *r_accel;

int l_foot;
int r_foot;

int l_accel_x;
int l_accel_y;
int l_accel_z;
int l_accel_b;

int r_accel_x;
int r_accel_y;
int r_accel_z;
int r_accel_b;

bool l_isBump;
bool r_isBump;

long previousMillis = 0;        
long interval = 30;      
int passes = 0;
void setup(void)
{
  Serial.begin(57600);
  
  // Footpads
  pinMode(LPad, INPUT);
  pinMode(RPad, INPUT);
//  pinMode(11, OUTPUT);
//  pinMode(10, OUTPUT);

  // Multiplexer Control
  pinMode(s0, OUTPUT);
//  pinMode(s1, OUTPUT);
//  pinMode(s2, OUTPUT);
//  pinMode(s3, OUTPUT);

  l_foot = 0;
  r_foot = 0;

// Left Accel instantiation
  digitalWrite(s0, LOW); 
//  digitalWrite(s1, LOW); 
//  digitalWrite(s2, LOW); 
//  digitalWrite(s3, LOW); 
  l_accel = new RedBotAccel;
  l_accel->enableBump();
  l_accel->setBumpThresh(33);
  
  l_accel_x = 0;
  l_accel_y = 0;
  l_accel_z = 0;
  l_accel_b = 0;
  
  l_isBump = false;
  
  digitalWrite(s0, HIGH); 
//  digitalWrite(s1, LOW); 
//  digitalWrite(s2, LOW); 
//  digitalWrite(s3, LOW); 
  r_accel = new RedBotAccel;
  r_accel->enableBump();
  r_accel->setBumpThresh(33);
  
  r_accel_x = 0;
  r_accel_y = 0;
  r_accel_z = 0;
  r_accel_b = 0;
  
  r_isBump = false;
}

void loop(void)
{
  unsigned long currentMillis = millis();
  if(currentMillis - previousMillis > interval){
    previousMillis = currentMillis; 
    printFootPads(); 
    printDataL();
    printDataR(); 
//    if(l_accel_b == 1)
//      digitalWrite(11, HIGH);
//    else
//      digitalWrite(11, LOW);
//    if(r_accel_b ==1)
//      digitalWrite(10, HIGH);
//    else
//      digitalWrite(10, LOW);
    if(passes > 7){
      l_isBump = false;
      r_isBump = false;
      l_accel_b = 0;
      r_accel_b = 0;
      passes = 0;
    }
    else
      passes++;
  }
  readFootPads(); 
  readDataL();
  readDataR(); 
}

void printDataL() {
  // Accelerometer X read
  Serial.print(l_accel_x); Serial.print(",");  

  // Accelerometer Y read
  Serial.print(l_accel_y); Serial.print(","); 

  // Accelerometer Z read
  Serial.print(l_accel_z); Serial.print(",");  

//  // Angle read XZ
//  Serial.print(l_accel->angleXZ); Serial.print(",");
//
//  // Angle read YZ
//  Serial.print(l_accel->angleYZ); Serial.print(",");
//
//  // Angle read XY
//  Serial.print(l_accel.angleXY); Serial.print(",");
  
  Serial.print(l_accel_b);Serial.print(","); 
}

void printDataR() {
  // Accelerometer X read
  Serial.print(r_accel_x); Serial.print(",");  // tab

  // Accelerometer Y read
  Serial.print(r_accel_y); Serial.print(",");  // tab

  // Accelerometer Z read
  Serial.print(r_accel_z); Serial.print(",");  // tab

  // Angle read XZ
//  Serial.print(accelerometer->angleXZ); Serial.print(",");
//
//  // Angle read YZ
//  Serial.print(accelerometer->angleYZ); Serial.print(",");
//
//  // Angle read XY
//  Serial.print(accelerometer->angleXY); Serial.println(",");

    Serial.print(r_accel_b); Serial.println(",");
}

void printFootPads(){
    // Left footpad
  Serial.print(l_foot); Serial.print(",");

  // Right footpad
  Serial.print(r_foot); Serial.print(",");
}

void readDataL(){
    //RedBotAccel accelerometer;
  digitalWrite(s0, LOW); 
//  digitalWrite(s1, LOW); 
//  digitalWrite(s2, LOW); 
//  digitalWrite(s3, LOW); 
  l_accel->read();
  l_accel_x = l_accel->x;
  l_accel_y = l_accel->y;
  l_accel_z = l_accel->z;
  int temp = r_accel->checkBump() ;
  if(!l_isBump && temp){
    l_accel_b = 1;
    l_isBump = true;
  }
}

void readDataR(){
    //RedBotAccel accelerometer;
  digitalWrite(s0, HIGH); 
//  digitalWrite(s1, LOW); 
//  digitalWrite(s2, LOW); 
//  digitalWrite(s3, LOW); 
  r_accel->read();
  r_accel_x = r_accel->x;
  r_accel_y = r_accel->y;
  r_accel_z = r_accel->z;
  int temp = r_accel->checkBump();
  if(!r_isBump && temp){
    r_accel_b = 1;
    r_isBump = true;
  }
}


void readFootPads(){
  l_foot = digitalRead(LPad);
  r_foot = digitalRead(RPad);
}

