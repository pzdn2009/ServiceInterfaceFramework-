using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceInterfaceFramework.DAL
{
    public interface IRepository<T, TKey>
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(TKey key);

        int ExecuteCommand(string sql);

        object ExecuteView(string sql);

        IQueryable<T> GetAll();
        T GetSingle(TKey key);

        /// <summary>
        /// 通过设置连接字符串，则可以选择不同的数据库
        /// </summary>
        string ConnectionString { get; set; }
    }
}
