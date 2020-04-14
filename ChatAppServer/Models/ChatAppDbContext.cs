﻿namespace ChatAppServer.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Collections.Generic;

    public class ChatAppDbContext : DbContext {
        public DbSet<User> Users { get; set; }
        public DbSet<ChatLog> ChatLogs { get; set; }

        /// <summary>
        /// エンティティの新規作成時または更新時にCreatedAtとUpdatedAtを自動で現在時刻に設定する
        /// </summary>
        /// <returns>SaveChangesの実行結果</returns>
        public override int SaveChanges()
        {
            var now = DateTime.Now;
            foreach (var entry in ChangeTracker.Entries().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
            {
                var type = entry.Entity.GetType();
                if (entry.State == EntityState.Added && HasCreatedAt(type))
                    entry.Property("CreatedAt").CurrentValue = now;
                if (HasUpdatedAt(type))
                    entry.Property("UpdatedAt").CurrentValue = now;
            }
            return base.SaveChanges();
        }


        private static Dictionary<Type, bool> _entityHasCreatedAtDic = new Dictionary<Type, bool>();
        private static Dictionary<Type, bool> _entityHasUpdatedAtDic = new Dictionary<Type, bool>();

        /// <summary>
        /// 条件にあったCreatedAtプロパティを持つエンティティがあるするかどうかを判定する
        /// </summary>
        /// <param name="type">エンティティの型</param>
        /// <returns>条件にあったCreatedAtプロパティを持つエンティティの有無</returns>
        private bool HasCreatedAt(Type type)
        {
            if (!_entityHasCreatedAtDic.ContainsKey(type))
                _entityHasCreatedAtDic[type] = type.GetProperties().Any(p =>
                p.Name == "CreatedAt" && p.CanWrite && p.PropertyType == typeof(DateTime));
            return _entityHasCreatedAtDic[type];
        }

        /// <summary>
        /// 条件にあったUpdatedAtプロパティを持つエンティティがあるするかどうかを判定する
        /// </summary>
        /// <param name="type">エンティティの型</param>
        /// <returns>条件にあったUpdatedAtプロパティを持つエンティティの有無</returns>
        private bool HasUpdatedAt(Type type)
        {
            if (!_entityHasUpdatedAtDic.ContainsKey(type))
                _entityHasUpdatedAtDic[type] = type.GetProperties().Any(p =>
                p.Name == "UpdatedAt" && p.CanWrite && p.PropertyType == typeof(DateTime));
            return _entityHasUpdatedAtDic[type];
        }
    }
}