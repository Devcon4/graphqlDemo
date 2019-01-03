using GraphqlDemo.BLL.Feature.MessageGraph;
using GraphqlDemo.DAL.Entities;
using HotChocolate.Types;

namespace GraphqlDemo.BLL.Feature.UserGraph
{
    public class UserModel : ObjectType<User>, IModelBase
    {
        protected override void Configure(IObjectTypeDescriptor<User> descriptor)
        {
            descriptor.Field("messages").Resolver(ctx => ctx.Service<IMessageQueries>().MessagesForUserId(ctx.Parent<User>().Id));
        }
    }
}
