using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace ShadeKey
{
    class LogFile
    {
        private string logFileName = string.Empty;
        private int buffSize = 0;
        private List<string> buff = new List<string>();

        public LogFile(string fileName,int buffSize)
        {
            this.buffSize = buffSize;
            logFileName = fileName;
        }

        public string ReadAll()
        {
            return File.ReadAllText(logFileName);
        }

        public void AppendData(string data)
        {
            if(buff.Count < buffSize)
            {
                buff.Add(data);
            }
            else
            {
                string dataToAppend = string.Empty;
                foreach(string _d in buff)
                {
                    dataToAppend += _d;
                }
                File.AppendAllText(logFileName, dataToAppend);
                buff.Clear();
            }
        }
    }
}
