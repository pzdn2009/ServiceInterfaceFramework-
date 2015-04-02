using InventoryUploadClient.InvertoryUP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventoryUploadClient
{
    [CallbackBehavior(UseSynchronizationContext = false)]
    public partial class FrmMain : Form, IInventoryChangeCallback
    {
        private SynchronizationContext uiSyncContext = null;
        private InventoryChangeClient inventoryChangeClient = null;
        public FrmMain()
        {
            InitializeComponent();
        }

        public void Notify(string mesage, DateTime dt)
        {
            SendOrPostCallback callback =
              delegate(object state)
              { this.lsbResult.Items.Add(string.Format(">> {0}:{1}", dt, mesage)); };

            uiSyncContext.Post(callback, null);

        }

        private void btnUpLoad_Click(object sender, EventArgs e)
        {
            var val = txtInventory.Text;
            inventoryChangeClient.GetData(val);  //use common client
            
            //create for every request
            //demo as WcfClientUtility.Using<>
            /*
            inventoryChangeClient.Using((client) =>
            {
                inventoryChangeClient = new InventoryChangeClient(new InstanceContext(this));
                client.GetData(val);
            });*/
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            // Capture the UI synchronization context
            uiSyncContext = SynchronizationContext.Current;

            inventoryChangeClient = new InventoryChangeClient(new InstanceContext(this));
            inventoryChangeClient.Open();
        }
    }
}
