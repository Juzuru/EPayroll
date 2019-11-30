using System;
using System.Collections.Generic;
using System.Text;

namespace EPayroll.Models
{
    public class GoogleTokenModel
    {
        public string Access_token { get; set; }
        public string Id_token { get; set; }
        public string Refresh_token { get; set; }
        public int Expires_in { get; set; }
        public string Token_type { get; set; }
    }

    public class GoogleUserInfoModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Picture { get; set; }
    }
}
