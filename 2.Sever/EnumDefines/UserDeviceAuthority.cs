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
        Read_BaseInfo=1<<0,
        /// <summary>
        /// 获取设备维修历史记录
        /// </summary>
        Read_Repair =1<<1,
        /// <summary>
        /// 获取设备历史命令
        /// </summary>
        Read_Cmd = 1 << 2,
        /// <summary>
        /// 获取设备定时任务
        /// </summary>
        Read_TimeSetting = 1 << 3,
        /// <summary>
        /// 获取设备当前状态信息
        /// </summary>
        Read_Status = 1 << 4,
        /// <summary>
        /// 获取设备历史信息
        /// </summary>
        Read_Data = 1 << 5,
        /// <summary>
        /// 删除设备历史信息
        /// </summary>
        Write_DeletData = 1 << 6,
        /// <summary>
        /// 删除设备
        /// </summary>
        Write_DeletDevice = 1 << 7,
        /// <summary>
        /// 修改设备基础信息
        /// </summary>
        Write_BaseInfo = 1 << 8,
        /// <summary>
        /// 修改设备维修信息
        /// </summary>
        Write_Repair = 1 << 9,
        /// <summary>
        /// 修改设备类型
        /// </summary>
        Write_Type = 1 << 10,
        /// <summary>
        /// 向设备发送命令
        /// </summary>
        Control_Cmd = 1 << 11,
        /// <summary>
        /// 修改设备定时任务
        /// </summary>
        Control_TimeSetting = 1 << 12,
        /// <summary>
        /// 转授
        /// </summary>
        Delegate=1<<31,
    }
}