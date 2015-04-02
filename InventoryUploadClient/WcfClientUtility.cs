using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace InventoryUploadClient
{
    public static class WcfClientUtility
    {
        public static void Using<T>(this T client, Action<T> work) where T : ICommunicationObject
        {
            try
            {
                work(client);

                client.Close();
            }
            catch (CommunicationException e)
            {
                client.Abort();
            }
            catch (TimeoutException e)
            {
                client.Abort();
            }
            //catch (EndpointNotFoundException e)
            //{
            //    "无法连接服务器。";
            //}
            //catch (CommunicationObjectAbortedException e)
            //{
            //    "操作已被强制终止。";
            //}
            catch (Exception e)
            {
                client.Abort();
                throw;
            }
        }
    }
}
