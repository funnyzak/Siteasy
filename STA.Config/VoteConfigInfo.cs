using System;

namespace STA.Config
{
	/// <summary>
	/// Vote配置信息类
	/// </summary>
	[Serializable]
    public class VoteConfigInfo : IConfigInfo
    {
        #region 私有字段
        private int m_vcode = 1; //验证码验证
        private int m_login = 1; //登录验证
        private int m_phoneverify = 1; //邮件验证码验证
        private int m_timeinterval = 1440; //同IP投票时间间隔
        private string m_timeslot = "00:00|23:59"; //投票时间段
        private int m_infoinput = 0; //登记个人信息
        private string m_infofields = ""; //登记的个人信息字段
        private string m_forbidips = "";
        #endregion

        public VoteConfigInfo()
		{
        }

        #region 属性

        public string Infofields
        {
            get { return m_infofields; }
            set { m_infofields = value; }
        }

        public int Infoinput
        {
            get { return m_infoinput; }
            set { m_infoinput = value; }
        }

        public string Timeslot
        {
            get { return m_timeslot; }
            set { m_timeslot = value; }
        }
        public int Timeinterval
        {
            get { return m_timeinterval; }
            set { m_timeinterval = value; }
        }
        public int Phoneverify
        {
            get { return m_phoneverify; }
            set { m_phoneverify = value; }
        }
        public int Login
        {
            get { return m_login; }
            set { m_login = value; }
        }
        public int Vcode
        {
            get { return m_vcode; }
            set { m_vcode = value; }
        }
        public string Forbidips
        {
            get { return m_forbidips; }
            set { m_forbidips = value; }
        }
        #endregion

    }
}
