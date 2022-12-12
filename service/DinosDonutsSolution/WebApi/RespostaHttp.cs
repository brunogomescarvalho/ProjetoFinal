using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi
{
    public class RespostaHttp
    {
         public int StatusCode { get; set; }
        public string Message { get; set; }

        public RespostaHttp(int status, string message)
        {
            this.StatusCode = status;
            this.Message = message;
        }
    }
}