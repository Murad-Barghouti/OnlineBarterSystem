using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBarterSystemDAL.Exceptions
{
    public class UnAutharizedException : Exception
    {
        public UnAutharizedException(string message) : base(message)
        {

        }
    }
}
