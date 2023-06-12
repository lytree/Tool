using Config.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Config.AppConfig.Repository
{
	public class ConfigRepository
	{
		private readonly IFreeSql<AppConfigFlag> _freeSql;
		public ConfigRepository(IFreeSql<AppConfigFlag> freeSql)
		{
			_freeSql = freeSql;
		}


	}
}
