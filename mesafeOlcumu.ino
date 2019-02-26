#include <X113647Stepper.h> 

const int stepsPerRevolution = 48*48;
X113647Stepper myStepper(stepsPerRevolution, 8, 9, 10, 11);
int trigPin=5;
int echoPin=6;
unsigned long sure;
int mesafe;
int adim=0;


void setup() {
  pinMode(trigPin,OUTPUT);
  pinMode(echoPin,INPUT);
  myStepper.setSpeed(1000);
  Serial.begin(9600);

}

void loop() {
  digitalWrite(trigPin,HIGH);
  delayMicroseconds(10);
  digitalWrite(trigPin,LOW);


  sure=pulseIn(echoPin,HIGH);
  mesafe=(double)sure*0.034/2;
  if(mesafe<=52){ // max 50 cm için tarama yapacak
  Serial.println(mesafe);
  }
  else Serial.println(0);  //geçici olarak step motorla yapıldı servo motor kullanmak daha uygun olacaktır 
  if(adim<=9){
    myStepper.step(-205);
    adim++;
  }
  if(adim>9 && adim<=18){
    myStepper.step(205);
    adim++;
  }
  if(adim==19){
    adim=0;
    
  }
  
delay(50);
  
  }

