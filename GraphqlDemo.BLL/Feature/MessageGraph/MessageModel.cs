using System;
using System.Collections.Generic;
using System.Text;
using GraphqlDemo.BLL.Feature.UserGraph;
using HotChocolate.Types;
using GraphqlDemo.DAL.Entities;

namespace GraphqlDemo.BLL.Feature.MessageGraph
{
    public class MessageModel: ObjectType<Message>, IModelBase
    {
        protected override void Configure(IObjectTypeDescriptor<Message> descriptor)
        {
            descriptor.Field("user").Resolver(ctx => ctx.Service<IUserQueries>().GetUserByMessageId(ctx.Parent<Message>().Id));
        }
    }
}
