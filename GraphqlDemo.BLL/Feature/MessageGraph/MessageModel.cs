using System;
using System.Collections.Generic;
using System.Text;
using HotChocolate.Types;
using GraphqlDemo.DAL.Entities;

namespace GraphqlDemo.BLL.Feature.MessageGraph
{
    public class MessageModel: ObjectType<Message>, IGraphQLBase
    {
        protected override void Configure(IObjectTypeDescriptor<Message> descriptor)
        {
            // descriptor.Include<MessageResolvers>();
        }
    }
}
