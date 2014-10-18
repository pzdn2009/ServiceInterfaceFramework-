using NUnit.Framework;
using ServiceInterfaceFramework.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Should;
using FluentAssertions;

namespace ServiceInterfaceFramework.Tests.Common
{
    [TestFixture]
    public class XmlHelperTests
    {
        private XmlTestEntity entity;
        private string globalXml;

        [SetUp]
        public void Setup()
        {
            entity = new XmlTestEntity()
            {
                Age = 20,
                Name = "pzdn",
                Items = new List<EntityItem>()
            };
            entity.Items.Add(new EntityItem()
            {
                Id = "1",
                GoodsName = "g1"
            });
            entity.Items.Add(new EntityItem()
            {
                Id = "2",
                GoodsName = "g2"
            });
            entity.Items.Add(new EntityItem()
            {
                Id = "3",
                GoodsName = "g3"
            });

            globalXml = "<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n" +
"<XmlTestEntity xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\r\n" +
@"  <Name>pzdn</Name>
  <Age>20</Age>
  <Items>
    <EntityItem>
      <Id>1</Id>
      <GoodsName>g1</GoodsName>
    </EntityItem>
    <EntityItem>
      <Id>2</Id>
      <GoodsName>g2</GoodsName>
    </EntityItem>
    <EntityItem>
      <Id>3</Id>
      <GoodsName>g3</GoodsName>
    </EntityItem>
  </Items>
</XmlTestEntity>";
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void XmlSerialize_Generic_IsNotNull_Test()
        {
            var xml = XmlHelper.XmlSerialize<XmlTestEntity>(null);
            xml.Should().Be(globalXml);
        }

        [Test]
        public void XmlSerialize_Generic_Test()
        {
            var xml = XmlHelper.XmlSerialize<XmlTestEntity>(entity);
            xml.Should().Be(globalXml);
        }

        [Test]
        public void XmlSerialize_Object_Test()
        {
            object obj = entity;
            var xml = XmlHelper.XmlSerialize(obj);
            xml.Should().Be(globalXml);
        }

        [Test]
        public void SerializeXml_Test()
        {
            var xml = XmlHelper.SerializeXml<XmlTestEntity>(entity);
            xml.Should().Be("<?xml version=\"1.0\"?>\r\n<XmlTestEntity xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\r\n  <Name>pzdn</Name>\r\n  <Age>20</Age>\r\n  <Items>\r\n    <EntityItem>\r\n      <Id>1</Id>\r\n      <GoodsName>g1</GoodsName>\r\n    </EntityItem>\r\n    <EntityItem>\r\n      <Id>2</Id>\r\n      <GoodsName>g2</GoodsName>\r\n    </EntityItem>\r\n    <EntityItem>\r\n      <Id>3</Id>\r\n      <GoodsName>g3</GoodsName>\r\n    </EntityItem>\r\n  </Items>\r\n</XmlTestEntity>");
        }

        [Test]
        public void XmlDeserialize_Test()
        {
            string xml = "<?xml version=\"1.0\" encoding=\"utf-16\"?>" +
"<XmlTestEntity xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">"+
  @"<Name>pzdn</Name>
  <Age>20</Age>
  <Items>
    <EntityItem>
      <Id>1</Id>
      <GoodsName>g1</GoodsName>
    </EntityItem>
    <EntityItem>
      <Id>2</Id>
      <GoodsName>g2</GoodsName>
    </EntityItem>
    <EntityItem>
      <Id>3</Id>
      <GoodsName>g3</GoodsName>
    </EntityItem>
  </Items>
</XmlTestEntity>";
            var entity = XmlHelper.XmlDeserialize<XmlTestEntity>(xml);
            entity.Name.ShouldEqual("pzdn");
        }
    }

    public class XmlTestEntity
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public List<EntityItem> Items { get; set; }
    }

    public class EntityItem
    {
        public string Id { get; set; }
        public string GoodsName { get; set; }
    }
}
