using FreeSql.DatabaseModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using FreeSql.DataAnnotations;

namespace Data.Entity;


[Table(Name = "c_machine_position", DisableSyncStructure = true)]
public partial class MachinePosition
{

	[Column(Name = "machine_id", StringLength = 20, IsPrimary = true, IsNullable = false)]
	public string MachineId { get; set; }

	[Column(Name = "position_id", IsPrimary = true)]
	public ushort PositionId { get; set; }

	[Column(Name = "position_type", IsPrimary = true)]
	public byte PositionType { get; set; }

	[Column(Name = "channel_id")]
	public ushort? ChannelId { get; set; }

	[Column(Name = "channel_name", StringLength = 100)]
	public string ChannelName { get; set; }

	[Column(Name = "channel_type")]
	public byte? ChannelType { get; set; }

	[Column(Name = "dgm_id")]
	public int? DgmId { get; set; }

	[Column(Name = "isalarm", DbType = "tinyint(1)")]
	public sbyte? Isalarm { get; set; }

	/// <summary>
	/// 测点可扩充配置
	/// </summary>
	[Column(Name = "json_config", DbType = "json")]
	public string JsonConfig { get; set; }

	[Column(Name = "pos_xml_config", StringLength = -1)]
	public string XmlConfig { get; set; }

	[Column(Name = "position_name", StringLength = 50)]
	public string PositionName { get; set; }

	[Column(Name = "status", DbType = "tinyint(1)")]
	public sbyte? Status { get; set; }

}
