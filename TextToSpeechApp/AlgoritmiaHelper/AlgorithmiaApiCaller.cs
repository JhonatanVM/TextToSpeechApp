using Algorithmia;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using TextToSpeechApp.Models;

namespace TextToSpeechApp
{
    public class AlgorithmiaApiCaller
    {
        public static Response GetSpeechFromAlgorithmia(string input)
        {
            var result = new Client(CredentialAccess.GetCredentialFile())
                        .algo("magicanded/algospeak/0.2.0")
                        .setOptions(timeout: 300)
                        .pipeJson<object>(input);

            return JsonConvert.DeserializeObject<Response>(result.result.ToString());
        }
    }
}
