﻿/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/10 20:07:46 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.CoreModules.Attribute;
using AutumnBox.CoreModules.Lib;
using AutumnBox.OpenFramework.Extension;

namespace AutumnBox.CoreModules.Extensions.Poweron
{
    [ExtName("激活黑洞", "en-us:Set Blackhole as DPM")]
    [ExtIcon("Icons.blackhole.png")]
    [ExtAppProperty(PKGNAME)]
    [DpmReceiver(RECEIVER_NAME)]
    internal class EBlackHole : EDpmSetterBase
    {
        public const string PKGNAME = "com.hld.apurikakusu";
        public const string RECEIVER_NAME = "com.hld.apurikakusu/.receiver.DPMReceiver";
    }
}
