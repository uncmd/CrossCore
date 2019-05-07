using CrossCore.FreeSqls;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrossCoreNUnit.FreeSqls
{
    public class FreeSqliteTest
    {
        FreeSqlite sqlite;
        Person person;

        [SetUp]
        public void Init()
        {
            sqlite = new FreeSqlite();
            person = new Person()
            {
                Name = "p1",
                Age = 20,
            };
        }

        [Test]
        public void SqliteTest()
        {
            Assert.AreEqual(1, sqlite.Insert(person));

            Assert.AreEqual(person.Age, sqlite.Query(person.Name).Age);

            Assert.AreEqual(18, sqlite.Update(person.Name, 18));

            Assert.IsTrue(sqlite.Delete(person.Name));
        }

        [TearDown]
        public void Clean()
        {
            sqlite.Delete(person.Name);
        }
    }
}
