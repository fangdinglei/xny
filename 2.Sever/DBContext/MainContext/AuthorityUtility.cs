using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    static public class AuthorityUtility
    {
        public enum OwnerType
        {
            Non = 0,
            SonOfCreator = 1,
            Creator = 2,
            FatherOfCreator = 3,
        }
        /// <summary>
        /// 获取用户和实体的权限关系
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="ct"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        static public async Task<OwnerType> GetOwnerTypeAsync(this IHasCreator obj, MainContext ct, long uid)
        {
            var u1 = obj.CreatorId;
            var u2 = uid;
            var sf = await ct.User_SFs
              .Where(it => it.User1Id == u1 && it.User2Id == u2)
               .AsNoTracking().FirstOrDefaultAsync();
            if (sf == null)
            {
                return OwnerType.Non;
            }
            else if (sf.IsSelf)
            {
                return OwnerType.Creator;
            }
            else if (sf.IsFather)
            {
                return OwnerType.FatherOfCreator;
            }
            else
            {
                return OwnerType.SonOfCreator;
            }

        }
        /// <summary>
        /// 获取用户和实体的权限关系
        /// </summary>
        /// <param name="creator"></param>
        /// <param name="ct"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        static public async Task<OwnerType> GetOwnerTypeAsync(this long creator, MainContext ct, long uid)
        {
            var u1 = creator;
            var u2 = uid;
            var sf = await ct.User_SFs
              .Where(it => it.User1Id == u1 && it.User2Id == u2)
               .AsNoTracking().FirstOrDefaultAsync();
            if (sf == null)
            {
                return OwnerType.Non;
            }
            else if (sf.IsSelf)
            {
                return OwnerType.Creator;
            }
            else if (sf.IsFather)
            {
                return OwnerType.FatherOfCreator;
            }
            else
            {
                return OwnerType.SonOfCreator;
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="creator"></param>
        /// <param name="ct"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        static public async Task<bool> IsDirectFatherAsync(this long creator, MainContext ct, long uid)
        {
            var r = await ct.Users.Where(it => it.Id == uid && it.CreatorId == creator).CountAsync();
            return r != 0;
        }

        /// <summary>
        /// 获取所有用户可访问的实体
        /// </summary>
        /// <returns></returns>
        static public async Task<List<TEntity>> GetEntityOfAccessible<TEntity>(this DbSet<TEntity> dbset, MainContext ct, long uid
            , int takecount = -1, long cursor = -1
            , bool fathervisitson = false, bool sonvisitfather = false, bool trace = false
            , ICollection<long> wantedids = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> filter = null) where TEntity : class, IHasCreator
        {
            IQueryable<TEntity> bd;
            if (fathervisitson && sonvisitfather)
            {
                var bd1 = dbset.Join(ct.User_SFs, dt => dt.CreatorId, us => us.User1Id,
                 (dt, us) => new { us, dt })
                 .Where(it => it.us.User2Id == uid)
                 .Select(it => it.dt);
                bd = bd1;
            }
            else if (fathervisitson)
            {
                bd = dbset.Join(ct.User_SFs, dt => dt.CreatorId, us => us.User1Id,
                   (dt, us) => new { us, dt })
                   .Where(it => it.us.User2Id == uid && (!it.us.IsFather || it.us.IsSelf))
                   .Select(it => it.dt);
            }
            else if (sonvisitfather)
            {
                bd = dbset.Join(ct.User_SFs, dt => dt.CreatorId, us => us.User1Id,
                     (dt, us) => new { us, dt })
                     .Where(it => it.us.User2Id == uid && (it.us.IsFather || it.us.IsSelf))
                     .Select(it => it.dt);
            }
            else
            {//只获取自己创建的
                bd = dbset.Where(it => it.CreatorId == uid);

            }
            if (wantedids != null && wantedids.Count > 0)
            {
                bd = bd.Where(it => wantedids.Contains(it.Id));
            }
            if (cursor > 0)
            {
                bd = bd.Where(it => it.Id >= cursor);
            }
            if (takecount > 0)
            {
                bd = bd.Take(takecount);
            }
            if (filter != null)
            {
                bd = filter(bd);
            }
            if (!trace)
            {
                bd = bd.AsNoTracking();
            }
            return await bd.ToListAsync();
        }

        /// <summary>
        /// 获取所有用户可访问的实体
        /// </summary>
        /// <returns></returns>
        static public async Task<int> GetEntityOfAccessibleCount<TEntity>(this DbSet<TEntity> dbset, MainContext ct, long uid
            , int takecount = -1, long cursor = -1
            , bool fathervisitson = false, bool sonvisitfather = false, bool trace = false
            , ICollection<long> wantedids = null) where TEntity : class, IHasCreator
        {
            IQueryable<TEntity> bd;
            if (fathervisitson && sonvisitfather)
            {
                var bd1 = dbset.Join(ct.User_SFs, dt => dt.CreatorId, us => us.User1Id,
                 (dt, us) => new { us, dt })
                 .Where(it => it.us.User2Id == uid)
                 .Select(it => it.dt);
                bd = bd1;
            }
            else if (fathervisitson)
            {
                bd = dbset.Join(ct.User_SFs, dt => dt.CreatorId, us => us.User1Id,
                   (dt, us) => new { us, dt })
                   .Where(it => it.us.User2Id == uid && (!it.us.IsFather || it.us.IsSelf))
                   .Select(it => it.dt);
            }
            else if (sonvisitfather)
            {
                bd = dbset.Join(ct.User_SFs, dt => dt.CreatorId, us => us.User1Id,
                     (dt, us) => new { us, dt })
                     .Where(it => it.us.User2Id == uid && (it.us.IsFather || it.us.IsSelf))
                     .Select(it => it.dt);
            }
            else
            {//只获取自己创建的
                bd = dbset.Where(it => it.CreatorId == uid);

            }
            if (wantedids != null && wantedids.Count > 0)
            {
                bd = bd.Where(it => wantedids.Contains(it.Id));
            }
            if (cursor > 0)
            {
                bd = bd.Where(it => it.Id >= cursor);
            }
            if (takecount > 0)
            {
                bd = bd.Take(takecount);
            }
            if (!trace)
            {
                bd = bd.AsNoTracking();
            }
            return await bd.CountAsync();
        }
    }
}
