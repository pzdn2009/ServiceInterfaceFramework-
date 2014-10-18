using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceInterfaceFramework.Service
{
    /// <summary>
    /// 服务配置元素
    /// </summary>
    public class ServiceConfigElement
    {
        /// <summary>
        /// 时间间隔
        /// </summary>
        public int Interval { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public EUnitType Unit { get; set; }

        /// <summary>
        /// 是否立即启动
        /// </summary>
        public bool DoWorkAtStart { get; set; }

        /// <summary>
        /// 只运行一次
        /// </summary>
        public bool RunOnlyOnce { get; set; }

        /// <summary>
        /// 服务名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 服务类型
        /// </summary>
        public string Type { get; set; }

        public int ToMiniSecond()
        {
            switch (Unit)
            {
                case EUnitType.hour:
                    return Interval * 1000 * 3600;
                case EUnitType.minute:
                    return Interval * 1000 * 60;
                case EUnitType.second:
                    return Interval * 1000;
                default:
                    throw new Exception("不支持此种类型");
            }
        }
    }
}
