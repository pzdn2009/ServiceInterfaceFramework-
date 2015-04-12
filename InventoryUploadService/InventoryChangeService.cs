using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace InventoryUploadService
{

    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class InventoryChangeService : IInventoryChange
    {
        private static List<IMessageCallback> callbackList = new List<IMessageCallback>();
        public InventoryChangeService()
        {
            var callback = OperationContext.Current.GetCallbackChannel<IMessageCallback>();

            if (!callbackList.Contains(callback))
                callbackList.Add(callback);

            Observers.Add(EServiceType.service1, callback);
        }

        public string GetData(string value)
        {
            var callback = OperationContext.Current.GetCallbackChannel<IMessageCallback>();
            callback.Notify(string.Format("You entered: {0}", value), DateTime.Now);

            return string.Format("You entered: {0}", value);
        }

        private void Broadcast()
        {
            callbackList.ForEach(
               delegate(IMessageCallback callback)
               { callback.Notify("my broadcast", DateTime.Now); });

        }
    }
}
