using System;
using System.Collections.Generic;
using System.Linq;

namespace STA.Web.Plus.CTAA.KJ.Entity
{
    [Serializable]
    public class Examinee
    {
        /// <summary>
        /// Id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 邹梅洋
        /// </summary>
        public string realname { get; set; }
        /// <summary>
        /// MAN
        /// </summary>
        public string gender { get; set; }
        /// <summary>
        /// 中国
        /// </summary>
        public string country { get; set; }
        /// <summary>
        /// 汉族
        /// </summary>
        public string nation { get; set; }
        /// <summary>
        /// 河北
        /// </summary>
        public string province { get; set; }
        /// <summary>
        /// 513436200010016000
        /// </summary>
        public string idCard { get; set; }
        /// <summary>
        /// ZGSXHB0012019000005
        /// </summary>
        public string certNo { get; set; }
        /// <summary>
        /// http://localhost:9779/potato233/20191015/5ea011d3f4d04ad7a6058ae623cce6ff.jpeg
        /// </summary>
        public string photo { get; set; }
        /// <summary>
        /// 播音主持
        /// </summary>
        public string major { get; set; }
        /// <summary>
        /// 伍级
        /// </summary>
        public string level { get; set; }
        /// <summary>
        /// CertTime
        /// </summary>
        public int certTime { get; set; }
        /// <summary>
        /// http://localhost:9779/potato233/20191015/8ab8aa58512e4687b8c79545edf44a99.pdf
        /// </summary>
        public string certFile { get; set; }
        /// <summary>
        /// 这是一个说明
        /// </summary>
        public string remark { get; set; }
    }

}