using NUnit.Framework;
using ServiceInterfaceFramework.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;

namespace ServiceInterfaceFramework.Tests.Cache
{
    [TestFixture]
    public class DataTableUtilsTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CopyToDataTable_Not_Null()
        {
            DataTableUtils.CopyToDataTable<string>(null);
        }

        [Test]
        public void CopyToDataTable_General_Test()
        {
            var dt = DataTableUtils.CopyToDataTable<UserEntity>(GetUserEntityList());
            dt.Rows.Count.Should().Be(3);
            dt.Rows[0][0].ToString().Should().Be("pzdn");
        }

        [Test]
        public void CopyToDataTable_Nullable_Test()
        {
            var dt = DataTableUtils.CopyToDataTable<UserEntityWithNullable>(GetUserEntityWithNullableList());
            dt.Rows.Count.Should().Be(3);
            dt.Rows[0][0].ToString().Should().Be("pzdn");
            dt.Rows[0][2].Should().Be(DBNull.Value);
        }

        private List<UserEntity> GetUserEntityList()
        {
            List<UserEntity> list = new List<UserEntity>();
            var entity = new UserEntity()
            {
                Age = 25,
                Name = "pzdn"
            };
            list.Add(entity);

            entity = new UserEntity()
            {
                Age = 25,
                Name = "cdn"
            };
            list.Add(entity);

            entity = new UserEntity()
            {
                Age = 26,
                Name = "other"
            };
            list.Add(entity);
            return list;
        }

        private List<UserEntityWithNullable> GetUserEntityWithNullableList()
        {
            List<UserEntityWithNullable> list = new List<UserEntityWithNullable>();
            var entity = new UserEntityWithNullable()
            {
                Age = 25,
                MarryTime = null,
                Money = null,
                Name = "pzdn"
            };
            list.Add(entity);

            entity = new UserEntityWithNullable()
            {
                Age = 25,
                MarryTime = null,
                Money = 200000,
                Name = "cdn"
            };
            list.Add(entity);

            entity = new UserEntityWithNullable()
            {
                Age = 26,
                MarryTime = DateTime.Now,
                Money = 100000,
                Name = "other"
            };
            list.Add(entity);
            return list;
        }
    }

    public class UserEntity
    {
        public string Name { get; set; }
        public int Age { get; set; }

    }

    public class UserEntityWithNullable
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public double? Money { get; set; }
        public DateTime? MarryTime { get; set; }
    }
}
