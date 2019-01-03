using System;
using System.Collections.Generic;
using System.Text;

namespace GraphqlDemo.DAL.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Phone { get; set; }
        public List<Message> Messages { get; set; }
    }
}
