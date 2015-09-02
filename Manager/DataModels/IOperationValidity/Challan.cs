using Manager.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.DataModels
{
    public partial class Challan : IOperationValidity
    {
        public bool OperationValidity(Operations operation)
        {
            switch(operation)
            {
                case Operations.GenerateChallan:
                    return CanGenerateChallan();
                case Operations.ViewChallan:
                    return CanViewChallan();
            }
            return false;
        }

        public bool CanGenerateChallan()
        {
            if (!IsPersist)
                return true;
            return false;
        }

        public bool CanViewChallan()
        {
            return !CanGenerateChallan();
        }
    }
}
