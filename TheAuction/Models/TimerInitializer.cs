using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
//using System.Timers;
using CodeFirst;

namespace TheAuction.Models
{
    //public class TimerInitializer
    //{
    //    List<Initializer> Initializers { get; set; }
    //    MyDbContext _mdb { get { return MyDbContext.Create(); } }
    //    DataManager _dManager { get { return new DataManager(_mdb); } }
    //    List<LotGroup> groups;
    //    LotGroup lotGroup;
    //    public TimerInitializer()
    //    {
    //        groups = _dManager.LotGroupModel.getLotGroups();
    //        if (groups.Count != 0)
    //        {
    //            foreach (LotGroup lg in groups)
    //            {
    //                Initializers.Add(new Initializer(_dManager));
    //            }
    //        }
    //    }
    //}
    public class Initializer
    {
        MyDbContext _mdb { get { return MyDbContext.Create(); } }
        DataManager _dManager { get { return new DataManager(_mdb); } }
        Timer Timer1 { get; set; }
        //private DataManager _dManager { get; set; }
        public Initializer(/*DataManager dManager*/)
        {
            //_dManager = dManager;
            long interval = 1000; //1 second
            Timer1 = new Timer(new TimerCallback(InvokeAddCheck), null, 0, interval); // For System.Threading Timer
            //timer = new Timer(interval);
            //timer.Elapsed += InvokeAddCheck;
            //timer.AutoReset = true;
            //timer.Enabled = true;
        }

        private void InvokeAddCheck(object obj)
        {
            //Timer timer = (Timer)obj;
            new LotChecksAddition(_dManager/*, timer*/).AddCheck();
        }
    }
}