﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChatAppServer.Models;

namespace ChatAppServer.Repository
{
    /// <summary>
    /// Userモデルにアクセスし新規登録や認証を行うメソッド群
    /// </summary>
    public class UserRepository
    {

        private ChatAppDbContext DbContext { get; set; }

        //コンストラクタ
        public UserRepository(ChatAppDbContext dbContext)
        {
            this.DbContext = dbContext;
        }


        /// <summary>
        /// 名前とパスワードをUsersに追加し、Userクラス型のデータとして返す
        /// </summary>
        /// <param name="userName">ログインフォームで入力されたユーザー名</param>
        /// <param name="password">ログインフォームで入力されたパスワード</param>
        /// <returns>Userクラス型のデータ</returns>
        public User CreateUser(string userName, string password)
        {
            var user = new User()
            {
                Name = userName,
                Password = password
            };
            DbContext.Users.Add(user);
            DbContext.SaveChanges();
            return user;
        }


        /// <summary>
        /// ユーザー名で検索する
        /// </summary>
        /// <param name="userName">ユーザー名</param>
        /// <returns>ユーザー名で検索した結果</returns>
        public IQueryable<User> FindByUserName(string userName)
        {
            return this.DbContext.Users.Where(r => r.Name == userName);
        }


        /// <summary>
        /// 新規登録フォームで入力されたユーザー名と同名のレコードが存在するかどうかを確認する
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>入力されたユーザー名の同名のレコードの有無</returns>
        public bool ExistsUserName(string userName)
        {
            return this.DbContext.Users.Any(r => r.Name == userName);
        }

        /// <summary>
        /// ユーザー名からIDやパスワードなどの情報を取得するメソッド
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public User FindByUserName(string userName)
        {

        }

    }
}
