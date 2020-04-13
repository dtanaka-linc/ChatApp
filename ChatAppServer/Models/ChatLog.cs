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
        /// Userモデルとのリレーション用プロパティ
        /// </summary>
        public virtual User User { get; set; }
    }
}
