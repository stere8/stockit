using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockIT.BLL.Exceptions
{
    public class UserServiceException : Exception
    {
        public UserServiceException(string message) : base(message) { }
    }
}
