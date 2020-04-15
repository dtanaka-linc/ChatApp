using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatAppServer.Models
{
    public class User: ModelBase
    {
        public int Id { get; set; }
        [Index(IsUnique = true)]
        [Required]
        [StringLength(256)]
        public string Name { get; set; }
        public string Password { get; set; }

        /// <summary>
        /// ChatLogモデルとのリレーション用プロパティ
        /// </summary>
        public virtual ICollection<ChatLog> ChatLogs { get; set; }
    }
}
