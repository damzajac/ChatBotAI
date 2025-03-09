namespace ChatBotAI.Application.UseCases.Commands.CreateChatMessage
{
    public class CreateChatMessageCommandResponse
    {
        public string EventType { get; }
        public string Data { get; }

        public CreateChatMessageCommandResponse(string eventType, string data)
        {
            EventType = eventType;
            Data = data;
        }
    }
}
