using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppServer.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        public void Resister(string user_name, string password)
        {
            using(var context = new ChatAppDbContext())
            {
                var new_user = new User()
                {
                    Name = user_name,
                    Password = password
                };

                context.Users.Add(new_user);
                context.SaveChanges();
            }
        }
    }
}
