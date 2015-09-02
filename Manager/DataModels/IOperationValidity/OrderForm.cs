using Manager.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.DataModels
{
    public partial class OrderForm : IOperationValidity
    {
        public bool OperationValidity(Operations operation)
        {
            if (Client == null)
                return false;
            switch(operation)
            {
                case Operations.SaveOrder:
                    return CanSaveOrder();

                case Operations.ViewOrder:
                    return CanViewOrder();
                
                case Operations.UpdateOrderItems:
                    return CanUpdateOrderItems();

                case Operations.EditOrderForm:
                    return CanEditOrder();

                case Operations.ResetOrderForm:
                    return CanResetOrderForm();

                case Operations.DiscardOrder:
                    return CanDiscardOrder();

                case Operations.ApproveOrder:
                    return CanApproveOrder();

                case Operations.CancelOrder:
                    return CanCancelOrder();

                case Operations.GenerateChallan:
                    return CanGenerateChallan();

                case Operations.ViewChallan:
                    return CanViewChallan();
            }
            return false;;
        }

        bool CanSaveOrder()
        {
            if (!IsPersist)
                return true;
            return false;
        }

        bool CanViewOrder()
        {
            return !CanSaveOrder();
        }

        bool CanEditOrder()
        {
            if (!IsPersist && !IsApproved)
                return true;
            return false;
        }

        private bool CanUpdateOrderItems()
        {
            return false;
        }

        bool CanResetOrderForm()
        {
            return CanSaveOrder();
        }

        bool CanDiscardOrder()
        {
            return CanSaveOrder();
        }

        bool CanApproveOrder()
        {
            if (IsPersist && !IsApproved)
                return true;
            return false;
        }

        private bool CanCancelOrder()
        {
            return false;
        }

        bool CanGenerateChallan()
        {
            if (IsApproved && IsPersist && Challan == null)
                return true;
            return false;
        }

        bool CanViewChallan()
        {
            if (Challan != null)
                return true;
            return false;
        }
    }
}
