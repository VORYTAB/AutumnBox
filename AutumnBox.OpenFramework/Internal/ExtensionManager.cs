﻿/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/26 19:35:44 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Internal.AccessCheck;
using AutumnBox.OpenFramework.Open.V1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AutumnBox.OpenFramework.Internal
{
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
    public static class ExtensionManager
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
    {
        private const string TAG = "ExtensionManager";
        private static ExtensionManagerInner inner;
        /// <summary>
        /// 拓展文件夹绝对路径
        /// </summary>
        public static string ExtensionsPath
        {
            get
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/AutumnBox/Extensions";
            }
        }
        static ExtensionManager()
        {
            inner = new ExtensionManagerInner();
        }
        /// <summary>
        /// 加载所有模块
        /// </summary>
        [Hide]
        public static void LoadAllExtension()
        {
            inner.Load();
        }
        /// <summary>
        /// 摧毁所有模块
        /// </summary>
        [Hide]
        public static void DestoryAllExtension()
        {
            inner.Extensions.ForEach((extRuntime) =>
            {
                extRuntime.Destory();
            });
        }
        /// <summary>
        /// 获取所有模块
        /// </summary>
        /// <param name="targetDeviceState"></param>
        /// <returns></returns>
        [Hide]
        public static ExtensionRuntime[] GetExtensions(DeviceState? targetDeviceState = null)
        {
            if (targetDeviceState != null)
            {
                return inner.Extensions.Where((extRuntime) =>
                {
                    return extRuntime.InnerExtension.RequiredDeviceState.HasFlag(targetDeviceState);
                }).ToArray();
            }
            else
            {
                return inner.Extensions.ToArray();
            }

        }
        private class ExtensionManagerInner : Context
        {
            public List<ExtensionRuntime> Extensions { get; private set; }
            public ExtensionManagerInner()
            {
                Extensions = new List<ExtensionRuntime>();
                if (Directory.Exists(ExtensionsPath) == false)
                {
                    Directory.CreateDirectory(ExtensionsPath);
                }
            }
            public void Load()
            {
                List<Type> extensionTypes = new List<Type>();
                List<Assembly> dllAssemblies = new List<Assembly>();
                string[] dllFiles = Directory.GetFiles(ExtensionsPath, "*.dll");
                OpenApi.Log.Info(this, $"已获取{dllFiles.Length}个文件");
                foreach (string file in dllFiles)
                {
                    try
                    {
                        dllAssemblies.Add(Assembly.LoadFile(file));
                    }
                    catch (Exception ex)
                    {
                        OpenApi.Log.Info(this, "加载一个程序集时发生异常\n" + ex);
                    }
                }
                foreach (Assembly dll in dllAssemblies)
                {
                    try
                    {
                        var types = dll.GetExportedTypes()
                        .Where((extType) => { return extType.BaseType == typeof(AutumnBoxExtension); });
                        if (types.Count() == 0)
                        {
                            throw new Exception("No AutumnBox Extension");
                        }
                        extensionTypes.AddRange(types);
                    }
                    catch (Exception ex)
                    {
                        OpenApi.Log.Warn(this, $"获取程序集{dll.FullName}的拓展类型时发生错误\n" + ex);
                    }
                }
                var initArgs = new InitArgs();
                foreach (Type extensionType in extensionTypes)
                {
                    try
                    {
                        Extensions.Add(ExtensionRuntime.Create(extensionType, initArgs));
                    }
                    catch (Exception ex)
                    {
                        OpenApi.Log.Warn(this, $"加载{extensionType.Name}时发生错误\n" + ex);
                    }
                }
            }
            ~ExtensionManagerInner()
            {
                DestoryArgs args = new DestoryArgs();
                Extensions.ForEach((ext) =>
                {
                    try { ext.Destory(args); }
                    catch { }
                });
            }
        }
    }
}
