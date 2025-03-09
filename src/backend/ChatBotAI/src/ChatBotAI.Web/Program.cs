using ChatBotAI.Application.UseCases.Commands.UpdateChatMessage;
using ChatBotAI.Infrastructure.Data;
using ChatBotAI.Web.Behaviors;
using ChatBotAI.Web.Extensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateChatMessageCommandValidator>();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining<UpdateChatMessageCommand>();
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

builder.Services.AddAllowAllCors();

var app = builder.Build();

ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Stop;
ValidatorOptions.Global.DefaultClassLevelCascadeMode = CascadeMode.Stop;

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var chatBotDbContext = scope.ServiceProvider.GetRequiredService<ChatBotDbContext>();
    chatBotDbContext.Database.Migrate();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseAllowAllCors();
app.MapControllers();
app.Run();
