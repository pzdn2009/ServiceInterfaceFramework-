using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eBay.Service.Core.Soap;

namespace EbayAPILibrary
{
    public class EbayOrderConvertor
    {
        public EbayOrderConvertor()
        {
        }

        public OrderDto ToDataOrder(object onlineOrderObj)
        {
            var orderEntity = new OrderDto();

            var onlineOrder = onlineOrderObj as OrderType;

            orderEntity.OrderID = onlineOrder.ShippingDetails.SellingManagerSalesRecordNumber.ToString();
            orderEntity.OrderStatus = onlineOrder.OrderStatus.ToString();
            orderEntity.Platform = "Ebay";
            orderEntity.PayTime = onlineOrder.PaidTime; //付款时间
            orderEntity.ShopID = "";
            orderEntity.CustomerID = "";

            orderEntity.TotalAmount = Convert.ToDecimal(onlineOrder.Total.Value.ToString());
            orderEntity.CurrencyID = onlineOrder.AmountPaid.currencyID.ToString();

            orderEntity.OrderDate = onlineOrder.CreatedTime;

            return orderEntity;
        }
    }
}
