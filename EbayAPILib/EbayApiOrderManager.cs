using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eBay.Service.Core.Sdk;
using eBay.Service.Core.Soap;
using eBay.Service.Call;
using System.Transactions;
using ServiceInterfaceFramework.Common;

namespace EbayAPILibrary
{
    /// <summary>
    /// Ebay API 管理器
    /// </summary>
    public class EbayApiOrderManager : ApiOrderBase
    {
        public override string PlatformType
        {
            get { return "Ebay"; }
        }

        private IEnumerable<EbayConfig> InitializeConfig()
        {
            return new List<EbayConfig>() { /*some configuration*/};
        }

        protected override void GetOrdersCore(string customerID, int spanDays = 90)
        {
            var ebayConfigs = InitializeConfig();

            if (ebayConfigs == null || ebayConfigs.Count() == 0)
            {
                LogHelper.Debug(EbayLogMessage.NO_API_CONFIG);
                return;
            }

            foreach (var ebayConfig in ebayConfigs)
            {
                spanDays = spanDays <= 0 ? 90 : spanDays;

                //绑定请求签名头部
                ApiContext Context = ApiContextFactory.GetApiContext(ebayConfig);
                //启动监听
                GetOrdersCall apiCall = new GetOrdersCall(Context);
                DetailLevelCodeTypeCollection detailLevels = new DetailLevelCodeTypeCollection();//详情级别设置
                detailLevels.Add(DetailLevelCodeType.ReturnAll);
                apiCall.DetailLevelList = detailLevels;

                GetOrdersRequestType grt = new GetOrdersRequestType(); //获取订单状态设置
                grt.OrderRole = TradingRoleCodeType.Seller;            //订单角色，卖家的订单
                grt.OrderStatus = OrderStatusCodeType.All;             //订单状态，全部
                grt.CreateTimeFrom = DateTime.UtcNow.AddDays(-spanDays);
                grt.CreateTimeTo = DateTime.UtcNow;                    //最后下单时间

                LogHelper.Debug(EbayLogMessage.BEGIN_WHILE);

                MakeDoWhile(apiCall, grt, ebayConfig);
            }
        }

        private void MakeDoWhile(GetOrdersCall apiCall, GetOrdersRequestType grt, EbayConfig ebayConfig)
        {
            int pagenum = 1;
            do
            {
                PaginationType pagetype = new PaginationType();
                pagetype.EntriesPerPage = 50;
                pagetype.PageNumber = pagenum++;
                apiCall.Pagination = pagetype;

                LogHelper.Debug(EbayLogMessage.BEGIN_DOWNLOAD);

                //获取API订单
                var orders = ((GetOrdersResponseType)apiCall.ExecuteRequest(grt)).OrderArray;

                LogHelper.Debug(EbayLogMessage.END_DOWNLOAD);

                if (orders.Count == 0)
                {
                    LogHelper.Debug(EbayLogMessage.NO_ORDERS);
                    return;
                }

                //遍历API获取的订单数据
                foreach (OrderType order in orders)
                {
                    ForEveryOrderType(order, ebayConfig);
                }

            } while (apiCall.HasMoreOrders);
        }


        private void ForEveryOrderType(OrderType order, EbayConfig ebayConfig)
        {
            var orderTypeId = order.ShippingDetails.SellingManagerSalesRecordNumber.ToString();

            //update order

            //var dbEbayTrans = dataOrderRepository.Get(orderTypeId);
            //if (dbEbayTrans != null)
            //{
            //    dbEbayTrans.OrderStatus = order.OrderStatus.ToString();

            //    if (order.PaidTimeSpecified)
            //    {
            //        dbEbayTrans.PayTime = order.PaidTime;
            //    }

            //    dataOrderRepository.Update(dbEbayTrans);
            //    return;
            //}

            //convert the orders
            var ebayConvertor = new EbayOrderConvertor();
            var orderDto = ebayConvertor.ToDataOrder(order); //转换对象

            //save
            DistributionTransactionScopeBlock.New(() =>
            {
                //dataOrderRepository.Save(orderDto);
                LogHelper.Debug("EBAY获取订单成功,订单ID：" + orderDto.OrderID);
            });
        }
    }
}
