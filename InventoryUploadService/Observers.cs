using ServiceInterfaceFramework.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace InventoryUploadService
{
    public enum EServiceType
    {
        service1,
        service2
    }
    public class Client
    {
        public EServiceType ServiceType { get; set; }
        public List<IMessageCallback> Callbacks { get; set; }
    }

    public class Observers
    {
        private static object lockObject = new object();
        private static List<Client> clientList;

        static Observers()
        {
            clientList = new List<Client>();
        }

        public static bool Exist(EServiceType serviceType, IMessageCallback callback)
        {
            lock (lockObject)
            {
                var client = clientList.Find(zw => zw.ServiceType == serviceType);
                if (client != null)
                {
                    LogHelper.Debug(serviceType.ToString() + ":" + "exist");
                    return client.Callbacks.Contains(callback);
                }
                return false;
            }
        }

        public static void Add(EServiceType serviceType, IMessageCallback callback)
        {
            lock (lockObject)
            {
                var tmp = clientList.Find(zw => zw.ServiceType == serviceType);
                if (tmp != null)
                {
                    LogHelper.Debug(serviceType.ToString() + ":" + "add");
                    tmp.Callbacks.Add(callback);
                }
                else
                {
                    clientList.Add(new Client() { ServiceType = serviceType, Callbacks = new List<IMessageCallback>() { callback } });
                }
            }
        }

        public static void Remove(EServiceType serviceType, IMessageCallback callback)
        {
            if (Exist(serviceType, callback))
            {
                lock (lockObject)
                {
                    LogHelper.Debug(serviceType.ToString() + ":" + "remove");
                    var tmp = clientList.Find(zw => zw.ServiceType == serviceType);
                    tmp.Callbacks.Remove(callback);
                }
            }
        }

        public static void BroadCastMessage(EServiceType serviceType, string message)
        {
            lock (lockObject)
            {
                var client = clientList.Find(zw => zw.ServiceType == serviceType);
                if (client == null) return;

                client.Callbacks.ForEach((callback) =>
                {
                    callback.Notify(message, DateTime.Now);
                });
            }
        }

        public static void CheckCallbackChannels()
        {
            lock (lockObject)
            {
                foreach (var eachClient in clientList)
                {
                    foreach (var callback in eachClient.Callbacks)
                    {
                        var callbackChannel = callback as ICommunicationObject;

                        if (callbackChannel.State == CommunicationState.Closed || callbackChannel.State == CommunicationState.Faulted)
                        {
                            LogHelper.Debug(eachClient.ServiceType.ToString() + ":" + "CheckCallbackChannels remove");
                            eachClient.Callbacks.Remove(callback);
                        }
                    }
                }
            }
        }
    }
}
