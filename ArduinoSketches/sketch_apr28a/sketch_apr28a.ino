const int l1 = 3;
const int l2 = 4;
const int l3 = 5;
const int l4 = 6;
const int l5 = 7;

const int w1 = 9;
const int w2 = 10;
const int w3 = 11;
const int w4 = 12;
const int w5 = 13;

int val = 0; 

// variable to store the value coming from the sensor
int btn = LOW;
int old_btn = LOW;
int state = 0;
char buffer[7] ;
int pointer = 0;


String[] str; 

void setup() {
  Serial.begin(9600); // open the serial port
  pinMode(l1, OUTPUT);
  pinMode(l2, OUTPUT);
  pinMode(l3, OUTPUT);
  pinMode(l4, OUTPUT);
  pinMode(l5, OUTPUT);
  pinMode(w1, OUTPUT);
  pinMode(w2, OUTPUT);
  pinMode(w3, OUTPUT);
  pinMode(w4, OUTPUT);
  pinMode(w5, OUTPUT);
}

void loop() {
  
  if (Serial.available() > 0) {
    // read the incoming byte:
    inByte = Serial.read();
    
    // If the marker's found, next 6 characters are the colour
    
    if (inByte == '#') {
      while (pointer < 6) { // accumulate 6 chars
        buffer[pointer] = Serial.read(); // store in the buffer
        pointer++; // move the pointer forward by 1
      }
      delay(100); // wait 100ms between each send
    }
  }
}
    
int hex2dec(byte c) { // converts one HEX character into a number
  if (c >= '0' && c <= '9') {
    return c - '0';
  } 
  else if (c >= 'A' && c <= 'F') {
    return c - 'A' + 10;
  }
}

