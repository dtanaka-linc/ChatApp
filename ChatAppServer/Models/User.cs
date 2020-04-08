using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatAppServer.Models
{
    public class User
    {
        public int Id { get; set; }
        [Index(IsUnique = true)]
        [Required]
        [StringLength(256)]
        public string Name { get; set; }
        public string Password { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
