using FreeSql.DatabaseModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FreeSql.DataAnnotations;

namespace Data.Entity;


[Table(Name = "c_navigate_tree", DisableSyncStructure = true)]
public partial class Tree
{

	[Column(Name = "t_id", IsPrimary = true)]
	public long TId { get; set; }

	[Column(Name = "t_depth", DbType = "int(4)")]
	public int? TDepth { get; set; }

	[Column(Name = "t_desc", StringLength = 256)]
	public string TDesc { get; set; }

	[Column(Name = "t_name", StringLength = 100)]
	public string TName { get; set; }

	[Column(Name = "t_path", StringLength = 100)]
	public string TPath { get; set; }

	[Column(Name = "t_pid")]
	public long? TPid { get; set; }

	[Column(Name = "t_root")]
	public long? TRoot { get; set; }

	[Column(Name = "xml_expand", StringLength = 256)]
	public string XmlExpand { get; set; }

}
