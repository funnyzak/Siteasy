using System;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;

using STA.Common;
using STA.Entity;
using STA.Config;
using STA.Data;

namespace STA.Web.Admin
{
    public partial class scheduleadd : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            if (!IsPostBack)
            {
                for (int i = 0; i < 24; i++)
                {
                    hour.Items.Add(new ListItem(i.ToString("00"), i.ToString()));
                }
                for (int i = 0; i < 60; i++)
                {
                    minute.Items.Add(new ListItem(i.ToString("00"), i.ToString()));
                }
                if (STARequest.GetString("key") != "")
                {
                    ScheduleConfigInfo sci = ScheduleConfigs.GetConfig();
                    foreach (STA.Config.Event ev1 in sci.Events)
                    {
                        if (ev1.Key == STARequest.GetString("key"))
                        {
                            oldkey.Value = txtName.Text = ev1.Key;
                            rblEnable.SelectedValue = ev1.Enabled ? "1" : "0";
                            txtType.Text = ev1.ScheduleType;
                            if (ev1.TimeOfDay != -1)
                            {
                                int _hour = ev1.TimeOfDay / 60;
                                int _minute = ev1.TimeOfDay % 60;
                                type1.Checked = true;
                                hour.SelectedValue = _hour.ToString();
                                minute.SelectedValue = _minute.ToString();
                            }
                            else
                            {
                                type2.Checked = true;
                                timeserval.Text = ev1.Minutes.ToString();
                            }

                            if (ev1.IsSystemEvent)
                            {
                                txtName.Enabled = txtType.Enabled = rblEnable.Enabled = false;
                            }
                        }
                    }
                }
            }
        }

        private void SaveInfo_Click(object sender, EventArgs e)
        {
            ScheduleConfigInfo sci = ScheduleConfigs.GetConfig();
            if (type2.Checked && (timeserval.Text == "" || !Utils.IsNumeric(timeserval.Text)))
            {
                Message("周期执行时间必须为数值!", 2);
                return;
            }
            if (STARequest.GetString("key") == "")
            {
                foreach (STA.Config.Event ev1 in sci.Events)
                {
                    if (ev1.Key == txtName.Text.Trim())
                    {
                        Message("任务名称已经存在!", 2);
                        return;
                    }
                }
                STA.Config.Event ev = new STA.Config.Event();
                ev.Key = txtName.Text;
                ev.Enabled = rblEnable.SelectedValue == "1";
                ev.IsSystemEvent = false;
                ev.ScheduleType = txtType.Text.Trim();
                if (type1.Checked)
                {
                    ev.TimeOfDay = int.Parse(hour.Text) * 60 + int.Parse(minute.Text);
                    ev.Minutes = sci.TimerMinutesInterval;
                }
                else
                {
                    ev.Minutes = int.Parse(timeserval.Text.Trim());
                    ev.TimeOfDay = -1;
                }
                STA.Config.Event[] es = new STA.Config.Event[sci.Events.Length + 1];
                for (int i = 0; i < sci.Events.Length; i++)
                {
                    es[i] = sci.Events[i];
                }
                es[es.Length - 1] = ev;
                sci.Events = es;
            }
            else
            {
                foreach (STA.Config.Event ev1 in sci.Events)
                {
                    if (txtName.Text.Trim() != oldkey.Value && ev1.Key == txtName.Text.Trim())
                    {
                        Message("任务名称已经存在!", 2);
                        return;
                    }
                }
                foreach (STA.Config.Event ev1 in sci.Events)
                {
                    if (ev1.Key == oldkey.Value)
                    {
                        ev1.Enabled = rblEnable.SelectedValue == "1";
                        ev1.Key = txtName.Text;
                        ev1.ScheduleType = txtType.Text;
                        if (type1.Checked)
                        {
                            ev1.TimeOfDay = int.Parse(hour.Text) * 60 + int.Parse(minute.Text);
                            ev1.Minutes = sci.TimerMinutesInterval;
                        }
                        else
                        {
                            if (int.Parse(timeserval.Text.Trim()) < sci.TimerMinutesInterval)
                                ev1.Minutes = sci.TimerMinutesInterval;
                            else
                                ev1.Minutes = int.Parse(timeserval.Text.Trim());
                            ev1.TimeOfDay = -1;
                        }
                        break;
                    }
                }
            }
            ScheduleConfigs.SaveConfig(sci);
            Redirect("global_schedulemanage.aspx");
        }

        #region Web 窗体设计器生成的代码
        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.SaveInfo.Click += new EventHandler(this.SaveInfo_Click);
        }
        #endregion
    }
}