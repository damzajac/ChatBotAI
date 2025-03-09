using MediatR;
using System.Text.Json.Serialization;

namespace ChatBotAI.Application.UseCases.Commands.UpdateChatMessage
{
    public class UpdateChatMessageCommand : IRequest
    {
        [JsonIgnore]
        public Guid? ChatMessageId { get; set; }
        public bool? IsLiked { get; set; }
    }
}
