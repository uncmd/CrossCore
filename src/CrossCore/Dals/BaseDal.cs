using CrossCore.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrossCore.Dals
{
    public class BaseDal
    {
        IFreeSql freeSql;

        public BaseDal()
        {
            freeSql = new FreeSql.FreeSqlBuilder()
                .UseConnectionString(FreeSql.DataType.Sqlite, @"Data Source=|DataDirectory|\document.db;Pooling=true;Max Pool Size=10")
                .UseLogger(new LoggerFactory().CreateLogger("FreeSql"))
                .UseAutoSyncStructure(true) //自动迁移实体的结构到数据库
                .Build();
            freeSql.CodeFirst.ConfigEntity<User>(p =>
            {
                p.Property(x => x.Id).IsPrimary(true);
            });
            freeSql.CodeFirst.ConfigEntity<Role>(p =>
            {
                p.Property(x => x.Id).IsPrimary(true);
            });
        }
    }
}
