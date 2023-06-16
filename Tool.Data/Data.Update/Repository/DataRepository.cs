using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
	public class DataRepository
	{
		private readonly IFreeSql _freeSql;
		public DataRepository(IFreeSql freeSql)
		{
			_freeSql = freeSql;
		}

		public IEnumerable<Vib> GetAll(string machineId,)
		{
			_freeSql.Select<Vib>().



		}



	}
}
