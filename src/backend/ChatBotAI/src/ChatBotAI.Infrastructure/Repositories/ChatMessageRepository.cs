using ChatBotAI.Domain.Entities;
using ChatBotAI.Domain.Repositories;
using ChatBotAI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ChatBotAI.Infrastructure.Repositories
{
    public class ChatMessageRepository : IChatMessageRepository
    {
        private readonly ChatBotDbContext dbContext;

        public ChatMessageRepository(ChatBotDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Commit()
        {
            await dbContext.SaveChangesAsync();
        }

        public async Task Create(ChatMessage chatMessage)
        {
            await dbContext.ChatMessages.AddAsync(chatMessage);
        }

        public async Task<ChatMessage> Get(Guid id)
        {
            return await dbContext.ChatMessages.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<ChatMessage>> GetAll(int skip, int take)
        {
            return await dbContext.ChatMessages
                .OrderBy(x => x.CreatedAt)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }
    }
}
