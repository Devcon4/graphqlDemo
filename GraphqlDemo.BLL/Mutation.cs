using System;
using System.Collections.Generic;
using System.Text;
using HotChocolate.Types;

namespace GraphqlDemo.BLL
{
    public class Mutation
    {
    }


    public class MutationConfig : ObjectType<Mutation>
    {
        protected override void Configure(IObjectTypeDescriptor<Mutation> descriptor) {
            descriptor.IncludeTypeByInterface<IResolvable, InputObjectType>(typeof(IResolvable).Assembly);
        }
    }
}
