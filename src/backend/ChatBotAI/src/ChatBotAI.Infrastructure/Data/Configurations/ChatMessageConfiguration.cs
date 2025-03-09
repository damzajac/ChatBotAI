using ChatBotAI.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ChatBotAI.Infrastructure.Data.Configurations
{
    public class ChatMessageConfiguration : IEntityTypeConfiguration<ChatMessage>
    {
        public void Configure(EntityTypeBuilder<ChatMessage> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Question)
                .IsRequired();
        }
    }
}
