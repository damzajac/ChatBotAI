using ChatBotAI.Domain.Entities;
using ChatBotAI.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace ChatBotAI.Infrastructure.Data
{
    public class ChatBotDbContext(DbContextOptions<ChatBotDbContext> options) : DbContext(options)
    {
        public DbSet<ChatMessage> ChatMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(ChatMessageConfiguration).Assembly);
        }
    }
}
