using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Shared.Exceptions
{
   public class UnAuthorizedExceptoin:ApplicationException
    {
        public UnAuthorizedExceptoin(string message):base(message)
        {
            
        }
    }
}
