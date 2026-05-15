#include <SPI.h>
#include <nRF24L01.h>
#include <RF24.h>

//create an RF24 object
RF24 radio(8, 9);  // CE, CSN

//address through which two modules communicate.
const byte address[6] = "00001";
int mesaj[5];

#define sol_joy_y A3
#define sol_joy_x A2
#define sag_joy_x A1
#define sag_joy_y A0

void setup()
{
  radio.begin();
  radio.openWritingPipe(address);
  radio.setPALevel(RF24_PA_MIN);
  radio.stopListening();

  pinMode(sag_joy_x, INPUT);
  pinMode(sag_joy_y, INPUT);
  pinMode(sol_joy_x, INPUT);
  pinMode(sol_joy_y, INPUT);
  pinMode(4, INPUT);
}
void loop()
{
  // ---------------------butonlar---------------------
  mesaj[4] = digitalRead(4);
  // ---------------------sag---------------------
  mesaj[0] = map(analogRead(sag_joy_x), 0, 1023, -200, 200);
  mesaj[1] = map(analogRead(sag_joy_y), 0, 1023, -200, 200);
  
  if(mesaj[1] >= -10 && mesaj[1] <=10)
  {mesaj[1] = 0;}
    
  if(mesaj[0] >= -10 && mesaj[0] <=10)
  {mesaj[0] = 0;}
  
  // ---------------------sol---------------------
  mesaj[2] = map(analogRead(sol_joy_x), 0, 1023, 0, 400);
  mesaj[3] = map(analogRead(sol_joy_y), 0, 1023, -200, 200);
    
  if(mesaj[3] >= -10 && mesaj[3] <=10)
  {mesaj[3] = 0;}
  
  if(mesaj[2] >= -10 && mesaj[2] <=10)
  {mesaj[2] = 0;}
  if(mesaj[2] >= 390 && mesaj[2] <=400)
  {mesaj[2] = 400;}
  /*radio.write(&mesaj, sizeof(mesaj));*//*
  
  delay(50);
}
