﻿//Add-Migration [--context MainContext]
//Remove-Migration 取消最近一次迁移
//Update-Database [迁移名称  迁移直到(包含)或回退直到(不回退指定的版本) 0表示一开始]
//Drop-Database 
//Get-Help about_EntityFrameworkCore
//get-help Add-Migration
//dotnet tool install --global dotnet-ef
//dotnet ef -h
//
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyDBContext.Main
{
    [Index(nameof(UserTreeId))]
    public class Device_Type : IHasCreator
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public int UserTreeId { get; set; }

        public string Name { get; set; }
        public string Script { get; set; }

        public long CreatorId { get; set; }
        public virtual User Creator { get; }

        public virtual IList<ThingModel> ThingModels { get; set; }
    }
}
