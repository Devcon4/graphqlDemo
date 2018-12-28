using HotChocolate.Types;

namespace GraphqlDemo.BLL.Feature.User
{
    class UserConfig : ObjectType<UserModel>{
        protected override void Configure(IObjectTypeDescriptor<UserModel> descriptor)
        {
            descriptor.Include<UserResolvers>();
            base.Configure(descriptor);
        }
    }
}
