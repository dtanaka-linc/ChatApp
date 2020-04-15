﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatAppServer.Models;
using ChatAppLibrary.Telegram;
using ChatAppServer.Repository;

namespace ChatAppServer.Service
{
    /// <summary>
    /// UserRepositoryに定義しているビジネスロジックに受け渡すためのメソッド群を定義しているクラス
    /// </summary>
    public class UserService
    {
        //
        public UserRepository UserRepository { get; set; }


        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="DbContext">データベースの接続やエンティティの管理を担当するChatAppDbContextクラスのインスタンス</param>
        public UserService(ChatAppDbContext DbContext)
        {
            /*UserRepositoryのインスタンスはこのクラスのすべてのメソッドで利用するのでコンストラクタ内でインスタンスを生成しプロパティに格納しておく*/
            /*DbContextはコールするごとにnewすると変更履歴が失われてしまうので○○Telegramクラスからこのクラスをコールされるときに受け取るようにする。また、このクラス内から直接DBを参照するのを防ぐためにプロパティは定義せずUserRepositoryインスタンス生成時の引数としてだけ利用する*/
            this.UserRepository = new UserRepository(DbContext);
        }


        /// <summary>
        /// RegistrationTelegramから文字列に戻されたユーザー名やパスワードを受け取り、ユーザー名の重複がなければUserRepositoryのCreateUserメソッドへデータを受け渡す
        /// </summary>
        /// <param name="registrationData">RegistrationTelegramで文字列に戻されたユーザー名やパスワード</param>
        /// <returns>新しいUserモデルクラスのデータまたはnull</returns>
        public User Register(RegistrationTelegram registrationData)
        {
            var userName = registrationData.GetHeader().UserName;
            var PassWord = registrationData.PassWord;

            //ExistUserNameで既存のユーザー名との重複を確認し新しいUserモデルクラスのデータまたはnullを返す
            if (UserRepository.ExistsUserName(userName))
            {
                return UserRepository.CreateUser(userName, PassWord);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// RegistrationTelegramで文字列に戻されたユーザー名やパスワードをUserRepositoryクラスのAuthメソッドに渡して認証結果を得る
        /// </summary>
        /// <param name="authRequestData">AuthTelegramのインスタンス</param>
        /// <returns>Usersテーブルに該当レコードの有があればtrue、なければfalse</returns>
        public bool Auth(AuthRequestTelegram authRequestData)
        {
            //名前が長いのでAuthTeregramの各プロパティの情報を変数に格納する
            var userName = authRequestData.GetHeader().UserName;
            var password = authRequestData.PassWord;

            //UserRepositoryのAuthメソッドの結果を格納する変数　代入しなくていいかな？？
            var authResult = UserRepository.Auth(userName, password);

            //Authメソッドで該当するレコードがあればtrue、なければfalseを返す
            if (authResult != null)
            {
                return true;
            }
            return false;
            }
        }
    }
}
