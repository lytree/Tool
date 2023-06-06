using Python.Runtime;

namespace Tool.PythonEnv;

public class DownloadEnv
{
    public static async Task<bool> DownloadInit(string path, Action<string> LogMessage)
    {
        try
        {
            // This example demonstrates how to download a Python distribution (v2.7.9) and install it locally 
            // ================================================

            // set the download source
            Python.Deployment.Installer.Source = new Python.Deployment.Installer.DownloadInstallationSource()
            {
                DownloadUrl = @"https://www.python.org/ftp/python/3.7.8/python-3.7.8-embed-amd64.zip",
            };

            // install in local directory. if you don't set it will install in local app data of your user account
            Python.Deployment.Installer.InstallPath = Path.GetFullPath(path);

            // see what the installer is doing
            Python.Deployment.Installer.LogMessage += LogMessage;

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
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}
