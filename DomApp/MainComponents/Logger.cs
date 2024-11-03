using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainComponents
{
    public class Logger
    {
        public static void Write(string message)
        {
            try 
            {
                string myMessege = DateTime.Now.ToString(ApplicationSettings.DateFormat) + " " + message;
                if(ApplicationSettings.Debug) Console.WriteLine(myMessege);
                FileManagement.SaveLog("Logs", "Log" + DateTime.Now.ToString("_yyyy.MM.dd"), myMessege);
            }
            catch (Exception ex)
            {
                ExceptionManagement.Log(ex, "Logger", "Write");
            }
        }
    }
}