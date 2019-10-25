using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace STA.Web.Plus.CTAA.KJ.Entity
{
    public class HttpPostResponse
    {
        /// <summary>
        /// Data
        /// </summary>
        public JObject data { get; set; }
        /// <summary>
        /// FAIL
        /// </summary>
        public string operationState { get; set; }
        /// <summary>
        /// Errors
        /// </summary>
        public List<string> errors { get; set; }
        /// <summary>
        /// Success
        /// </summary>
        public bool success { get; set; }
    }
}