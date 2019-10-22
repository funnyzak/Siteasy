using System;
using System.Collections.Generic;
using System.Linq;

namespace STA.Web.Plus.CTAA.KJ.Entity
{
    [Serializable]
    public class ConnectorConfig
    {
        /// <summary>
        ///  http://localhost:9779
        /// </summary>
        public string apiHost { get; set; }
        /// <summary>
        /// 48931735830
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 0af89e84b77e49da9ab3894e7b7d67aa
        /// </summary>
        public string secretKey { get; set; }
    }
}