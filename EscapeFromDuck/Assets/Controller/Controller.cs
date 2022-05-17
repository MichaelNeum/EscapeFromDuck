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
        private static int x = 0;
        private static int y = 0;
        private static bool running = true;
        private static string input = "";
        static GameController()
        {
            serial.BaudRate = 9600;
            serial.PortName = "COM4";
            connectController();
            Thread readThread = new Thread(read);
            readThread.Start();
        }

        public static Led Led;

        public static int forward { get { return move; } }
        public static int xAxis { get { return x; } }
        public static int yAxis { get { return y; } }

        public static void read()
        {
            while(running)
            {
                input = serial.ReadLine();
                handleInput(input);
            }
        }

        public static void connectController()
        {
            try
            {
                serial.Open();
                write("L4ONN");
            }
            catch { }
        }

        public static void quit()
        {
            running = false;
            write("L4OFF");
            serial.Close();
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
                    break;
                case '2':
                    move = input[4] == 'N' ? -1 : 0;
                    break;
            }
        }
    }
}

