using Microsoft.Extensions.Logging;

namespace CrossCore.FreeSqls
{
    public class FreeSqlite
    {
        IFreeSql freeSql;

        public FreeSqlite()
        {
            freeSql = new FreeSql.FreeSqlBuilder()
                .UseConnectionString(FreeSql.DataType.Sqlite, @"Data Source=|DataDirectory|\document.db;Pooling=true;Max Pool Size=10")
                .UseLogger(new LoggerFactory().CreateLogger("FreeSql"))
                .UseAutoSyncStructure(true) //自动迁移实体的结构到数据库
                .Build();
            freeSql.CodeFirst.ConfigEntity<Person>(p =>
            {
                p.Property(x => x.Name).IsPrimary(true);
            });
        }

        public int Insert(Person person)
        {
            return freeSql.Insert(person).ExecuteAffrows();
        }

        public Person Query(string name)
        {
            return freeSql.Select<Person>().Where(p => p.Name == name).First();
        }

        public int Update(string name, int age)
        {
            freeSql.Update<Person>(name).Set(p => p.Age, age).ExecuteAffrows();
            return Query(name).Age;
        }

        public bool Delete(string name)
        {
            var row = freeSql.Delete<Person>().Where(p => p.Name == name).ExecuteAffrows();
            var result = Query(name);
            if (result == null && row > 0)
                return true;
            return false;
        }
    }
}
