using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eBay.Service.Core.Sdk;
using eBay.Service.Core.Soap;

namespace EbayAPILibrary
{
    public class ApiContextFactory
    {
        public static ApiContext GetApiContext(EbayConfig ebayConfig)
        {
            ApiContext Context = new ApiContext();
            //set Api Server Url
            //Context.SoapApiServerUrl = System.Configuration.ConfigurationManager.AppSettings["Environment.ApiServerUrl"];
            //set Api Token to access eBay Api Server
            ApiCredential apiCredential = new ApiCredential();
            apiCredential.eBayToken = ebayConfig.Token;   //获取token码
            Context.ApiCredential = apiCredential;
            //set eBay Site target to US
            Context.Site = SiteCodeType.US;

            Context.ApiCredential.ApiAccount.Developer = ebayConfig.Developer;
            Context.ApiCredential.ApiAccount.Certificate = ebayConfig.Certificate;
            Context.ApiCredential.ApiAccount.Application = ebayConfig.Application;
            Context.RuName = ebayConfig.RuName;

            return Context;
        }
    }
}
