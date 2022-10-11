using Microsoft.EntityFrameworkCore;
//Add-Migration
//Remove-Migration [ -Migration 0 ] 
//Update-Database
//Drop-Database 
//
namespace MyDBContext.Main
{
    public interface IDBValueBuilder
    {
        void OnModelCreating(ModelBuilder modelBuilder);
    }


}
