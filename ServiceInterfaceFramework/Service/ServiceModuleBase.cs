using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using ServiceInterfaceFramework.Common;

namespace ServiceInterfaceFramework.Service
{
    public abstract class ServiceModuleBase : IService
    {
        protected abstract void DoWork();

        private Timer timer;
        private bool _stopFlag = false;

        public void Start()
        {
            _stopFlag = false;

            if (DoWorkAtStart)
            {
                Work();

                if (RunOnlyOnce)
                {
                    return;
                }
            }

            if (timer != null)
            {
                return;
            }

            timer = new Timer();
            timer.Enabled = true;
            timer.Interval = Interval;
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            timer.Enabled = false;

            Work();

            if (RunOnlyOnce)
            {
                timer.Enabled = false;
                timer.Dispose();
                timer = null;
                return;
            }

            if (timer != null && _stopFlag)  //停止定时器
            {
                timer.Enabled = false;
                timer.Dispose();
                timer = null;
            }
            else
            {
                timer.Enabled = true;
            }
        }

        private void Work()
        {
            try
            {
                DoWork();
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex);
            }
        }

        public void Stop()
        {
            _stopFlag = true;
        }

        public int Interval
        {
            get;
            set;
        }

        public bool DoWorkAtStart
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public bool RunOnlyOnce
        {
            get;
            set;
        }
    }
}
