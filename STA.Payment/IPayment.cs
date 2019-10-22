using System;

namespace STA.Payment
{
    public interface IPayment
    {
        string Author();
        string DefaultDescription();
        string Description();
        string GetParms();
        string Name();
        string OfficeUrl();
        string Version();
        string GetPayUrl(string parmstr, string orderno, string subject, string body, string total_fee);
    }
}

