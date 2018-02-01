using System;
using System.Collections.Generic;
using System.Text;

namespace PropertyService.Domain.ModelView.External
{
    /// <summary>
    /// Class zipData.
    /// </summary>
    [Serializable]
    public class zipData
    {
        public string zip_code { get; set; }
        public string lat { get; set; }
        public string lng { get; set; }
        public string city { get; set; }
        public string state { get; set; }
    }

    /// <summary>
    /// Class RecaptchaResponse.
    /// </summary>
    [Serializable]
    public class RecaptchaResponse
    {
        public bool success { get; set; }
        public string challenge_ts { get; set; }
        public string hostname { get; set; }
   
    }

    /// <summary>
    /// Class RecaptchaRequest.
    /// </summary>
    [Serializable]
    public class RecaptchaRequest
    {
        public string secret { get; set; }
        public string response { get; set; }

    }
}
