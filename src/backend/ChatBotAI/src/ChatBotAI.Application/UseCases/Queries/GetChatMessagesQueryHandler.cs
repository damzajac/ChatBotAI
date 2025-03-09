using ChatBotAI.Application.DTOs;
using ChatBotAI.Domain.Repositories;
using MediatR;

namespace ChatBotAI.Application.UseCases.Queries
{
    public class GetChatMessagesQueryHandler : IRequestHandler<GetChatMessagesQuery, IEnumerable<ChatMessageDto>>
    {
        private const int MaxTakeNumber = 10;
        private readonly IChatMessageRepository chatMessageRepository;

        public GetChatMessagesQueryHandler(IChatMessageRepository chatMessageRepository)
        {
            this.chatMessageRepository = chatMessageRepository;
        }

        public async Task<IEnumerable<ChatMessageDto>> Handle(GetChatMessagesQuery request, CancellationToken cancellationToken)
        {
            var skipNumber = request.SkipMessagesNumber != null && request.SkipMessagesNumber >= 0 ? request.SkipMessagesNumber.Value : 0;
            var takeNumber = request.TakeMessagesNumber != null && request.TakeMessagesNumber <= MaxTakeNumber ? request.TakeMessagesNumber.Value : MaxTakeNumber;

            var chatMessages = await chatMessageRepository.GetAll(skipNumber, takeNumber);

            return chatMessages.Select(x => new ChatMessageDto
            {
                Id = x.Id,
                CreatedAt = x.CreatedAt,
                Question = x.Question,
                Answer = x.Answer,
                IsLiked = x.IsLiked
            });
        }
    }
}
