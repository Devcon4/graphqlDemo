using System;
using System.Collections.Generic;
using System.Text;

namespace GraphqlDemo.BLL.Feature.Message
{
    public class MessageModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}
