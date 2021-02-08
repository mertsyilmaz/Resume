using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ResumeMvcCore.Models.GoogleReCAPTCHA
{
    public class ReCAPTCHAService
    {
        private ReCAPTCHA _reCAPTCHA;

        public ReCAPTCHAService(IOptions<ReCAPTCHA> options)
        {
            _reCAPTCHA = options.Value;
        }

        public virtual async Task<ReCAPTCHAResponse> Verify(string token)
        {
            ReCAPTCHAData reCAPTCHAData = new ReCAPTCHAData
            {
                response = token,
                secret = _reCAPTCHA.ReCAPTCHA_Secret_Key
            };

            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync($"https://www.google.com/recaptcha/api/siteverify?secret={reCAPTCHAData.secret}&response={reCAPTCHAData.response}");

            var recapResponse = JsonConvert.DeserializeObject<ReCAPTCHAResponse>(response);

            return recapResponse;
        }
    }
}
