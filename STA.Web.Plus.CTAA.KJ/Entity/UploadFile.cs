using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace STA.Web.Plus.CTAA.KJ.Entity
{
    public class UploadFile
    {
        /// <summary>
        /// http://localhost:9779
        /// </summary>
        public string host { get; set; }
        /// <summary>
        /// /potato233/20191029/
        /// </summary>
        public string path { get; set; }
        /// <summary>
        /// DUD4RWZZCCDBPWJKDQB7CJX8GTFBQ2NE.png
        /// </summary>
        public string originName { get; set; }
        /// <summary>
        /// DUD4RWZZCCDBPWJKDQB7CJX8GTFBQ2NE.png
        /// </summary>
        public string newName { get; set; }
        /// <summary>
        /// application/octet-stream
        /// </summary>
        public string mime { get; set; }
        /// <summary>
        /// Size
        /// </summary>
        public int size { get; set; }
        /// <summary>
        /// png
        /// </summary>
        public string ext { get; set; }
        /// <summary>
        /// http://localhost:9779/potato233/20191029/DUD4RWZZCCDBPWJKDQB7CJX8GTFBQ2NE.png
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// Thumbs
        /// </summary>
        public List<string> thumbs { get; set; }
    }
}