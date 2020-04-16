﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//作業メモ：もしハッシュ化をこのクラスで行わないなら削除
using System.Security.Cryptography;
using ChatAppServer.Models;
using ChatAppLibrary.Telegram;
using ChatAppServer.Repository;

namespace ChatAppServer.Service
{

    //作業メモ：パスワードをハッシュ化する処理を○○Telegramクラス側で行うかどうか相談中


    /// <summary>
    /// UserRepositoryに定義しているビジネスロジックに受け渡すためのメソッド群を定義しているクラス
    /// </summary>
    public class UserService
    {
        //プロパティ
        /*UserRepositoryインスタンスはこのクラスの複数のメソッドで使うのでコンストラクタで生成されたらプロパティに格納している*/
        public UserRepository UserRepository { get; set; }
        //作業メモ：もしハッシュ化をこのクラスで行わないなら削除
        public SHA256 Sha { get; set; }


        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dbContext">データベースの接続やエンティティの管理を担当するChatAppDbContextクラスのインスタンス</param>
        public UserService(ChatAppDbContext dbContext)
        {
            /*UserRepositoryのインスタンスはこのクラスのすべてのメソッドで利用するのでコンストラクタ内でインスタンスを生成しプロパティに格納しておく*/
            /*DbContextはコールするごとにnewすると変更履歴が失われてしまうので○○Telegramクラスからこのクラスをコールされるときに受け取るようにする。また、このクラス内から直接DBを参照するのを防ぐためにプロパティは定義せずUserRepositoryインスタンス生成時の引数としてだけ利用する*/
            this.UserRepository = new UserRepository(dbContext);
            //作業メモ：もしハッシュ化をこのクラスで行わないなら削除
            this.Sha = new SHA256CryptoServiceProvider();
        }


        /// <summary>
        /// RegistrationTelegramから文字列に戻されたユーザー名やパスワードを受け取り、ユーザー名の重複がなければUserRepositoryのCreateUserメソッドへデータを受け渡す
        /// </summary>
        /// <param name="registrationData">RegistrationTelegramで文字列に戻されたユーザー名やパスワード</param>
        /// <returns>新しいUserモデルクラスのデータまたはnull</returns>
        public User Register(RegistrationTelegram registrationData)
        {
            //名前が長いので一度変数に格納します
            //作業メモ：ハッシュ化されたパスワードを受け取る場合はプロパティ名変えるかも
            var userName = registrationData.GetHeader().UserName;
            var password = registrationData.PassWord;

            //ExistUserNameで既存のユーザー名との重複を確認し新しいUserモデルクラスのデータまたはnullを返す
            if (UserRepository.ExistsUserName(userName))
            {
                return UserRepository.CreateUser(userName, password);
            }
            return null;
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

            //UserRepositoryのAuthメソッドの結果を返却する(該当するユーザー名とパスワードの組み合わせのデータがあればtrue、なければfalse)
            return UserRepository.Auth(userName, password);
        }
    }
}
