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

            Observers.Add(OperationContext.Current.SessionId, callback);
        }

        public string GetData(string value)
        {
            var callback = Observers.Get(OperationContext.Current.SessionId);
            callback.Notify(string.Format("You entered: {0}", value), DateTime.Now);
            callback.Notify(string.Format("现在队列有{0}个callback,id is {1}", Observers.Count(), OperationContext.Current.SessionId), DateTime.Now);

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
