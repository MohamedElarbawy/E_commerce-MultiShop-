using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Interfaces
{
    public interface IMailService
    {
        void SendEmail(string emailTo, string subject, string body,IList<IFormFile>? attatchments=null);
    }
}
