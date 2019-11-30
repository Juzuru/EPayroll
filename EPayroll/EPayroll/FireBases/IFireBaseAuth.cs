using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EPayroll.FireBases
{
    public interface IFireBaseAuth
    {
        Task<string> Login(string email, string password);
    }
}
