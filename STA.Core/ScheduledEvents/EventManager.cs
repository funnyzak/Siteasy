using System;
using System.Diagnostics;
using System.Web;
using System.Threading;

using STA.Config;
using STA.Common;
using System.Collections.Generic;

namespace STA.Core.ScheduledEvents
{
    /// <summary>
    /// EventManager is called from the EventHttpModule (or another means of scheduling a Timer). Its sole purpose
    /// is to iterate over an array of Events and deterimine of the Event's IEvent should be processed. All events are
    /// added to the managed threadpool. 
    /// </summary>
    public class EventManager
    {
        public static string RootPath;

        private EventManager()
        {
        }

        public static readonly int TimerMinutesInterval = 5;
        static EventManager()
        {
            if (ScheduleConfigs.GetConfig().TimerMinutesInterval > 0)
            {
                TimerMinutesInterval = ScheduleConfigs.GetConfig().TimerMinutesInterval;
            }
        }


        public static void Execute()
        {
            STA.Config.Event[] simpleItems = ScheduleConfigs.GetConfig().Events;
            Event[] items;

            List<Event> list = new List<Event>();

            foreach (STA.Config.Event newEvent in simpleItems)
            {
                if (!newEvent.Enabled)
                {
                    continue;
                }
                Event e = new Event();
                e.Key = newEvent.Key;
                e.Minutes = newEvent.Minutes;
                e.ScheduleType = newEvent.ScheduleType;
                e.TimeOfDay = newEvent.TimeOfDay;

                list.Add(e);
            }
            items = list.ToArray();
            Event item = null;

            if (items != null)
            {

                for (int i = 0; i < items.Length; i++)
                {
                    item = items[i];
                    if (item.ShouldExecute)
                    {
                        item.UpdateTime();
                        ConUtils.InsertLog(3, 0, "", 0, "", "", "定时任务", string.Format("执行定时任务 {0} .", item.ScheduleType));
                        IEvent e = item.IEventInstance;
                        ManagedThreadPool.QueueUserWorkItem(new WaitCallback(e.Execute));
                    }
                }
            }
        }
    }
}
