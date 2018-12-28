using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace GraphqlDemo.BLL.Feature.Message
{
    public class MessageResolvers: IResolvable
    {
        public Task<List<MessageModel>> MessagesForUserId(int id)
        {
            return new Task<List<MessageModel>>(f => new List<MessageModel>(), null);
        }

        public Task<MessageModel> UpdateMessage(MessageModelInput message)
        {
            throw new NotImplementedException();
        }

    }
}
