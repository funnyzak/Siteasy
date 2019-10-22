using System;
using System.Text;
using System.Text.RegularExpressions;

namespace STA.Common
{
    public class TypeParse
    {

        /// <summary>
        /// string��ת��Ϊbool��
        /// </summary>
        /// <param name="strValue">Ҫת�����ַ���</param>
        /// <param name="defValue">ȱʡֵ</param>
        /// <returns>ת�����bool���ͽ��</returns>
        public static bool StrToBool(object expression, bool defValue)
        {
            if (expression != null)
            {
                return StrToBool(expression.ToString(), defValue);
            }
            return defValue;
        }

        /// <summary>
        /// string��ת��Ϊbool��
        /// </summary>
        /// <param name="strValue">Ҫת�����ַ���</param>
        /// <param name="defValue">ȱʡֵ</param>
        /// <returns>ת�����bool���ͽ��</returns>
        public static bool StrToBool(string expression, bool defValue)
        {
            if (expression != null)
            {
                if (string.Compare(expression, "true", true) == 0)
                {
                    return true;
                }
                else if (string.Compare(expression, "false", true) == 0)
                {
                    return false;
                }
            }
            return defValue;
        }

        /// <summary>
        /// ������ת��ΪInt32����
        /// </summary>
        /// <param name="strValue">Ҫת�����ַ���</param>
        /// <param name="defValue">ȱʡֵ</param>
        /// <returns>ת�����int���ͽ��</returns>
        public static int ObjectToInt(object expression, int defValue)
        {
            if (expression != null)
            {
                return StrToInt(expression.ToString(), defValue);
            }
            return defValue;
        }


        public static int ObjectToInt(object expression)
        {
            return ObjectToInt(expression, 0);
        }


        /// <summary>
        /// ������ת��ΪInt32����
        /// </summary>
        /// <param name="strValue">Ҫת�����ַ���</param>
        /// <param name="defValue">ȱʡֵ</param>
        /// <returns>ת�����int���ͽ��</returns>
        public static int StrToInt(object expression, int defValue)
        {
            if (expression != null)
            {
                return StrToInt(expression.ToString(), defValue);
            }
            return defValue;
        }

        public static int StrToInt(object expression)
        {
            return StrToInt(expression, 0);
        }

        /// <summary>
        /// ������ת��ΪInt32����
        /// </summary>
        /// <param name="str">Ҫת�����ַ���</param>
        /// <param name="defValue">ȱʡֵ</param>
        /// <returns>ת�����int���ͽ��</returns>
        public static int StrToInt(string str, int defValue)
        {
            if (string.IsNullOrEmpty(str) || str.Trim().Length >= 11 || !Regex.IsMatch(str.Trim(), @"^([-]|[0-9])[0-9]*(\.\w*)?$"))
                return defValue;

            int rv;
            if (Int32.TryParse(str, out rv))
                return rv;

            return Convert.ToInt32(StrToFloat(str, defValue));
        }

        /// <summary>
        /// ������ת��Ϊ����ʱ������
        /// </summary>
        /// <param name="obj">Ҫת���Ķ���</param>
        /// <returns>ת�����int���ͽ��</returns>
        public static DateTime StrToDateTime(object obj)
        {
            return StrToDateTime(obj, DateTime.Now);
        }

        /// <summary>
        /// ������ת��Ϊ����ʱ������
        /// </summary>
        /// <param name="str">Ҫת�����ַ���</param>
        /// <param name="defValue">ȱʡֵ</param>
        /// <returns>ת�����ʱ�����ͽ��</returns>
        public static DateTime StrToDateTime(object str, DateTime defValue)
        {
            if (str != null && str.ToString() != string.Empty)
            {
                DateTime dateTime;
                if (DateTime.TryParse(str.ToString(), out dateTime))
                    return dateTime;
            }
            return defValue;
        }

        /// <summary>
        /// string��ת��Ϊfloat��
        /// </summary>
        /// <param name="strValue">Ҫת�����ַ���</param>
        /// <param name="defValue">ȱʡֵ</param>
        /// <returns>ת�����int���ͽ��</returns>
        public static float StrToFloat(object strValue, float defValue)
        {
            if ((strValue == null))
            {
                return defValue;
            }

            return StrToFloat(strValue.ToString(), defValue);
        }

        /// <summary>
        /// ������ת��Ϊ�ַ���
        /// </summary>
        /// <param name="strValue">Ҫת�����ַ���</param>
        /// <param name="defValue">Ĭ��ֵ</param>
        /// <returns></returns>
        public static string ObjToString(object strValue, string defValue)
        {
            if (strValue == null) return defValue;
            return strValue.ToString() == string.Empty ? defValue : strValue.ToString();
        }

        /// <summary>
        /// ������ת��Ϊ�ַ�����Ĭ��ֵΪ��
        /// </summary>
        /// <param name="strValue">Ҫת���ַ���</param>
        /// <returns></returns>
        public static string ObjToString(object strValue)
        {
            return ObjToString(strValue, string.Empty);
        }

        /// <summary>
        /// string��ת��Ϊfloat��
        /// </summary>
        /// <param name="strValue">Ҫת�����ַ���</param>
        /// <param name="defValue">ȱʡֵ</param>
        /// <returns>ת�����float���ͽ��</returns>
        public static float StrToFloat(string strValue, float defValue)
        {
            if (strValue == null)
            {
                return defValue;
            }

            float intValue = defValue;
            if (strValue != null)
            {
                bool IsFloat = Regex.IsMatch(strValue.Trim(), @"^([-]|[0-9])[0-9]*(\.\w*)?$");
                if (IsFloat)
                {
                    float.TryParse(strValue.Trim(), out intValue);
                }
            }
            return intValue;
        }

        /// <summary>
        /// string��ת��Ϊfloat��
        /// </summary>
        /// <param name="strValue">Ҫת�����ַ���</param>
        /// <param name="defValue">ȱʡֵ</param>
        /// <returns>ת�����decimal���ͽ��</returns>
        public static Decimal StrToDecimal(object strValue, decimal defValue)
        {
            if ((strValue == null))
            {
                return defValue;
            }

            return StrToDecimal(strValue.ToString(), defValue);
        }

        public static Decimal StrToDecimal(object strValue)
        {
            return StrToDecimal(strValue, 0);
        }


        public static Decimal StrToDecimal(string strValue, decimal defValue)
        {
            if ((strValue == null) || (strValue.Length > 16))
            {
                return defValue;
            }

            decimal intValue = defValue;
            if (strValue != null)
            {
                if (!Decimal.TryParse(strValue, out intValue)) { return defValue; }
            }
            return intValue;
        }

    }
}
