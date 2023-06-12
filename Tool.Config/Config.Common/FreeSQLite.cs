using Config.Entity;
using FreeSql;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Config.Common;

public static class FreeSQLite
{
	public static void AddDataRepository(this IServiceCollection services, string connect)
	{
		var fsql = new FreeSqlBuilder()
			.UseConnectionString(FreeSql.DataType.MySql, connect)
			.UseAutoSyncStructure(true) //自动迁移实体的结构到数据库
		.Build<AppConfigFlag>();
		fsql.Aop.AuditValue += (s, e) =>
		{
			if (e.Column.CsType == typeof(long) && s is BaseEntity entity
			 && e.Property.GetCustomAttribute<KeyAttribute>(false) != null)
			{
				if (e.Value?.ToString() != "0")
				{
					entity.UpdateTime = DateTime.Now;
				}
				else
				{
					entity.UpdateTime = DateTime.Now;
					entity.CreateTime = DateTime.Now;
				}
				entity.UpdateTime = DateTime.Now;

			}
		};
		services.AddSingleton(fsql);
	}
}

public class AppConfigFlag { }
