using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppServer.Models
{
    public class ChatLog: ModelBase
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Body { get; set; }
        
        /// <summary>
        /// Userモデルとのリレーションのためのプロパティ
        /// </summary>
        public User User { get; set; }
    }
}
