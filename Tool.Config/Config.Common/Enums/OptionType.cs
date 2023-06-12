using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Config.Enums
{
	/// <summary>
	/// 选项类型
	/// </summary>
	public enum OptionType
	{
		[Description("系统内置")]
		SYSTEM,
		[Description("用户自定义")]
		NO_SYSTEM
	}
}
