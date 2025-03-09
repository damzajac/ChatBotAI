using ChatBotAI.Application.DTOs;
using MediatR;

namespace ChatBotAI.Application.UseCases.Queries
{
    public class GetChatMessagesQuery : IRequest<IEnumerable<ChatMessageDto>>
    {
        public int? SkipMessagesNumber { get; set; }
        public int? TakeMessagesNumber { get; set; }
    }
}
