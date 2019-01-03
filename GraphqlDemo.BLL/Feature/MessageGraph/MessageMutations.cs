using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GraphqlDemo.DAL.Entities;

namespace GraphqlDemo.BLL.Feature.MessageGraph
{
    public interface IMessageMutations : IMutationBase
    {
        Message UpdateMessage(Message message);
    }

    public class MessageMutations: IMessageMutations
    {
        public Message UpdateMessage(Message message)
        {
            return message;
        }
    }
}
