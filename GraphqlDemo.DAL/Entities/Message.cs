using System;
using System.Collections.Generic;
using System.Text;

namespace GraphqlDemo.DAL.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}
