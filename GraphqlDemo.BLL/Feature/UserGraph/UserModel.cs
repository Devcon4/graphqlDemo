using GraphqlDemo.DAL.Entities;
using HotChocolate.Types;

namespace GraphqlDemo.BLL.Feature.UserGraph
{
    public class UserModel : ObjectType<User>, IGraphQLBase {
        protected override void Configure(IObjectTypeDescriptor<User> descriptor) {
            // descriptor.Include<UserResolvers>();
        }
    }
}
