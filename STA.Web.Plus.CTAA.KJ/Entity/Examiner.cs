using System;
using System.Collections.Generic;
using System.Linq;

namespace STA.Web.Plus.CTAA.KJ.Entity
{
    [Serializable]
    public class Examiner
    {
        /// <summary>
        /// AddTime
        /// </summary>
        public int addTime { get; set; }
        /// <summary>
        /// UpdateTime
        /// </summary>
        public int updateTime { get; set; }
        /// <summary>
        /// LatestSubmitTime
        /// </summary>
        public int latestSubmitTime { get; set; }
        /// <summary>
        /// ReviewTime
        /// </summary>
        public int reviewTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string reviewIdea { get; set; }
        /// <summary>
        /// ReviewUser
        /// </summary>
        public int reviewUser { get; set; }
        /// <summary>
        /// SUCCESS
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// 87466875783
        /// </summary>
        public string num { get; set; }
        /// <summary>
        /// MAN
        /// </summary>
        public string gender { get; set; }
        /// <summary>
        /// http://localhost:9779/potato233/20191015/d532cad4985149ee8591ffb2311bbbbb.jpg
        /// </summary>
        public string photo { get; set; }
        /// <summary>
        /// 朗康
        /// </summary>
        public string realName { get; set; }
        /// <summary>
        /// Birthday
        /// </summary>
        public int birthday { get; set; }
        /// <summary>
        /// 13001038910
        /// </summary>
        public string phone { get; set; }
        /// <summary>
        /// 123456789123456789
        /// </summary>
        public string idCard { get; set; }
        /// <summary>
        /// 本科
        /// </summary>
        public string resume { get; set; }
        /// <summary>
        /// GraduateDate
        /// </summary>
        public int graduateDate { get; set; }
        /// <summary>
        /// WorkDate
        /// </summary>
        public int workDate { get; set; }
        /// <summary>
        /// ArtLearnYear
        /// </summary>
        public int artLearnYear { get; set; }
        /// <summary>
        /// ArtWorkYear
        /// </summary>
        public int artWorkYear { get; set; }
        /// <summary>
        /// 影视动漫
        /// </summary>
        public string major { get; set; }
        /// <summary>
        /// 中央美院
        /// </summary>
        public string graduateSchool { get; set; }
        /// <summary>
        /// 总经理
        /// </summary>
        public string workTitle { get; set; }
        /// <summary>
        /// 北京雅美娱乐有限公司
        /// </summary>
        public string workUnit { get; set; }
        /// <summary>
        /// 播音主持
        /// </summary>
        public string applyMajor { get; set; }
        /// <summary>
        /// 北京视协
        /// </summary>
        public string workOrganization { get; set; }

        public string majorResume { get; set; }

        public string workResume { get; set; }
        /// <summary>
        /// 127.0.0.1
        /// </summary>
        public string ip { get; set; }
        /// <summary>
        /// WEB
        /// </summary>
        public string source { get; set; }
    }

}