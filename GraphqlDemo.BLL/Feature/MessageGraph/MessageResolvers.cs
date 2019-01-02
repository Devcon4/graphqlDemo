using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GraphqlDemo.DAL.Entities;

namespace GraphqlDemo.BLL.Feature.MessageGraph
{
    public class MessageResolvers: IResolvable
    {
        public Task<IEnumerable<Message>> MessagesForUserId(int id)
        {
            return new Task<IEnumerable<Message>>(f => new List<Message>(), CancellationToken.None);
        }

        public Task<Message> UpdateMessage(MessageModelInput message)
        {
            throw new NotImplementedException();
        }

    }
}
