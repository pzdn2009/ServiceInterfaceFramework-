using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace InventoryUploadService
{
    public interface IMessageCallback
    {
        [OperationContract(IsOneWay = true)]
        void Notify(string mesage, DateTime dt);
    }
}
