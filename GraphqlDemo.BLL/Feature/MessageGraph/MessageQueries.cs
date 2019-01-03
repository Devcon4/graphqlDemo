using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GraphqlDemo.DAL.Entities;

namespace GraphqlDemo.BLL.Feature.MessageGraph
{
    public interface IMessageQueries: IQueryBase
    {
        List<Message> MessagesForUserId(int id);
        List<Message> GetAllMessages();
    }

    public class MessageQueries: IMessageQueries
    {
        public List<Message> MessagesForUserId(int id)
        {
            return new List<Message>()
            {
                new Message()
                {
                    Id = 1234,
                    Text = $"Test-{id}",
                    CreatedDateTime = DateTime.Today
                }
            };
        }

        public List<Message> GetAllMessages()
        {
            return new List<Message>()
            {
                new Message()
                {
                    Id = 123,
                    CreatedDateTime = DateTime.Today,
                    Text = "First!"
                }
            };
        }
    }
}
