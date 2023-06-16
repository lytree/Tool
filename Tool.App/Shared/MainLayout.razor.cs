using AntDesign;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Tool.App.Shared;

public partial class MainLayout
{
	[Inject] NavigationManager nvm { get; set; }
	readonly string[] rootSubmenuKeys = { "1", "2", " 3", "4", "5", "6" };

	string[] openKeys = Array.Empty<string>();
	private void GoHome()
	{
		nvm.NavigateTo("/");
	}
	void OnOpenChange(string[] openKeys)
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
