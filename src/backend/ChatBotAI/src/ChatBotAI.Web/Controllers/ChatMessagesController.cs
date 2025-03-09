using ChatBotAI.Application.DTOs;
using ChatBotAI.Application.UseCases.Commands.CreateChatMessage;
using ChatBotAI.Application.UseCases.Commands.UpdateChatMessage;
using ChatBotAI.Application.UseCases.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ChatBotAI.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatMessagesController : ControllerBase
    {
        private readonly IMediator mediator;

        public ChatMessagesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<ChatMessageDto>> GetAll([FromQuery] GetChatMessagesQuery query)
        {
            return await mediator.Send(query);
        }

        [HttpPut("{id}")]
        public async Task Update([FromRoute] Guid? id, [FromBody] UpdateChatMessageCommand command)
        {
            command.ChatMessageId = id;
            await mediator.Send(command);
        }

        [HttpGet("conversation")]
        public async Task Create([FromQuery] CreateChatMessageCommand command, CancellationToken cancellationToken)
        {
            Response.Headers.Append("Content-Type", "text/event-stream");

            await foreach (var response in mediator.CreateStream<CreateChatMessageCommandResponse>(command, cancellationToken))
            {
                await WriteEventToStream(response.EventType, response.Data);
            }

            async Task WriteEventToStream(string eventType, string data)
            {
                await Response.WriteAsync($"event: {eventType}\n");
                await Response.WriteAsync($"data: {data}");
                await Response.WriteAsync($"\n\n");
                await Response.Body.FlushAsync();
            }
        }
    }
}
