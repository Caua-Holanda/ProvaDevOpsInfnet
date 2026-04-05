using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvaMedGroup.DomainModel.Exceptions
{
    public class TratedExceptions : Exception
    {
        public TratedExceptions(string message) : base(message)
        {

        } 

    }
}
