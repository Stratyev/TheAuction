using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quartz;
using Quartz.Impl;
using TheAuction.Models;
using CodeFirst;

namespace TheAuction.Infrastructure.Quartz
{
    public class CheckCreatorScheduler
    {
        public MyDbContext _context = new MyDbContext();
        public void Start()
        {
            List<Lot> lots = _dManager.LotModel.getLots();
            foreach(Lot lot in lots)
            {
                IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
                scheduler.Start();
                IJobDetail job = JobBuilder.Create<CheckCreator>().Build();
                job.JobDataMap["lot"] = lot;
                job.JobDataMap["_dManager"] = _dManager;
                DateTimeOffset t = lot.Auction_end;
                ITrigger trigger = TriggerBuilder.Create()
                    .WithIdentity($"trigger{lot.Lot_id}", $"group{lot.Lot_id}").StartAt(t).WithSimpleSchedule(x => x.WithRepeatCount(0)).Build();

                scheduler.ScheduleJob(job, trigger);
            }
                
        }
        public DataManager _dManager
        {
            get
            {
                return new DataManager(_context);
            }
        }
    }
}