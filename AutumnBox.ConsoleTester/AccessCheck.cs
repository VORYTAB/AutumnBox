﻿/*************************************************
** auth： zsh2401@163.com
** date:  2018/3/14 12:21:46 (UTC +8:00)
** desc： ...
*************************************************/
using System;

namespace System
{
    public static class ObjectExtension
    {
        public static void PrintOnConsole(this object obj)
        {
            Console.WriteLine(obj);
        }
    }
}

namespace AutumnBox.ConsoleTester
{
    class Test
    {

        public void TestX()
        {
            Console.WriteLine("TestX");
        }
    }
}
