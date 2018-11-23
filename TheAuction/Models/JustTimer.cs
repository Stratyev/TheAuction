using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Timers;

namespace TheAuction.Models
{
    public class JustTimer
    {
        private Timer Timer { get; set; }
        public JustTimer(long interval)
        {
            Timer = new Timer(interval);
            Timer.Elapsed += Target;
            Timer.AutoReset = true;
            Timer.Enabled = true;
        }
        private void Target(object state, ElapsedEventArgs e)
        {

        }
    }
}