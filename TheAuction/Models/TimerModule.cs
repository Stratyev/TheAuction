using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Threading;
using System.Timers;
using CodeFirst;

namespace TheAuction.Models
{// <system.webServer>    // In Web.config
 //<modules>

    public class OnEvent
    {
        MyDbContext _mdb { get { return MyDbContext.Create(); } }
        DataManager _dManager { get { return new DataManager(_mdb); } }
        public void OnEndRequest(object sender, EventArgs e)
        {
            HttpApplication app = (HttpApplication)sender;
            string url = app.Request.Url.OriginalString; // check which request the module responds to
            new LotChecksAddition(_dManager).AddCheck();
        }
    }

    public class TimerModule: IHttpModule
    {
        //Not using timer
        public void Init(HttpApplication app)
        {
            app.EndRequest += new OnEvent().OnEndRequest;
        }
        public void Dispose()
        { }

        //**************************************************************************************

        //Using timer
        //MyDbContext _mdb { get { return MyDbContext.Create(); } }
        //DataManager _dManager { get { return new DataManager(_mdb); } }
        //static Timer timer;
        //long interval;

        //public void Init(HttpApplication app)
        //{
        //    interval = 2000; //2 seconds
        //   // timer = new Timer(new TimerCallback(InvokeAddCheck), null, 0, interval); // For System.Threading Timer
        //    timer = new Timer(interval);
        //    timer.Elapsed += InvokeAddCheck;
        //    timer.AutoReset = true;
        //    timer.Enabled = true;
        //}

        //private void InvokeAddCheck(object obj, ElapsedEventArgs e)
        //{
        //    new LotChecksAddition(_dManager).AddCheck();
        //}
        //public void Dispose()
        //{ }
    }

}