//Add-Migration [--context MainContext]
//Remove-Migration 取消最近一次迁移
//Update-Database [迁移名称  迁移直到(包含)或回退直到(不回退指定的版本) 0表示一开始]
//Drop-Database 
//Get-Help about_EntityFrameworkCore
//get-help Add-Migration
//dotnet tool install --global dotnet-ef
//dotnet ef -h
//


namespace MyDBContext.Main
{
    /// <summary>
    /// TODO重命名
    /// </summary>
    [Flags]
    public enum UserDeviceAuthority : int
    {
        No=0,
        Every=-1,
        /// <summary>
        /// 获取设备基础信息
        /// </summary>
        Read_BaseInfo=1,
        /// <summary>
        /// 获取设备维修历史记录
        /// </summary>
        read_repair ,
        /// <summary>
        /// 获取设备历史命令
        /// </summary>
        read_cmd,
        /// <summary>
        /// 获取设备定时任务
        /// </summary>
        read_timesetting,
        /// <summary>
        /// 获取设备当前状态信息
        /// </summary>
        read_status,
        /// <summary>
        /// 获取设备历史信息
        /// </summary>
        read_data,
        /// <summary>
        /// 删除设备历史信息
        /// </summary>
        write_data,
        /// <summary>
        /// 删除设备
        /// </summary>
        write_device,
        /// <summary>
        /// 修改设备基础信息
        /// </summary>
        write_baseinfo,
        /// <summary>
        /// 修改设备维修信息
        /// </summary>
        write_repair,
        /// <summary>
        /// 修改设备类型
        /// </summary>
        write_type,
        /// <summary>
        /// 向设备发送命令
        /// </summary>
        control_cmd,
        /// <summary>
        /// 修改设备定时任务
        /// </summary>
        Control_TimeSetting,
        /// <summary>
        /// 转授
        /// </summary>
        Delegate=1<<31,

    }
}