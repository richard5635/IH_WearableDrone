namespace {
  const int AVERAGE_NUM = 10;
  const int BASE_X      = 530;
  const int BASE_Y      = 519;
  const int BASE_Z      = 545;
}
  
void setup()
{
  Serial.begin(9600);
  pinMode(13, OUTPUT);
}

void readAccelerometer()
{
  int x = 0, y = 0, z = 0;
  for (int i = 0; i < AVERAGE_NUM; ++i) {
    x += analogRead(0);
    y += analogRead(1);
    z += analogRead(2);
  }
  x /= AVERAGE_NUM;
  y /= AVERAGE_NUM;
  z /= AVERAGE_NUM;
  
  const int angleX = atan2(x - BASE_X, z - BASE_Z) / PI * 180;
  const int angleY = atan2(y - BASE_Y, z - BASE_Z) / PI * 180; 

  Serial.print(angleX);
  Serial.print("\t");
  Serial.print(angleY);
  Serial.println("");
}

void setLed()
{
  if ( Serial.available() ) {
    char mode = Serial.read();
    switch (mode) {
      case '0' : digitalWrite(13, LOW);  break;
      case '1' : digitalWrite(13, HIGH); break;
    }
  }
}

void loop()
{
  //readAccelerometer();
  setLed();
}
