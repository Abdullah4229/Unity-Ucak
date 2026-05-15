#include <SPI.h>
#include <nRF24L01.h>
#include <RF24.h>

//RX RF24
RF24 radio(9, 8);  // CE, CSN
//GND, 9(CE), 13(SCK), 12(MISO)
//3V3, 8(CSN), 11(MOSI), -
//(10-100 uF 16V kutuplu kondansatör)

const byte address[6] = "00001";
int mesaj[5];

void setup()
{
  while (!Serial);
    Serial.begin(9600);
  
  radio.begin();
  radio.openReadingPipe(0, address);
  radio.setPALevel(RF24_PA_MIN);
  radio.startListening();
}

void loop()
{
  if (radio.available())
  {
    radio.read(&mesaj, sizeof(mesaj));
    Serial.print(mesaj[0]);
    Serial.print(",");
    Serial.print(mesaj[1]);
    Serial.print(",");
    Serial.print(mesaj[2]);
    Serial.print(",");
    Serial.print(mesaj[3]);
    Serial.print(",");
    Serial.println(mesaj[4]);
  }
  delay(50);
}
