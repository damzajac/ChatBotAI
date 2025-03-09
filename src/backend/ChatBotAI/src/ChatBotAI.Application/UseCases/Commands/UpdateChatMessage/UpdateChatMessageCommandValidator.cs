using FluentValidation;

namespace ChatBotAI.Application.UseCases.Commands.UpdateChatMessage
{
    public class UpdateChatMessageCommandValidator : AbstractValidator<UpdateChatMessageCommand>
    {
        public UpdateChatMessageCommandValidator()
        {
            RuleFor(x => x.ChatMessageId)
                .NotNull();

            RuleFor(x => x.IsLiked)
                .NotNull();
        }
    }
}
