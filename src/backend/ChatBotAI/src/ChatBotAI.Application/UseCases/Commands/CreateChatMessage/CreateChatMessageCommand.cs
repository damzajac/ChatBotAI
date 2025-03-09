using MediatR;

namespace ChatBotAI.Application.UseCases.Commands.CreateChatMessage
{
    public class CreateChatMessageCommand : IStreamRequest<CreateChatMessageCommandResponse>
    {
        public string Question { get; set; }
    }
}
