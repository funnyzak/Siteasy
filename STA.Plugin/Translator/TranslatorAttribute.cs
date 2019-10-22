using System;
using System.Text;

namespace STA.Plugin.Translator
{

    #region 定制语言翻译插件属性值

    /// <summary>
    /// 定制语言翻译的插件属性值
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class TranslatorAttribute : System.Attribute
    {
        private string _plugInName;

        private string _version = null;

        private string _author = null;

        private string _dllFileName = null;


        public TranslatorAttribute(string Name)
            : base()
        {
            _plugInName = Name;
            return;
        }

        /// <summary>
        /// 插件名称
        /// </summary>
        public string PlugInName
        {
            get { return _plugInName; }
            set { _plugInName = value; }
        }

        /// <summary>
        /// 版本
        /// </summary>
        public string Version
        {
            get { return _version; }
            set { _version = value; }
        }

        /// <summary>
        /// 作者
        /// </summary>
        public string Author
        {
            get { return _author; }
            set { _author = value; }
        }

        /// <summary>
        /// DLL 文件名称
        /// </summary>
        public string DllFileName
        {
            get { return _dllFileName; }
            set { _dllFileName = value; }
        }
    }
    #endregion

}
