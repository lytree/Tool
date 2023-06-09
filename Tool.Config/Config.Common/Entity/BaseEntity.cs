using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Config.Entity
{
	public class BaseEntity
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column("id")]
		public int Id { get; set; }
		/// <summary>
		/// 创新时间
		/// </summary>
		[Column("create_time")]
		public DateTime CreateTime { get; set; }
		/// <summary>
		/// 更新时间
		/// </summary>
		[Column("update_time")]
		public DateTime UpdateTime { get; set; }


	}
}
