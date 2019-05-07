using CrossCore.FreeSqls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrossCoreMSTest.FreeSqls
{
    /// <summary>
    /// FreeSql Sqlite 简单测试
    /// </summary>
    [TestClass]
    public class FreeSqliteTest
    {
        FreeSqlite sqlite;
        Person person;

        [ClassInitialize]
        public static void Init(TestContext context)
        {
            
        }

        public FreeSqliteTest()
        {
            sqlite = new FreeSqlite();
            person = new Person()
            {
                Name = "p1",
                Age = 20,
            };
        }

        [TestMethod]
        public void SqliteTest()
        {
            Assert.AreEqual(1, sqlite.Insert(person));

            Assert.AreEqual(person.Age, sqlite.Query(person.Name).Age);

            Assert.AreEqual(18, sqlite.Update(person.Name, 18));

            Assert.IsTrue(sqlite.Delete(person.Name));
        }

        [TestCleanup]
        public void Clean()
        {
            sqlite.Delete(person.Name);
        }
    }
}
