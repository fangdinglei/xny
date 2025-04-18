﻿using BaseDefines;
using Grpc.Core;
using GrpcMain.Attributes;
using Microsoft.EntityFrameworkCore;
using MyDBContext.Main;
using System.Management;

namespace GrpcMain.System
{
    public class SystemServiceImp : SystemService.SystemServiceBase
    {
        [MyGrpcMethod(nameof(UserAuthorityEnum.SystemInfoRead))]
        public override async Task<Response_GetSystemBaseInfo> GetSystemBaseInfo(Request_GetSystemBaseInfo request, ServerCallContext context)
        {
            Response_GetSystemBaseInfo res = new Response_GetSystemBaseInfo();
            string machineName = Environment.MachineName;
            string osVersionName = GetOsVersion(Environment.OSVersion.Version);
            string servicePack = Environment.OSVersion.ServicePack;
            osVersionName = osVersionName + " " + servicePack;
            string userName = Environment.UserName;
            string domainName = Environment.UserDomainName;
            var tickCount = Environment.TickCount;
            var systemPageSize = Environment.SystemPageSize;
            string systemDir = Environment.SystemDirectory;
            string stackTrace = Environment.StackTrace;
            var processorCounter = Environment.ProcessorCount;
            string platform = Environment.OSVersion.Platform.ToString();
            string newLine = Environment.NewLine;

            string currDir = Environment.CurrentDirectory;
            string cmdLine = Environment.CommandLine;
            string[] drives = Environment.GetLogicalDrives();

            res.SeverId = request.SeverId;
            res.ProcesserCount = processorCounter;
            res.MachineName = machineName;
            res.OsVersionName = osVersionName;
            res.PageSize = systemPageSize;
            res.PhisicalMemory = GetPhisicalMemory();
            res.SystemTime = tickCount;
            res.Is64System = Environment.Is64BitOperatingSystem;
            res.Is64Process = Environment.Is64BitProcess;
            return await Task.Run(() =>
            {
                return res;
            });
        }

        [MyGrpcMethod(nameof(UserAuthorityEnum.SystemInfoRead))]
        public override async Task<Response_GetStatics> GetStatics(Request_GetStatics request, ServerCallContext context)
        {
            List<UserStatics> ls = new List<UserStatics>();
            using (MainContext ct = new MainContext())
            {
                var topusers = await ct.Users.Where(it => it.TreeDeep==1)
                    .AsNoTracking().ToListAsync();
                foreach (var item in topusers)
                {
                    var us = new UserStatics();
                    var treeId= item.UserTreeId;
                    us.TopUserId= item.Id;
                    us.TreeId = treeId;
                    us.TotalDevice = await ct.Devices.Where(it => it.UserTreeId == treeId).CountAsync();
                    us.TotalDeviceType = await ct.Device_Types.Where(it => it.UserTreeId == treeId).CountAsync();
                    us.TotalDataPoint = await ct.Devices.Where(it => it.UserTreeId == treeId).Select(it => it.Id).Join(
                            ct.Device_DataPoints, it => it, it => it.DeviceId, (a, b) => b).CountAsync();
                    us.TotalColdData=await ct.Device_DataPoint_Colds.Where(it=>it.TreeId== treeId).CountAsync();
                    us.SubUserCount = await ct.Users.Where(it => it.UserTreeId == treeId).CountAsync() - 1;
                    ls.Add(us);
                }
            }
            var res = new Response_GetStatics() { };
            res.Data.AddRange(ls);
            return res;
        }


        private string GetOsVersion(Version ver)
        {
            string strClient = "";
            if (ver.Major == 5 && ver.Minor == 1)
            {
                strClient = "Win XP";
            }
            else if (ver.Major == 6 && ver.Minor == 0)
            {
                strClient = "Win Vista";
            }
            else if (ver.Major == 6 && ver.Minor == 1)
            {
                strClient = "Win 7";
            }
            else if (ver.Major == 10)
            {
                strClient = "Win 10";
            }
            else if (ver.Major == 11)
            {
                strClient = "Win 11";
            }
            else if (ver.Major == 5 && ver.Minor == 0)
            {
                strClient = "Win 2000";
            }
            else
            {
                strClient = "未知";
            }
            return strClient;
        }

        /// <summary>
        /// 获取系统内存大小
        /// </summary>
        /// <returns>内存大小（单位M）</returns>
        private int GetPhisicalMemory()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher();   //用于查询一些如系统信息的管理对象
            searcher.Query = new SelectQuery("Win32_PhysicalMemory ", "", new string[] { "Capacity" });//设置查询条件
            ManagementObjectCollection collection = searcher.Get();   //获取内存容量
            ManagementObjectCollection.ManagementObjectEnumerator em = collection.GetEnumerator();

            long capacity = 0;
            while (em.MoveNext())
            {
                ManagementBaseObject baseObj = em.Current;
                if (baseObj.Properties["Capacity"].Value != null)
                {
                    try
                    {
                        capacity += long.Parse(baseObj.Properties["Capacity"].Value.ToString());
                    }
                    catch
                    {
                        return 0;
                    }
                }
            }
            return (int)(capacity / 1024 / 1024);
        }


        private int GetPhicnalInfo()
        {
            //ManagementClass osClass = new ManagementClass("Win32_Processor");//后面几种可以试一下，会有意外的收获//Win32_PhysicalMemory/Win32_Keyboard/Win32_ComputerSystem/Win32_OperatingSystem
            //foreach (ManagementObject obj in osClass.GetInstances())
            //{
            //    PropertyDataCollection pdc = obj.Properties;
            //    foreach (PropertyData pd in pdc)
            //    {
            //        this.rtbOs.AppendText(string.Format("{0}： {1}{2}", pd.Name, pd.Value, "\r\n"));
            //    }
            //}
            return 0;
        }

    }
}
