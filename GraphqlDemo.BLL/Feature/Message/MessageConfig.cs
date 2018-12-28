using System;
using System.Collections.Generic;
using System.Text;
using HotChocolate.Types;

namespace GraphqlDemo.BLL.Feature.Message
{
    public class MessageConfig: ObjectType<MessageModel>
    {
        protected override void Configure(IObjectTypeDescriptor<MessageModel> descriptor)
        {
            descriptor.Include<MessageResolvers>();
            base.Configure(descriptor);
        }
    }
}
