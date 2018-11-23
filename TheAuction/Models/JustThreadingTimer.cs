using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;

namespace TheAuction.Models
{
    public class JustThreadingTimer
    {
        private Timer Timer { get; set; }
        public JustThreadingTimer(long interval)
        {
            Timer = new Timer(new TimerCallback(Target), null, 0, interval);
        }
        private void Target(object state)
        {

        }
    }
}