using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

using STA.Common;
using STA.Config;
namespace STA.Payment
{
    public class AliPay
    {
        public string CreatUrl(string gateway, string service, string partner, string sign_type, string out_trade_no, string subject, string body, string payment_type, string total_fee, string show_url, string seller_email, string key, string return_url, string _input_charset, string notify_url, string logistics_type, string logistics_fee, string logistics_payment, string logistics_type_1, string logistics_fee_1, string logistics_payment_1, string quantity, string agent)
        {
            string[] sortedstr = BubbleSort(new string[] { 
                "service=" + service, "partner=" + partner, "subject=" + subject, "body=" + body, "out_trade_no=" + out_trade_no, "price=" + total_fee, "show_url=" + show_url, "payment_type=" + payment_type, "seller_email=" + seller_email, "notify_url=" + notify_url, "_input_charset=" + _input_charset, "return_url=" + return_url, "quantity=" + quantity, "logistics_type=" + logistics_type, "logistics_fee=" + logistics_fee, "logistics_payment=" + logistics_payment, 
                "logistics_type_1=" + logistics_type_1, "logistics_fee_1=" + logistics_fee_1, "agent=" + agent, "logistics_payment_1=" + logistics_payment_1
             });

            StringBuilder prestr = new StringBuilder();
            for (int i = 0; i < sortedstr.Length; i++)
            {
                if (i == (sortedstr.Length - 1))
                {
                    prestr.Append(sortedstr[i]);
                }
                else
                {
                    prestr.Append(sortedstr[i] + "&");
                }
            }

            prestr.Append(key);

            string sign = GetMD5(prestr.ToString(), _input_charset);

            StringBuilder parameter = new StringBuilder();
            parameter.Append(gateway);
            for (int j = 0; j < sortedstr.Length; j++)
            {
                parameter.Append(sortedstr[j].Split('=')[0] + "=" + HttpUtility.UrlEncode(sortedstr[j].Split('=')[1]) + "&");
            }
            parameter.Append("sign=" + sign + "&sign_type=" + sign_type);
            return parameter.ToString();
        }

        public string GetPayUrl(string agent, string parmstr, string orderno, string service, string subject, string body, string total_fee)
        {
            string[] parms = parmstr.Split(new char[] { ',' });
            string out_trade_no = orderno;
            string gateway = "https://mapi.alipay.com/gateway.do?";
            string partner = parms[2];
            string sign_type = "MD5";
            string payment_type = "1";
            string quantity = "1";
            string showurl = "http://" + STARequest.GetRootURI() + BaseConfigs.GetSitePath + "/user/orderdetail.aspx?orderno=" + orderno;
            showurl = HttpContext.Current.Server.UrlEncode(showurl);
            string seller_email = parms[0];
            string key = parms[1];
            string returnurl = "http://" + STARequest.GetRootURI() + BaseConfigs.GetSitePath + "/user/returnpage_pay_ali.aspx";
            string notify_url = "http://" + STARequest.GetRootURI() + BaseConfigs.GetSitePath + "/user/returnpage_pay_ali.aspx";
            string input_charset = "utf-8";
            string logistics_type = "EXPRESS";
            string logistics_fee = "0";
            string logistics_payment = "BUYER_PAY";
            string logistics_type_1 = "POST";
            string logistics_fee_1 = "0";
            string logistics_payment_1 = "BUYER_PAY";
            return this.CreatUrl(gateway, service, partner, sign_type, out_trade_no, subject, body, payment_type, total_fee, showurl, seller_email, key, returnurl, input_charset, notify_url, logistics_type, logistics_fee, logistics_payment, logistics_type_1, logistics_fee_1, logistics_payment_1, quantity, agent);
        }

        public static string GetMD5(string s, string _input_charset)
        {
            byte[] buffer = new MD5CryptoServiceProvider().ComputeHash(Encoding.GetEncoding(_input_charset).GetBytes(s));
            StringBuilder builder = new StringBuilder(0x20);
            for (int i = 0; i < buffer.Length; i++)
            {
                builder.Append(buffer[i].ToString("x").PadLeft(2, '0'));
            }
            return builder.ToString();
        }

        public static string[] BubbleSort(string[] r)
        {
            for (int i = 0; i < r.Length; i++)
            {
                bool flag = false;
                for (int j = r.Length - 2; j >= i; j--)
                {
                    if (string.CompareOrdinal(r[j + 1], r[j]) < 0)
                    {
                        string str = r[j + 1];
                        r[j + 1] = r[j];
                        r[j] = str;
                        flag = true;
                    }
                }
                if (!flag)
                {
                    return r;
                }
            }
            return r;
        }
    }
}

