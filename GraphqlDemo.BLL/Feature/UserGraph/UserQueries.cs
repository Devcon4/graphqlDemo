using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GraphqlDemo.DAL.Entities;

namespace GraphqlDemo.BLL.Feature.UserGraph
{
    public interface IUserQueries: IQueryBase
    {
        List<User> GetUsers();
        User GetUserByMessageId(int id);
    }

    public class UserQueries: IUserQueries
    {
         public List<User> GetUsers()
         {
             return new List<User>()
             {
                 new User()
                 {
                     Id = 4312,
                     Name = "Test!"
                 }
             };
         }

        public User GetUserByMessageId(int id)
        {
            return new User()
            {
                Id = 6534,
                Name = "Test!!!"
            };
        }
    }
}
