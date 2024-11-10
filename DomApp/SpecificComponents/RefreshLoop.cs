using MainComponents;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace DomApp
{
    public partial class MainWindow
    {
        public static bool RunLoop {  get; set; }

        public async Task RefreshLoop()
        {
            try
            {
                while (RunLoop)
                {
                    await Dispatcher.Invoke(async () => { await LoadValuesOfDom(); });
                    
                    ApplicationSettings.CheckRefresh();

                    Thread.Sleep(ApplicationSettings.LoopDelay);
                }
            }
            catch (Exception ex)
            {
                RunLoop = false;
                ExceptionManagement.Log(ex, "RefreshLoop");
            }
        }
    }
}