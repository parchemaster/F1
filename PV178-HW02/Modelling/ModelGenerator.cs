using System.Diagnostics;
using System.Runtime.InteropServices;

namespace PV178_HW02.Modelling
{ 
    internal class ModelGenerator
    {
        public static void RunProcessMining(string fileName)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                RunProcessMiningIOS(fileName);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                RunProcessMiningWindows(fileName);
            }
            else
            {
                RunProcessMiningLinux(fileName);
            }
        }

        private static void RunProcessMiningLinux(string fileName)
        {
            string myScriptPath = Path.Combine(Directory.GetCurrentDirectory(), "Modelling", "process_miner.py"); //update the path if necessary
            string myLogPath = Path.Combine(Directory.GetCurrentDirectory(), fileName); //update the path if necessary
            string pathToPMResult = myLogPath.Replace(".csv", "");

            string strCmdText = $"{myScriptPath} {myLogPath} {pathToPMResult} id activity";

            Process.Start("/bin/python", strCmdText).WaitForExit();
        }

        private static void RunProcessMiningIOS(string fileName)
        {
            string myScriptPath = Path.Combine("/Users/Ivan/Downloads/PV178-HW02/PV178-HW02/Modelling", "process_miner.py"); //update the path if necessary
            string myLogPath = Path.Combine(Directory.GetCurrentDirectory(), fileName); //update the path if necessary
            string pathToPMResult = myLogPath.Replace(".csv", "");

            string strCmdText = $"{myScriptPath} {myLogPath} {pathToPMResult} id activity";

            Process.Start("/usr/bin/python3", strCmdText).WaitForExit();
        }

        private static void RunProcessMiningWindows(string fileName)
        {
            string myScriptPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "Modelling", "process_miner.py"); //update the path if necessary
            string myLogPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, fileName); //update the path if necessary
            string pathToPMResult = myLogPath.Replace(".csv", "");

            string strCmdText = $"/C python {myScriptPath} {myLogPath} {pathToPMResult} id activity";

            Process.Start("CMD.exe", strCmdText).WaitForExit();
        }
        
        public static void run_cmd()
        {
            
            var psi = new ProcessStartInfo();
            psi.FileName = @"/usr/local/bin/python3.9";

            // 2) Provide script and arguments
            var script = Path.Combine(Directory.GetCurrentDirectory()
                .Replace("bin/Debug/net6.0", "Modelling"), "process_miner.py"); // didn't find perfect function for Mac OS
            var arg1 = "id";
            var arg2 = "activity";
            var arg3 = Path.Combine(Directory.GetCurrentDirectory()
                .Replace("bin/Debug/net6.0", "Export"), "fif1_file");

            psi.Arguments = $"\"{script}\" \"{arg1}\" \"{arg2}\" \"{arg3}\"";

            // 3) Process configuration
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;
            
            using(var process = Process.Start(psi))
            {
                process.StandardError.ReadToEnd();
            }
        }
    }
}
