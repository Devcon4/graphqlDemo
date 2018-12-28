using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GraphqlDemo.BLL.Feature.User
{
    public class UserResolvers: IResolvable
    {
        public Task<List<UserModel>> GetUsers()
        {
            throw new NotImplementedException();
        }
    }
}
