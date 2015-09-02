using Manager.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.DataModels
{
    public partial class Client : IOperationValidity
    {
        public bool OperationValidity(Operations operation)
        {
            switch(operation)
            {
                case Operations.EditClient:
                    return CanEditClient();
            }
            return false;
        }

        bool CanEditClient()
        {
            if (!IsPersist)
                return true;
            return false;
        }
    }
}
