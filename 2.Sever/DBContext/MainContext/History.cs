//Add-Migration [--context MainContext]
//Remove-Migration 取消最近一次迁移
//Update-Database [迁移名称  迁移直到(包含)或回退直到(不回退指定的版本) 0表示一开始]
//Drop-Database 
//Get-Help about_EntityFrameworkCore
//get-help Add-Migration
//dotnet tool install --global dotnet-ef
//dotnet ef -h
//
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyDBContext.Main
{
    public enum HistoryType {
        Unknown,
        Login,
        SendDeviceCmd,
    }
    public class  History {
        public short Type;

        public string Data { get; set; }
        [NotMapped]
        public HistoryType _Type
        {
            get => Enum.IsDefined(typeof(HistoryType), Type)
                ? (HistoryType)Type
                : HistoryType.Unknown;
            set =>
               Type = (byte)value;
        }

    }
}
