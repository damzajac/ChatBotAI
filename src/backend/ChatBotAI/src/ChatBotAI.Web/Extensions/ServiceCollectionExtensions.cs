using ChatBotAI.Application.Services;
using ChatBotAI.Domain.Repositories;
using ChatBotAI.Infrastructure.Data;
using ChatBotAI.Infrastructure.Repositories;
using ChatBotAI.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace ChatBotAI.Web.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddScoped<IChatMessageRepository, ChatMessageRepository>()
                .AddScoped<IGenerateChatAnswerService, LoremIpsumChatAnswerGeneratorService>();

            services.AddDbContext<ChatBotDbContext>(opt
                => opt.UseSqlServer(configuration.GetConnectionString("ChatBotConnectionString")));

            return services;
        }
    }
}
