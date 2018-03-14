﻿using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Internal;
using System;
using System.Diagnostics;
using System.Linq;

namespace AutumnBox.ConsoleTester
{
    class Program
    {
        public readonly static DeviceBasicInfo mi6 = new DeviceBasicInfo()
        {
            Serial = new DeviceSerialNumber("af0fe186"),
            State = DeviceState.Poweron,
        };
        public readonly static DeviceBasicInfo mi6net = new DeviceBasicInfo()
        {
            Serial = new DeviceSerialNumber("192.168.0.12:5555"),
            State = DeviceState.Poweron,
        };
        public readonly static DeviceBasicInfo mi4 = new DeviceBasicInfo()
        {
            Serial = new DeviceSerialNumber("9dd1b490"),
            State = DeviceState.Poweron,
        };
        unsafe static int Main(string[] cmdargs)
        {
            Console.WriteLine(new string[] {
                "a",
                "b"
            }.Contains("b"));
            Console.ReadKey();
            return 0;
        }
    }
}
