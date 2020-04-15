﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChatAppServer.Models;

namespace ChatAppServer.Repository
{
    /// <summary>
    /// Userモデルにアクセスし新規登録や認証を行うメソッド群をもつクラス
    /// </summary>
    public class UserRepository
    {

        private ChatAppDbContext DbContext { get; set; }


        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dbContext">データベースの接続やエンティティの管理を担当するChatAppDbContextクラスのインスタンス</param>
        public UserRepository(ChatAppDbContext dbContext)
        {
            /*クラスごとにDbContextをnewすると変更履歴が失われてしまうのでUserServiceからコールされるときに受けとって設定している*/
            this.DbContext = dbContext;
        }


        /// <summary>
        /// 名前とパスワードをUsersテーブルに追加し、Userモデルクラスのデータとして返す
        /// </summary>
        /// <param name="userName">UserServiceクラスのRegisterから渡されたユーザー名</param>
        /// <param name="password">UserServiceクラスのRegisterから渡されたパスワード</param>
        /// <returns>Userモデル型のデータ</returns>
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
        ///UserServiceのAuthメソッドから渡されたユーザー名・パスワードのレコードがUsersテーブルに存在するかどうかを確認する
        /// </summary>
        ///<param name="userName">UserServiceのAuthメソッドから渡されたユーザー名</param>
        ///<param name="password">UserServiceのAuthメソッドから渡されたパスワード</param>
        /// <returns>該当するユーザー名とパスワードの組み合わせのレコードがあればtrue、なければfalse</returns>
        public bool Auth(string userName, string password)
        {
            //ユーザー名が一致したレコードに対してパスワードが存在するかを調べる
            return this.DbContext.Users
                .Where(r => r.Name == userName)
                .Any(r => r.Password == password);
        }


        /// <summary>
        /// UserServiceのResisterメソッドから渡された新規ユーザー名が既存のNameカラムのレコードと重複しているかを確認する/// </summary>
        /// <param name="userName">UserServiceのResisterメソッドから渡された新規ユーザー名</param>
        /// <returns>重複している場合true, していなければfalse</returns>
        public bool ExistsUserName(string userName)
        {
            return this.DbContext.Users
                .Any(r => r.Name == userName);
        }

    }
}
