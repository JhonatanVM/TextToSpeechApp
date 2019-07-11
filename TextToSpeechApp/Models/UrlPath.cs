using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TextToSpeechApp.Models
{
    public class UrlPath
    {
        [JsonProperty("outputUrl")]
        public string Path { get; set; }
    }
}
