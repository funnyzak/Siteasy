using System;

namespace STA.Entity
{
    /// <summary>
    ///广告实体类。
    /// </summary>
    [Serializable]
    public class AdInfo
    {
        #region 变量定义
        private int id = 0;
        private string name = string.Empty;
        private AdStatus status = 0;
        private string filename = string.Empty;
        private AdType adtype = 0;
        private DateTime addtime = DateTime.Now;
        private DateTime startdate = DateTime.Now;
        private DateTime enddate = DateTime.Now;
        private int click = 0;
        private string paramarray = string.Empty;
        private string outdate = string.Empty;
        #endregion

        #region 字段属性
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Filename
        {
            get { return filename; }
            set { filename = value; }
        }

        public string Outdate
        {
            get { return outdate; }
            set { outdate = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value.Trim(); }
        }

        public AdStatus Status
        {
            get { return status; }
            set { status = value; }
        }

        public AdType Adtype
        {
            get { return adtype; }
            set { adtype = value; }
        }

        public DateTime Addtime
        {
            get { return addtime; }
            set { addtime = value; }
        }

        public DateTime Startdate
        {
            get { return startdate; }
            set { startdate = value; }
        }

        public DateTime Enddate
        {
            get { return enddate; }
            set { enddate = value; }
        }

        public int Click
        {
            get { return click; }
            set { click = value; }
        }

        public string Paramarray
        {
            get { return paramarray; }
            set { paramarray = value.Trim(); }
        }
        #endregion

        #region 副本
        public AdInfo Clone()
        {
            return (AdInfo)this.MemberwiseClone();
        }
        #endregion
    }

    public enum AdStatus
    {
        不生效 = 0, 生效 = 1, 永远生效 = 2
    }

    public enum AdType
    {
        文字 = 1, 图片 = 2, Flash = 3, 代码 = 4
    }
}
