using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;
using System.ComponentModel;

namespace ShadeKey
{
    class Keyboard
    {
        /* Virtual Key Codes
         * https://docs.microsoft.com/en-us/windows/win32/inputdev/virtual-key-codes
         */

        private static Dictionary<byte, string> VK_CODES = new Dictionary<byte, string>()
        {
            { 0x08, "BACKPACE" },
            { 0X09, "TAB" },
            { 0x0D, "RETURN" },
            { 0x10, "SHIFT" },
            { 0x11, "CONTROL" },
            { 0x14, "CAPSLOCK" },
            { 0x1B, "ESCAPE" },
            { 0x20, "SPACEBAR"},
            { 0x30, "0" },
            { 0x31, "1" },
            { 0x32, "2" },
            { 0x33, "3" },
            { 0x34, "4" },
            { 0x35, "5" },
            { 0x36, "6" },
            { 0x37, "7" },
            { 0x38, "8" },
            { 0x39, "9" },
            { 0x41, "a" },
            { 0x42, "b" },
            { 0x43, "c" },
            { 0x44, "d" },
            { 0x45, "e" },
            { 0x46, "f" },
            { 0x47, "g" },
            { 0x48, "h" },
            { 0x49, "i" },
            { 0x4A, "j" },
            { 0x4B, "k" },
            { 0x4C, "l" },
            { 0x4D, "m" },
            { 0x4E, "n" },
            { 0x4F, "o" },
            { 0x50, "p" },
            { 0x51, "q" },
            { 0x52, "r" },
            { 0x53, "s" },
            { 0x54, "t" },
            { 0x55, "u" },
            { 0x56, "v" },
            { 0x57, "w" },
            { 0x58, "x" },
            { 0x59, "y" },
            { 0x5A, "z" }
        };

        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(int key);

        public delegate void keyIsPressed_Delegate(KeyValuePair<byte, string> keyValue);
        public event keyIsPressed_Delegate KeyIsPressed;

        private BackgroundWorker backgroundWorker;

        public Keyboard()
        {
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += BackgroundWorker_DoWork;
            backgroundWorker.RunWorkerAsync();
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Stopwatch idleTimer = new Stopwatch();
            idleTimer.Start();
            while (true)
            {
                foreach (var button in VK_CODES)
                {
                    if (GetAsyncKeyState(button.Key) != 0)
                    {
                        idleTimer.Restart();
                        while (GetAsyncKeyState(button.Key) != 0) { Thread.Sleep(100); }
                        KeyIsPressed(button);
                    }
                }

                if(idleTimer.Elapsed.TotalSeconds > 5)
                {
                    idleTimer.Stop();
                    Thread.Sleep(1000);
                }
                else
                {
                    Thread.Sleep(100);
                }
            }
        }
    }
}

