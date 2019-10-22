using System;

namespace STA.Entity
{
    /// <summary>
    ///Attachment的实体。
    /// </summary>
    [Serializable]
    public class AttachmentInfo
    {
        #region 变量定义
        private int id = 0;
        private int uid = 0;
        private string username = string.Empty;
        private DateTime addtime = DateTime.Now;
        private string filename = string.Empty;
        private string description = string.Empty;
        private string filetype = string.Empty;
        private int filesize = 0;
        private string attachment = string.Empty;
        private int width = 0;
        private int height = 0;
        private int downloads = 0;
        private int attachcredits = 0;
        private string noupload = string.Empty;
        private string fileext = string.Empty;
        private int lastedituid = 0;
        private string lasteditusername = "";
        private DateTime lasteditime = DateTime.Now;


        #endregion



        #region 字段属性

        public int Lastedituid
        {
            get { return lastedituid; }
            set { lastedituid = value; }
        }
        public string Lasteditusername
        {
            get { return lasteditusername; }
            set { lasteditusername = value; }
        }
        public DateTime Lasteditime
        {
            get { return lasteditime; }
            set { lasteditime = value; }
        }
        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public string Fileext
        {
            get { return fileext; }
            set { fileext = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Noupload
        {
            get { return noupload; }
            set { noupload = value; }
        }

        public int Uid
        {
            get { return uid; }
            set { uid = value; }
        }

        public DateTime Addtime
        {
            get { return addtime; }
            set { addtime = value; }
        }

        public string Filename
        {
            get { return filename; }
            set { filename = value.Trim(); }
        }

        public string Description
        {
            get { return description; }
            set { description = value.Trim(); }
        }

        public string Filetype
        {
            get { return filetype; }
            set { filetype = value.Trim(); }
        }

        public int Filesize
        {
            get { return filesize; }
            set { filesize = value; }
        }

        public string Attachment
        {
            get { return attachment; }
            set { attachment = value.Trim(); }
        }

        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        public int Height
        {
            get { return height; }
            set { height = value; }
        }

        public int Downloads
        {
            get { return downloads; }
            set { downloads = value; }
        }

        public int Attachcredits
        {
            get { return attachcredits; }
            set { attachcredits = value; }
        }
        #endregion

        #region 副本
        public AttachmentInfo Clone()
        {
            return (AttachmentInfo)this.MemberwiseClone();
        }
        #endregion
    }
}
