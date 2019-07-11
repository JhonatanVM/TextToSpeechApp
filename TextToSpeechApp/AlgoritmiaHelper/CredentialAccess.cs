using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TextToSpeechApp
{
    public class CredentialAccess
    {
        public static string GetCredentialFile()
        {
            using (var sr = File.OpenText(@"..\..\..\credential.json"))
            {
                var file = sr.ReadToEnd();
                var jObjectTemp = JObject.Parse(file);
                return jObjectTemp["api_key"].ToString();
            }
        }
    }
}
