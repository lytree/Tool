using FreeSql.DatabaseModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FreeSql.DataAnnotations;

namespace Data.Entity;


[Table(Name = "c_machine", DisableSyncStructure = true)]
public partial class Machine
{

	[Column(Name = "machine_id", StringLength = 50, IsPrimary = true, IsNullable = false)]
	public string MachineId { get; set; }

	[Column(Name = "dbname", StringLength = 20)]
	public string Dbname { get; set; }

	/// <summary>
	/// 机组扩充配置
	/// </summary>
	[Column(Name = "json_config", DbType = "json")]
	public string JsonConfig { get; set; }

	[Column(Name = "m_me", StringLength = 100)]
	public string MName { get; set; }

	/// <summary>
	/// 机组类型，目前为1表示风电
	/// </summary>
	[Column(Name = "m_type", DbType = "int(4)")]
	public int? MType { get; set; }

	/// <summary>
	/// 机组器件连接xml
	/// 
	/// </summary>
	[Column(Name = "mach_model_xml", DbType = "mediumtext")]
	public string MachModelXml { get; set; }

	[Column(Name = "manufacturer", StringLength = 100)]
	public string Manufacturer { get; set; }

	[Column(Name = "model_number", StringLength = 50)]
	public string ModelNumber { get; set; }

	[Column(Name = "source", DbType = "int(4)")]
	public int? Source { get; set; }

	[Column(Name = "status", DbType = "int(4)")]
	public int? Status { get; set; }

	[Column(Name = "tree_id")]
	public long? TreeId { get; set; }

	[Column(Name = "xml_config", StringLength = -1)]
	public string XmlConfig { get; set; }

}
