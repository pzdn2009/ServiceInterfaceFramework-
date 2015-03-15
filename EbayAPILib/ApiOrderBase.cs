using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EbayAPILibrary
{
    public abstract class ApiOrderBase 
    {
        //protected IDataOrderRepository dataOrderRepository;
        //protected IOrderItemRepository orderItemRepository;

        public ApiOrderBase()
        {
        }

        public void GetOrders(int spanDays = 3)
        {
            this.GetOrdersCore(null, spanDays);
        }

        public void GetOrders(string customerID, int spanDays = 90)
        {
            this.GetOrdersCore(customerID, spanDays);
        }

        protected abstract void GetOrdersCore(string customerID = null, int spanDays = 90);

        public abstract string PlatformType { get; }
    }
}
