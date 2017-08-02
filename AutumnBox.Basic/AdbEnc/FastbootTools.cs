﻿using AutumnBox.Basic.Devices;
using AutumnBox.Basic.Other;
using System.Collections.Generic;

namespace AutumnBox.Basic.AdbEnc
{
    internal class FastbootTools:Cmd,ITools
    {
        public FastbootTools():base()
        {

        }
        public OutputData Execute(string command)
        {
            return base.ExecuteCommand(Paths.FASTBOOT_TOOLS + " " + command);
        }
        public DevicesHashtable GetDevices()
        {
            List<string> x = Execute(" devices").output;
            DevicesHashtable hs = new DevicesHashtable();
            for (int i = 0; i < x.Count - 1; i++)
            {
                try
                {
                    hs.Add(x[i].Split('\t')[0], x[i].Split('\t')[1]);
                }
                catch { }
            }
            return hs;
        }
    }
}