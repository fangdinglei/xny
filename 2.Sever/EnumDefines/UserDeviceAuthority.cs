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
        Read_BaseInfo=1,
        read_repair ,
        read_cmd,
        read_timesetting,
        read_status,
        read_data,
        write_data,
        write_device,
        write_baseinfo,
        write_repair,
        write_type,
        control_cmd,
        Control_TimeSetting,
        /// <summary>
        /// 转授
        /// </summary>
        Delegate=1<<31,

    }
}