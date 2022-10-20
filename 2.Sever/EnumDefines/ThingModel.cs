//Add-Migration [--context MainContext]
//Remove-Migration 取消最近一次迁移
//Update-Database [迁移名称  迁移直到(包含)或回退直到(不回退指定的版本) 0表示一开始]
//Drop-Database 
//Get-Help about_EntityFrameworkCore
//get-help Add-Migration
//dotnet tool install --global dotnet-ef
//dotnet ef -h
//


namespace MyDBContext
{
    public class ThingModel { 
        public long Id;
        public string Name;
        public ThingModelValueType Type;
        public string Remark;
        /// <summary>
        /// 单位
        /// </summary>
        public string Unit;
        public float MinValue;
        public float MaxValue;
    }
}