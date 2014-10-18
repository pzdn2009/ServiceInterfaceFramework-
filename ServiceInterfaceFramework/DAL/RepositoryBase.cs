using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceInterfaceFramework.Model;
using System.Data.Linq;

namespace ServiceInterfaceFramework.DAL
{
    public abstract class WmsRepositoryBase<T, TKey> : IRepository<T, TKey> where T : class
    {
        protected MainDataContext wmsDbContext;

        public WmsRepositoryBase()
        {

        }

        public WmsRepositoryBase(MainDataContext db)
        {
            this.wmsDbContext = db;
        }

        public Table<T> Table
        {
            get { return this.wmsDbContext.GetTable<T>(); }
        }

        public virtual void Add(T entity)
        {
            Table.InsertOnSubmit(entity);
            wmsDbContext.SubmitChanges();
        }

        public virtual void Update(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual void Delete(T entity)
        {
            Table.DeleteOnSubmit(entity);
            wmsDbContext.SubmitChanges();
        }

        public virtual void Delete(TKey key)
        {
            throw new NotImplementedException();
        }

        public int ExecuteCommand(string sql)
        {
            int ret = wmsDbContext.ExecuteCommand(sql);
            return ret;
        }

        public object ExecuteView(string sql)
        {
            //wmsDbContext.ExecuteQuery(sql, null);
            throw new NotImplementedException();
        }

        public IQueryable<T> GetAll()
        {
            return Table;
        }

        public virtual T GetSingle(TKey key)
        {
            throw new NotImplementedException();
        }

        private string connectionString;
        public string ConnectionString
        {
            get { return connectionString; }
            set
            {
                connectionString = value;
                this.wmsDbContext = new MainDataContext(connectionString);
            }
        }


        
    }
}
