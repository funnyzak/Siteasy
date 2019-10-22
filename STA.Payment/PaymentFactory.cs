using System;
using STA.Common;
using STA.Config;

namespace STA.Payment
{
    public class PaymentFactory
    {
        public static IPayment GetInstance(string PaymentName)
        {
            try
            {
                return (IPayment)Activator.CreateInstance(Type.GetType(string.Format("STA.Payment.{0}.Payment, STA.Payment.{0}", PaymentName), false, true));
            }
            catch (Exception ex)
            {
                STAException.WriteError(Utils.GetMapPath(BaseConfigs.GetSitePath + "/sta/logs/error"), "不存在STA.Payment." + PaymentName + ".dll这个文件,请检查BIN文件夹", ex);
                return null;
            }
        }
    }
}

