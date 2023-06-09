using FreeSql;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Common
{
	public static class FreeMySQL
	{
		public static void AddDataRepository(this IServiceCollection services, string connect)
		{
			_ = services.AddSingleton<IFreeSql<DataFlag>>(new FreeSqlBuilder()
				.UseConnectionString(FreeSql.DataType.MySql, connect)
				.UseAutoSyncStructure(true) //自动迁移实体的结构到数据库
			.Build<DataFlag>());
		}

	}
	public class DataFlag { }
}
