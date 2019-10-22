using System;
using System.Web;
using System.Data;
using System.Collections.Generic;

using STA.Common;
using STA.Entity;
using STA.Config;
using STA.Core;
using STA.Data;

namespace STA.Web.Admin
{
    public partial class schedulemanage : AdminPage
    {
        string action = STARequest.GetString("hidAction");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BuildData();
            }

            if (IsPostBack && action != string.Empty)
            {
                switch (action)
                {
                    case "exec":
                        Execute();
                        Message();
                        break;
                    case "del":
                        Message(ConUtils.EmptyChannel(STARequest.GetFormInt("hidValue", 0)));
                        break;
                }
                hidAction.Value = "";
                BuildData();
            }
        }

        protected void Execute()
        {
            STA.Config.Event[] events = ScheduleConfigs.GetConfig().Events;
            foreach (STA.Config.Event ev in events)
            {
                if (ev.Key == hidValue.Value.Trim())
                {
                    ((STA.Core.ScheduledEvents.IEvent)Activator.CreateInstance(Type.GetType(ev.ScheduleType))).Execute(HttpContext.Current);
                    STA.Core.ScheduledEvents.Event.SetLastExecuteScheduledEventDateTime(ev.Key, Environment.MachineName, DateTime.Now);
                    break;
                }
            }
        }

        protected void BuildData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("key");
            dt.Columns.Add("scheduletype");
            dt.Columns.Add("exetime");
            dt.Columns.Add("lastexecute");
            dt.Columns.Add("issystemevent");
            dt.Columns.Add("enable");
            STA.Config.Event[] events = ScheduleConfigs.GetConfig().Events;
            foreach (STA.Config.Event ev in events)
            {
                DataRow dr = dt.NewRow();
                dr["key"] = ev.Key;
                dr["scheduletype"] = ev.ScheduleType;
                if (ev.TimeOfDay != -1)
                {
                    dr["exetime"] = "定时执行:" + (ev.TimeOfDay / 60) + "时" + (ev.TimeOfDay % 60) + "分";
                }
                else
                {
                    dr["exetime"] = "周期执行:" + ev.Minutes + "分钟";
                }
                DateTime lastExecute = STA.Core.ScheduledEvents.Event.GetLastExecuteScheduledEventDateTime(ev.Key, Environment.MachineName);
                if (lastExecute == DateTime.MinValue)
                {
                    dr["lastexecute"] = "从未执行";
                }
                else
                {
                    dr["lastexecute"] = lastExecute.ToString("yyyy-MM-dd HH:mm:ss");
                }
                dr["issystemevent"] = ev.IsSystemEvent ? "系统级" : "用户级";
                dr["enable"] = ev.Enabled ? "启用" : "禁用";
                dt.Rows.Add(dr);
            }
            rptData.DataSource = dt;
            rptData.DataBind();
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

    }
}