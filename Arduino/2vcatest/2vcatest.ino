#include <Wire.h>
#include "Haptic_Driver.h"

#define NUMBER_OF_SENSORS 2
Haptic_Driver hapDrive[NUMBER_OF_SENSORS];

void TCA9548A(uint8_t bus){
  Wire.beginTransmission(0x70);  // TCA9548A address
  Wire.write(1 << bus);          // send byte to select bus
  Wire.endTransmission();
  //Serial.print(bus);
}

hapticSettings lfi;

void setup() {
  // put your setup code here, to run once:
  //start
  Wire.begin();
  Serial.begin(115200);

  lfi.motorType = LRA_TYPE;
  lfi.absVolt = 3.5;
  lfi.nomVolt = 2.47;
  lfi.currMax = 295.1;
  lfi.impedance = 8.37;
  lfi.lraFreq = 80;

  for (int i; i<NUMBER_OF_SENSORS; i++) {
    TCA9548A(i);
    hapDrive[i].begin();

    if (!hapDrive[i].begin())
      Serial.println("Could not communicate with Haptic Driver1.");
    
    hapDrive[i].enableFreqTrack(false);
    hapDrive[i].setOperationMode(DRO_MODE);

    if (!hapDrive[i].setMotor(lfi)) {
      Serial.println("Setting failed");
    }

    hapDrive[i].setVibrate(0);
  }
}

//just in case
int event = 0; 

//motors
int indEx = 0;
int indFlex = 1;

char buf[3];

void copy(int* src, int* dst, int len) {
    memcpy(dst, src, sizeof(src[0])*len);
}

void loop() {
  for (int i; i<NUMBER_OF_SENSORS; i++) {
    event = hapDrive[i].getIrqEvent();
    hapDrive[i].clearIrq(event);
  }

  if (Serial.available() > 2) {
    //from unity
    Serial.readBytes(buf, 3);
    uint8_t setting = int(buf[0]);
    uint8_t command = int(buf[1]);
    uint8_t duration = int(buf[2]);

    //manual check
    // int setting = Serial.parseInt();
    // int command = Serial.parseInt();
    // int duration = Serial.parseInt();

    Serial.print("Setting read: ");
    Serial.println(setting);
    Serial.print("Command read: ");
    Serial.println(command);

    if (setting == 0){
      return;
    }

    int amp = 127;
    if (command == 0){
      amp = 0;
    }

    int targetMotor;

    switch (setting){
      case 1:
        targetMotor = indEx;
        break;
      case 2:
        targetMotor = indFlex;
        break;
    }
    
    Serial.print("Target Motors: ");
    Serial.println(targetMotor);

    Serial.print("Vibrating with amplitude: ");
    Serial.println(amp);

    TCA9548A(targetMotor);
    hapDrive[targetMotor].setVibrate(amp);

    if (command == 2) {
      Serial.print("Vibrating with duration(ms): ");
      Serial.println(duration * 250);
      delay(duration * 250);
      hapDrive[targetMotor].setVibrate(0);
    }
  }
}
