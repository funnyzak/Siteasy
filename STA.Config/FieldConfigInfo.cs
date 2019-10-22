using System;

namespace STA.Config
{
    /// <summary>
    ///  基本设置描述类, 加[Serializable]标记为可序列化
    /// </summary>
    [Serializable]
    public class FieldConfigInfo : IConfigInfo
    {
        #region 私有字段
        private string contentAuthor = string.Empty;
        private string contentSource = string.Empty;
        private int popWinOverlay = 10;
        private int maxFavouriteMenuCount = 15;
        #endregion

        #region 属性
        public string ContentAuthor
        {
            get { return contentAuthor; }
            set { contentAuthor = value; }
        }
        public string ContentSource
        {
            get { return contentSource; }
            set { contentSource = value; }
        }
        public int PopWinOverlay
        {
            get { return popWinOverlay; }
            set { popWinOverlay = value; }
        }

        public int MaxFavouriteMenuCount
        {
            get { return maxFavouriteMenuCount; }
            set { maxFavouriteMenuCount = value; }
        }
        #endregion
    }
}