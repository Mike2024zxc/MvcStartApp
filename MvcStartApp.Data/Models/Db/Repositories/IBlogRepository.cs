using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcStartApp.Data.Models.Db.Repositories
{
        public interface IBlogRepository
        {
            Task AddUser(User user);
            Task<User[]> GetUsers();
        }

}
 
