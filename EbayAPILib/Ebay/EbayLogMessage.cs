using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EbayAPILibrary
{
    public class EbayLogMessage
    {
        public readonly static string BEGIN_WHILE = "开始DO循环";
        public readonly static string BEGIN_DOWNLOAD = "开始下载EBAY订单";

        public readonly static string END_DOWNLOAD = "结束下载EBAY订单";
        public readonly static string NO_ORDERS = "订单数量为0！";
        public readonly static string ORDER_EXIST = "订单已经存在！";
        public readonly static string NEW_ORDER = "新增同步订单";

        public readonly static string NO_API_CONFIG = "无API配置！";
    }
}
