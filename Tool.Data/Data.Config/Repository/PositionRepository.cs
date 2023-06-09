using Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
	public class PositionRepository
	{
		private readonly IFreeSql<DataFlag> _freeSql;

		public PositionRepository(IFreeSql<DataFlag> freeSql) { _freeSql = freeSql; }
	}
}
