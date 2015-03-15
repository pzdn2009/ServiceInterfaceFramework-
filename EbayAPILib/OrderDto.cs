using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EbayAPILibrary
{
    /// <summary>
    /// 订单
    /// </summary>
    public class OrderDto
    {
        public long ID { get; set; }

        /// <summary>
        /// 订单ID
        /// </summary>
        public string OrderID { get; set; }

        /// <summary>
        /// 订单日期
        /// </summary>
        public DateTime? OrderDate { get; set; }

        /// <summary>
        /// 支付日期
        /// </summary>
        public DateTime? PayTime { get; set; }

        /// <summary>
        /// 订单总金额
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// 币种ID
        /// </summary>
        public string CurrencyID { get; set; }

        /// <summary>
        /// 平台类型
        /// </summary>
        public string Platform { get; set; }

        /// <summary>
        /// 店铺ID
        /// </summary>
        public string ShopID { get; set; }

        public string ShopName { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public string OrderStatus { get; set; }

        /// <summary>
        /// 客户ID
        /// </summary>
        public string CustomerID { get; set; }
    }
}
