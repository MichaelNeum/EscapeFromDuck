using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;
using UnityEngine;

namespace ControllerSpace
{
    public class GameController : MonoBehaviour
    {
        private static SerialPort serial = new SerialPort();
        private static int move = 0;
        private static bool interact = false;
        private static int x = 0;
        private static int y = 0;
        private static bool running = true;
        private static string input = "";
        static GameController()
        {
            serial.BaudRate = 9600;
            serial.PortName = "COM4";
            connectController();
        }

        public static int forward { get { return move; } }
        public static int xAxis { get { return x; } }
        public static int yAxis { get { return y; } }

        public static bool interaction { get { return interact; } }

        public static void read()
        {
            while(running)
            {
                try
                {
                    input = serial.ReadLine();
                    handleInput(input);
                }
                catch { }
            }
        }

        public static void connectController()
        {
            try
            {
                serial.Open();
                write("L4ONN");
                running = true;
                Thread readThread = new Thread(read);
                readThread.Start();
            }
            catch 
            {
                running = false;
            }
        }

        public static void quit()
        {
            running = false;
            write("L4OFF");
            serial.Close();
        }

        public static void turnOnLed(int num)
        {
            write("L" + num + "ONN");
        }

        public static void turnOffAllLed()
        {
            for(int i = 0; i < 4; i++)
            {
                write("L" + i + "OFF");
            }
        }

        protected static void write(string message)
        {
            serial.WriteLine(message);
        }
        

        private static void handleInput(string input)
        {
            switch(input[0])
            {
                case 'B':
                    handleButtons(input);
                    break;
                case 'X':
                    x = toInt(input) - 1024 / 2;
                    break;
                case 'Y':
                    y = toInt(input) - 1024 / 2;
                    break;
                default:
                    break;
            }
        }

        private static int toInt(string value)
        {
            return (value[1] - '0') * 1000 + (value[2] - '0') * 100 + (value[3] - '0') * 10 + value[4] - '0';
        }

        private static void handleButtons(string input)
        {
            switch (input[1])
            {
                case '0':
                    move = input[4] == 'N' ? 1 : 0;
                    break;
                case '1':
                    interact = input[4] == 'N';
                    break;
                case '2':
                    move = input[4] == 'N' ? -1 : 0;
                    break;
            }
        }
    }
}

