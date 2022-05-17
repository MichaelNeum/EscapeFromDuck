const int statusLedPins[] = {8, 9, 10, 11};
const int connectionLedPin = 12;
const int buttonPins[] = {2, 3, 4};
const int xPin = A0;
const int yPin = A1;

int x = 0, xBefore = 0;
int y = 0, yBefore = 0;

bool buttonStatesBefore[] = {false, false, false};

String inputString = "";

void setup() {
  Serial.begin(9600);
  for(int i = 0; i < 4; i++) {
    pinMode(statusLedPins[i], OUTPUT);
  }
  pinMode(connectionLedPin, OUTPUT);
  for(int i = 0; i < 3; i++) {
    pinMode(buttonPins[i], INPUT_PULLUP);
  }
}

bool myblink();
bool * readButtons();
void handleInput(String input);
void writeLed(int led, bool state);
void sendButtonInfo(int button, bool state);
void sendJoyStickInfo(bool axis, int value);
String normalize(int value);

void loop() {
  bool * buttonStates = readButtons();
  for(int i = 0; i < 3; i++) {
    bool buttonState = *(i + buttonStates);
    if(buttonState != buttonStatesBefore[i]) {
      sendButtonInfo(i, buttonState);
    }
    buttonStatesBefore[i] = buttonState;
  }
  x = analogRead(xPin);
  y = analogRead(yPin);
  if(x != xBefore) sendJoyStickInfo(false, x);
  if(y != yBefore) sendJoyStickInfo(true, y);
  xBefore = x;
  yBefore = y;
}

bool myblink() {
  return millis() % 1000 > 500;
}

bool * readButtons() {
  static bool states[3];
  for(int i = 0; i < 3; i++) {
    states[i] = !digitalRead(buttonPins[i]);
  }
  return states;
}

void serialEvent() {
  while(Serial.available()) {
    char inChar = (char)Serial.read();
    if(inChar == '\n') {
      handleInput(inputString);
      inputString = "";
      break;
    }
    inputString += inChar;
  }
}

void handleInput(String input) {
  switch(input[0]) {
    case 'L':
      writeLed(input[1] - '0', input[4] == 'N');
      break;
    default:
      break;
  }
}

void writeLed(int led, bool state) {
  digitalWrite(led + 8, state);
}

void sendButtonInfo(int button, bool state) {
  Serial.println(state ? "B" + String(button) + "ONN" : "B" + String(button) + "OFF");
}

void sendJoyStickInfo(bool axis, int value) {
  Serial.println(axis ? "X" + normalize(value) : "Y" + normalize(value));
}

String normalize(int value) {
  String result = "";
  for(int i = 0; i < 4 - String(value).length(); i++) {
    result += '0';
  }
  return result + String(value);
}
