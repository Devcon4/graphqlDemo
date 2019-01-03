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
            descriptor.IncludeQueriesByInterface<IMutationBase>(typeof(IMutationBase).Assembly);
            //descriptor.IncludeTypeByInterface<IResolvable>(IncludeType.Mutation, typeof(IResolvable).Assembly);
        }
    }
}
