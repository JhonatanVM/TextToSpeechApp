using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TextToSpeechApp.Models
{
    public class Response
    {
        [JsonProperty("isError")]
        public bool IsError { get; set; }

        [JsonProperty("errorMessage")]
        public string ErrorMessage { get; set; }

        [JsonProperty("errorCode")]
        public string ErrorCode { get; set; }

        [JsonProperty("results")]
        public UrlPath UrlPath { get; set; }
    }
}
