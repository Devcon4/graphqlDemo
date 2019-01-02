using System;
using System.Collections.Generic;
using System.Text;
using GraphqlDemo.DAL.Entities;
using HotChocolate.Types;

namespace GraphqlDemo.BLL.Feature.MessageGraph
{
    public class MessageModelInput: InputObjectType<Message>, IGraphQLBase
    {
        protected override void Configure(IInputObjectTypeDescriptor<Message> descriptor)
        {
        }
    }
}
