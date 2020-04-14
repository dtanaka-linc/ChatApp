using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppServer.Models
{
    /// <summary>
    /// 全てのモデルで共通のプロパティをまとめたクラス
    /// </summary>
    public class ModelBase
    {
        public DateTime UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
