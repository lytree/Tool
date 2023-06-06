using System;
using System.Threading.Tasks;
using Python.Included;
using Python.Runtime;
namespace Test.Pythonnet
{
    public class UnitTest1
    {
        [Fact]
        public async void Test1()
        {
            await Installer.SetupPython();
            PythonEngine.Initialize();
            PythonEngine.Initialize();
            dynamic sys = Py.Import("sys");
            Console.WriteLine("Python version: " + sys.version);
        }
        [Fact]
        public void GetPythonDistributionProperties()
        {
            Python.Deployment.Installer.InstallationSource src = new Python.Deployment.Installer.EmbeddedResourceInstallationSource()
            {
                Assembly = typeof(UnitTest1).Assembly,
                ResourceName = "python-3.7.8-embed-amd64.zip",
            };
            //Assert.Equal("python-3.7.8-embed-amd64.zip", src.GetPythonZipFileName());
            //Assert.Equal("python-3.7.8-embed-amd64", src.GetPythonDistributionName());
            //Assert.Equal("python37", src.GetPythonVersion());
            src = new Python.Deployment.Installer.DownloadInstallationSource()
            {
                DownloadUrl = @"https://www.python.org/ftp/python/3.7.8/python-3.7.8-embed-amd64.zip",
            };
            //Assert.Equal("python-3.7.3-embed-amd64.zip", src.GetPythonZipFileName());
            //Assert.Equal("python-3.7.3-embed-amd64", src.GetPythonDistributionName());
            //Assert.Equal("python37", src.GetPythonVersion());
        }
    }
}