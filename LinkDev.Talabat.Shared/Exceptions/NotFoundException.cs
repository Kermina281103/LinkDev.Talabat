using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Shared.Exceptions
{
    public class NotFoundException:ApplicationException
    {
        public NotFoundException(string name ,object key)
            :base($"The {name} with id : {key} is not found.")
        {
            
        }
    }
}
