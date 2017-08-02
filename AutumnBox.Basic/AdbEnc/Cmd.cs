﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.AdbEnc
{
    /// <summary>
    /// 封装Cmd命令行
    /// </summary>
    internal class Cmd
    {
        public const string NOT_FOUND = "NOT_FOUND";
        protected Process cmdProcess = new Process();
        public Cmd()
        {
            //初始化Cmd
            cmdProcess.StartInfo.FileName = "cmd.exe";
            cmdProcess.StartInfo.CreateNoWindow = true;         // 不创建新窗口    
            cmdProcess.StartInfo.UseShellExecute = false;       //不启用shell启动进程  
            cmdProcess.StartInfo.RedirectStandardInput = true;  // 重定向输入    
            cmdProcess.StartInfo.RedirectStandardOutput = true; // 重定向标准输出    
            cmdProcess.StartInfo.RedirectStandardError = true;  // 重定向错误输出  
        }
        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="command">完整命令</param>
        /// <returns>输出数据的数据结构</returns>
        public OutputData ExecuteCommand(string command)
        {
            List<string> output = new List<string>();
            string error = "";
            Debug.WriteLine("执行命令 -> " + command);
            cmdProcess.StartInfo.Arguments = "/c " + command;
            cmdProcess.Start();
            StreamReader x = cmdProcess.StandardOutput;
            string str = x.ReadToEnd();
            string[] lines = str.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            int i = 0;
            try
            {
                foreach (string line in lines)
                {
                    output.Add(line);
                    i++;
                }
                error = cmdProcess.StandardError.ReadToEnd();
            }catch { }
            try{cmdProcess.WaitForExit(); }catch { }
            cmdProcess.Close();
            OutputData o = new OutputData();
            o.output = output;
            o.error = error;
            return o;
        }
    }
}