using System;
using System.ComponentModel.DataAnnotations.Schema; 
//Add-Migration
//Remove-Migration
//Update-Database
//
namespace MyDBContext.Main
{
    public enum LogLevel : byte
    {
        Unknown,
        Warning,
        Error,
        Information,
    }
    [Table("log")]
    public class Log
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }
        public byte Level { get; set; }
        public string Title { get; set; }
        public string Msg { get; set; }
        public long Time { get; set; }

        

        [NotMapped]
        public LogLevel _Level
        {
            get => Enum.IsDefined(typeof(LogLevel), Level)
                ? (LogLevel)Level
                : LogLevel.Unknown;
            set =>
               Level = (byte) value;
        }

    }
} 
