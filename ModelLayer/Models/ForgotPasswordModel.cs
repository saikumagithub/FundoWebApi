using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Models
{
    public  class ForgotPasswordModel
    {
        public long UserId { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }

       
    }
}
