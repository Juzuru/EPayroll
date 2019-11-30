using System;
using System.Collections.Generic;
using System.Text;

namespace EPayroll.Models
{
    public class AccountModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsRemove { get; set; }
    }

    public class AccountLoginModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Picture { get; set; }
    }

    public class AccountTokenModel
    {
        public string TokenType { get; set; }
        public string Token { get; set; }
    }
}
