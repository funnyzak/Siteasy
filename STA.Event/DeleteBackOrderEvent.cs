using System;
using System.Text;
using STA.Core.ScheduledEvents;

using STA.Core;
using STA.Data;
using STA.Config;
namespace STA.Event
{
    /// <summary>
    /// 生成全站
    /// </summary>
    public class DeleteBackOrder : IEvent
    {
        #region IEvent 成员

        public void Execute(object state)
        {
            Pays.DelBackOrder(GeneralConfigs.GetConfig().Orderbackday);
        }

        #endregion
    }
}
