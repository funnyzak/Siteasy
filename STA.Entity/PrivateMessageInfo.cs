using System;

namespace STA.Entity
{
    /// <summary>
    ///Pm的实体。
    /// </summary>
    [Serializable]
    public class PrivateMessageInfo
    {
        #region 变量定义
        private int m_id = 0;
        private string m_msgfrom = string.Empty;
        private int m_msgfromid = 0;
        private string m_msgto = string.Empty;
        private int m_msgtoid = 0;
        private Folder m_folder;
        private int m_new = 0;
        private string m_subject = string.Empty;
        private string m_content = string.Empty;
        private DateTime m_addtime = DateTime.Now;
        #endregion

        #region 构造函数
        public PrivateMessageInfo()
        {
        }

        public PrivateMessageInfo(int m_id, string m_msgfrom, int m_msgfromid, string m_msgto, int m_msgtoid, Folder m_folder, int m_new, string m_subject, string m_content)
        {
            this.Id = m_id;
            this.Msgfrom = m_msgfrom;
            this.Msgfromid = m_msgfromid;
            this.Msgto = m_msgto;
            this.Msgtoid = m_msgtoid;
            this.Folder = m_folder;
            this.New = m_new;
            this.Subject = m_subject;
            this.Content = m_content;
        }
        #endregion

        #region 字段属性
        public int Id
        {
            get { return m_id; }
            set { m_id = value; }
        }

        public string Msgfrom
        {
            get { return m_msgfrom; }
            set { m_msgfrom = value.Trim(); }
        }

        public int Msgfromid
        {
            get { return m_msgfromid; }
            set { m_msgfromid = value; }
        }

        public DateTime Addtime
        {
            get { return m_addtime; }
            set { m_addtime = value; }
        }

        public string Msgto
        {
            get { return m_msgto; }
            set { m_msgto = value.Trim(); }
        }

        public int Msgtoid
        {
            get { return m_msgtoid; }
            set { m_msgtoid = value; }
        }

        public Folder Folder
        {
            get { return m_folder; }
            set { m_folder = value; }
        }

        public int New
        {
            get { return m_new; }
            set { m_new = value; }
        }

        public string Subject
        {
            get { return m_subject; }
            set { m_subject = value.Trim(); }
        }

        public string Content
        {
            get { return m_content; }
            set { m_content = value.Trim(); }
        }
        #endregion

        #region 副本
        public PrivateMessageInfo Clone()
        {
            return (PrivateMessageInfo)this.MemberwiseClone();
        }
        #endregion
    }

    public enum Folder
    {
        收件 = 0,
        发件 = 1,
        草稿 = 2
    }
}
