using CrossCore.FreeSqls;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CrossCorexUnit.FreeSqls
{
    public class FreeSqliteTest
    {
        [Theory]
        [InlineData("p1", 10)]
        [InlineData("p2", 20)]
        public void SqliteTest(string name, int age)
        {
            FreeSqlite sqlite = new FreeSqlite();
            Person person = new Person()
            {
                Name = name,
                Age = age
            };

            Assert.Equal(1, sqlite.Insert(person));

            Assert.Equal(person.Age, sqlite.Query(person.Name).Age);

            Assert.Equal(18, sqlite.Update(person.Name, 18));

            Assert.True(sqlite.Delete(person.Name));

            Assert.False(sqlite.Delete("我是谁"));
        }
    }
}
