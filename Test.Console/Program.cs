// See https://aka.ms/new-console-template for more information
using Python.Runtime;
using System.IO;
using System.Text;
using System.Text.Json.Nodes;
using System.Xml.Linq;

Console.WriteLine("Hello, World!");
// ================================================
// This example demonstrates how to download a Python distribution (v2.7.9) and install it locally 
// ================================================

// set the download source
Python.Deployment.Installer.Source = new Python.Deployment.Installer.DownloadInstallationSource()
{
    DownloadUrl = @"https://www.python.org/ftp/python/3.7.8/python-3.7.8-embed-amd64.zip",
};

// install in local directory. if you don't set it will install in local app data of your user account
Python.Deployment.Installer.InstallPath = Path.GetFullPath(".");

// see what the installer is doing
Python.Deployment.Installer.LogMessage += Console.WriteLine;

// install from the given source
await Python.Deployment.Installer.SetupPython();
// install from the given source
// install from the given source
await Python.Deployment.Installer.SetupPython(force: true);
// install pip3 for package installation
await Python.Deployment.Installer.TryInstallPip();
await Python.Deployment.Installer.PipInstallModule("numpy");
Runtime.PythonDLL = "python37.dll";

// ok, now use pythonnet from that installation
PythonEngine.Initialize();

// call Python's sys.version to prove we are executing the right version
dynamic sys = Py.Import("sys");
Console.WriteLine("### Python version:\n\t" + sys.version);

// call os.getcwd() to prove we are executing the locally installed embedded python distribution
dynamic os = Py.Import("os");
Console.WriteLine("### Current working directory:\n\t" + os.getcwd());
Console.WriteLine("### PythonPath:\n\t" + PythonEngine.PythonPath);
//调用无参无返回值方法
sys.path.append(os.getcwd());
//python对象应声明为dynamic类型
dynamic fan = Py.Import("fan_feature_modify");
Console.WriteLine("### fan_feature_modify:\n\t" + fan);
dynamic json = Py.Import("json");
Console.WriteLine("### json:\n\t" + json);

var str= File.ReadAllText(@"F:\github\Tool\Tool\Test.Console\files\423210224406829422072417441383507.json", Encoding.UTF8);
var jsonObject = json.loads(str);
var text=fan.deviation_factor(jsonObject["args"][0],jsonObject["args"][1],jsonObject["args"][2],jsonObject["args"][3]);
Console.WriteLine(text);

