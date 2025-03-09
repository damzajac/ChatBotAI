using ChatBotAI.Domain.Repositories;
using MediatR;

namespace ChatBotAI.Application.UseCases.Commands.UpdateChatMessage
{
    public class UpdateChatMessageCommandHandler : IRequestHandler<UpdateChatMessageCommand>
    {
        private readonly IChatMessageRepository chatMessageRepository;

        public UpdateChatMessageCommandHandler(IChatMessageRepository chatMessageRepository)
        {
            this.chatMessageRepository = chatMessageRepository;
        }

        public async Task Handle(UpdateChatMessageCommand request, CancellationToken cancellationToken)
        {
            var chatMessage = await chatMessageRepository.Get(request.ChatMessageId.Value);
            if (chatMessage == null)
            {
                throw new ArgumentException();
            }

            if (chatMessage.IsLiked != request.IsLiked)
            {
                chatMessage.IsLiked = request.IsLiked;
                await chatMessageRepository.Commit();
            }
        }
    }
}
