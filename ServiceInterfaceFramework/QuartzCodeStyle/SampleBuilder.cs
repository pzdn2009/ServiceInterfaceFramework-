using System;

namespace ServiceInterfaceFramework.QuartzCodeStyle
{
    //连贯接口的建造器
    public class SampleBuilder
    {
        //属性s

        private SampleKey key;
        private string description;
        private Type jobType = null;// typeof (NoOpJob);
        private bool durability;
        private bool shouldRecover;

        protected SampleBuilder()
        {
        }

        // 工厂方法，创建一个建造器
        public static SampleBuilder Create()
        {
            return new SampleBuilder();
        }

        public static SampleBuilder Create(Type jobType)
        {
            SampleBuilder b = new SampleBuilder();
            b.OfType(jobType);
            return b;
        }

        public static SampleBuilder Create<T>() where T : new()
        {
            SampleBuilder b = new SampleBuilder();
            b.OfType(typeof(T));
            return b;
        }

        //返回产品
        public ISampleProduct Build()
        {
            ISampleProduct job = new ISampleProduct();

            job.JobType = jobType;
            job.Description = description;
            if (key == null)
            {
                key = new SampleKey(Guid.NewGuid().ToString(), null);
            }
            job.Key = key;
            job.Durable = durability;
            job.RequestsRecovery = shouldRecover;

            return job;
        }

     
        public SampleBuilder WithIdentity(string name)
        {
            key = new SampleKey(name, null);
            return this;
        }

        public SampleBuilder WithIdentity(string name, string group)
        {
            key = new SampleKey(name, group);
            return this;
        }

        public SampleBuilder WithIdentity(SampleKey key)
        {
            this.key = key;
            return this;
        }

        public SampleBuilder WithDescription(string description)
        {
            this.description = description;
            return this;
        }

        public SampleBuilder OfType<T>()
        {
            return OfType(typeof(T));
        }

        public SampleBuilder OfType(Type type)
        {
            jobType = type;
            return this;
        }

        public SampleBuilder RequestRecovery()
        {
            this.shouldRecover = true;
            return this;
        }

        public SampleBuilder RequestRecovery(bool shouldRecover)
        {
            this.shouldRecover = shouldRecover;
            return this;
        }

        public SampleBuilder StoreDurably()
        {
            this.durability = true;
            return this;
        }

        public SampleBuilder StoreDurably(bool durability)
        {
            this.durability = durability;
            return this;
        }

        public SampleBuilder UsingJobData(string key, string value)
        {
            //jobDataMap.Put(key, value);
            return this;
        }
    }
}