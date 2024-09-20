using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SlnMain.Domain.Models
{
    public class APIResponse
    {
        public HttpStatusCode statusCode { get; set; }
        public bool IsExitoso { get; set; }
        public List<string> ErrorMessage { get; set; }
        public object Resultado { get; set; }
    }
}
