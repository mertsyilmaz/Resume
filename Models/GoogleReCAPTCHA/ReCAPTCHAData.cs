using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeMvcCore.Models.GoogleReCAPTCHA
{
    public class ReCAPTCHAData
    {
        public string response { get; set; }
        public string secret { get; set; }
    }
}
