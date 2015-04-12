using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace ServiceInterfaceFramework.Winform
{
    public partial class ucWinServiceMgr : UserControl
    {
        public ucWinServiceMgr()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.DoubleBuffer, true);
        }

        public string ServiceName { get { return txtServiceName.Text.Trim(); } set { this.txtServiceName.Text = value; } }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            GetLastestStatus();
        }

        private void btnInstall_Click(object sender, EventArgs e)
        {
            InstallOrUnInstallService(ServiceName, true);
            GetLastestStatus();
        }

        private void btnUnInstall_Click(object sender, EventArgs e)
        {
            InstallOrUnInstallService(ServiceName, false);
            GetLastestStatus();
        }

        private void InstallOrUnInstallService(string serviceName, bool install)
        {
            var ofd = new OpenFileDialog();
            if (ofd.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }

            string exeFileName = ofd.FileName;

            if (install)
            {
                WinServiceControl.InstallmyService(null, exeFileName);
                if (WinServiceControl.Existed(serviceName))
                {
                    labMsg.Text = "服务【" + serviceName + "】安装成功！";
                    labStatus.Text = GetStaus();
                }
                else
                {
                    labMsg.Text = "服务【" + serviceName + "】安装失败，请检查日志！";
                }
            }
            else
            {
                WinServiceControl.UnInstallmyService(exeFileName);
                if (!WinServiceControl.Existed(serviceName))
                {
                    labMsg.Text = "服务【" + serviceName + "】卸载成功！";
                }
                else
                {
                    labMsg.Text = "服务【" + serviceName + "】卸载失败，请检查日志！";
                }
            }
        }

        private string GetStaus()
        {
            btnStart.Enabled = false;
            btnStop.Enabled = false;
            string staStr = "";
            var status = WinServiceControl.GetServiceStatus(ServiceName);
            switch (status)
            {
                case ServiceControllerStatus.ContinuePending:
                    staStr = "服务即将继续！";
                    break;
                case ServiceControllerStatus.PausePending:
                    staStr = "服务即将暂停！";
                    break;
                case ServiceControllerStatus.Paused:
                    staStr = "服务已暂停！";
                    btnStart.Enabled = true;
                    btnStop.Enabled = true;
                    break;
                case ServiceControllerStatus.Running:
                    staStr = "服务正在运行！";
                    btnStop.Enabled = true;
                    break;
                case ServiceControllerStatus.StartPending:
                    staStr = "服务正在启动！";
                    break;
                case ServiceControllerStatus.StopPending:
                    staStr = "服务正在停止！";
                    break;
                case ServiceControllerStatus.Stopped:
                    staStr = "服务未运行！";
                    btnStart.Enabled = true;
                    break;
                default:
                    staStr = "未知状态！";
                    break;
            }
            return staStr;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            WinServiceControl.Run(ServiceName);
            GetLastestStatus();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            WinServiceControl.Stop(ServiceName);
            GetLastestStatus();
        }

        private void ucWinServiceMgr_Load(object sender, EventArgs e)
        {
            GetLastestStatus();

            Task task = new Task(() =>
            {
                while (true)
                {
                    int num = 3;
                    while (num-- != 0)
                    {
                        labStatus.Text = string.Format("update status after {0} seconds...... ", num);
                        System.Threading.Thread.Sleep(1000);
                        labStatus.Text = "";
                    }

                    GetLastestStatus();

                    System.Threading.Thread.Sleep(5000);
                    labMsg.Text = "";
                }
            });
            task.Start();
            this.labStatus.Text = GetStaus();
        }

        private void GetLastestStatus()
        {
            if (WinServiceControl.Existed(ServiceName))
            {
                btnInstall.Enabled = false;
                btnUnInstall.Enabled = true;

                labStatus.Text = GetStaus();
            }
            else
            {
                btnInstall.Enabled = true;
                btnUnInstall.Enabled = false;

                if (this.Visible)
                {
                    this.labMsg.Text = string.Format("{0}不存在", ServiceName);
                    this.labMsg.ForeColor = Color.Red;
                    //MessageBox.Show();
                }
            }
        }
    }
}
