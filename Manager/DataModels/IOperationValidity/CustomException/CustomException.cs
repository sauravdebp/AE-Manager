using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.DataModels.CustomException
{
    public class CustomException : Exception
    {
        
        INPC_impl sourceObject;

        public CustomException(String message, INPC_impl source)
        {
            
            sourceObject = source;
        }
    }
}
