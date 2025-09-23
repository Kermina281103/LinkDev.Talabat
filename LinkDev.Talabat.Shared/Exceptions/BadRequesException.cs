using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Shared.Exceptions
{
    public class BadRequesException:ApplicationException
    {

        public BadRequesException(string? message)
            :base(message)
        {
            
        }
    }
}
