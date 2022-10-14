using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OpenAPI2MD.CommunityToolkit.Command
{
    public class CmdRunner
    {
        // 通过命令行获取help显示信息
        public string PrintDoc(string command)
        {
            try
            {
                var info = new ProcessStartInfo()
                {
                    CreateNoWindow = true,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    FileName = "cmd.exe"
                };
                var p = new Process
                {
                    StartInfo = info,
                };
                p.Start();
                p.StandardInput.WriteLine(command);
                p.StandardInput.WriteLine("exit");
                var o = p.StandardOutput.ReadToEnd();
                p.WaitForExit();  //等待程序执行完退出进程
                p.Close();
                return Regex.Match(o, @"(?<txt>.+(?=\>cd))").Value;


            }
            catch (Exception e)
            {
                throw;
            }
        }

    }
}
