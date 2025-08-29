using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Application.Exceptions
{
    public class ValidationExceptions:BadRequesException
    {
        public required IEnumerable<string>  Errors { get; set; }
        public ValidationExceptions( string message="Bad Request")
            :base(message)
        {
            
        }
    }
}
