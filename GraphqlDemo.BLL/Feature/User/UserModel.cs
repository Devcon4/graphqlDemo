namespace GraphqlDemo.BLL.Feature.User
{
    public class UserModel : IGraphqlable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Phone { get; set; }
    }
}
