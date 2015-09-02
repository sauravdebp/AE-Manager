using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Utilities
{
    public enum Operations
    {
        SaveOrder,
        ViewOrder,
        UpdateOrderItems,
        EditOrderForm,
        ResetOrderForm,
        DiscardOrder,
        ApproveOrder,
        CancelOrder,
        GenerateChallan,
        ViewChallan,
        EditClient
    };

    public interface IOperationValidity
    {
        Boolean OperationValidity(Operations operation);
    }
}
