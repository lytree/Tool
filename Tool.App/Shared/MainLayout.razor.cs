using AntDesign;
using Microsoft.AspNetCore.Components.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
namespace Tool.App.Shared;

public partial class MainLayout
{
	string[] rootSubmenuKeys = { "1", "2", " 3", "4", "5", "6" };

	string[] openKeys = {  };

	void onOpenChange(string[] openKeys)
	{
		var latestOpenKey = openKeys.FirstOrDefault(key => !this.openKeys.Contains(key));
		if (!rootSubmenuKeys.Contains(latestOpenKey))
		{
			this.openKeys = openKeys;
		}
		else
		{
			this.openKeys = !string.IsNullOrEmpty(latestOpenKey) ? new[] { latestOpenKey } : Array.Empty<string>();
		}
	}
}
