using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace Ex3.libs
{
    public class Log
    {
        //DateTime _ultimaobterlogs;
        private string _destino;

        public Log(string destino)
        {
            _destino = destino;
            string[] subdir = destino.Split('/');
            string x = string.Empty;
            for (int i = 0; i <= subdir.Length - 2; i++)
                if(i==0)
                    x += subdir[i];
                else
                    x += $"/{subdir[i]}";

            if (!Directory.Exists(x))
                Directory.CreateDirectory(x);
                

            if(!File.Exists(destino))
                File.Create(destino).Close();
        }

        public bool EscreverLog(string r)
        {
            EventWaitHandle waitHandle = new EventWaitHandle(true, EventResetMode.AutoReset, "SHARED_BY_ALL_PROCESSES");

            waitHandle.WaitOne();
            if (!String.IsNullOrEmpty(r) && !String.IsNullOrWhiteSpace(r))
            {
                StreamWriter fw = new StreamWriter(_destino, true);
                fw.WriteLine(string.Format("{0} : {1}",DateTime.Now.ToString(),r));
                fw.Close();
                waitHandle.Set();
                return true;
            }
            waitHandle.Set();
            return false;
        }

        public List<string> ObterLogs()
        {
            //if ((DateTime.Now - _ultimaobterlogs).Seconds > 30)
            //{
            EventWaitHandle waitHandle = new EventWaitHandle(true, EventResetMode.AutoReset, "SHARED_BY_ALL_PROCESSES");
            waitHandle.WaitOne();
            StreamReader fr = new StreamReader(_destino);
                List<string> r = new List<string>();
                while (!fr.EndOfStream)
                    r.Add(fr.ReadLine());
                fr.Close();
            waitHandle.Set();
                //_ultimaobterlogs = DateTime.Now;
                return r;
            //}
            //return null;
        }

        public void LimparLogs()
        {
            EventWaitHandle waitHandle = new EventWaitHandle(true, EventResetMode.AutoReset, "SHARED_BY_ALL_PROCESSES");
            waitHandle.WaitOne();
            StreamWriter fw = new StreamWriter(_destino, false);
            fw.Close();
            waitHandle.Set();
        }
    }
}
