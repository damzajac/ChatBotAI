namespace ChatBotAI.Web.Extensions
{
    public static class CorsExtensions
    {
        private const string AllowAllPolicy = "AllowAllPolicy";

        public static IServiceCollection AddAllowAllCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(AllowAllPolicy, builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            return services;
        }

        public static IApplicationBuilder UseAllowAllCors(this IApplicationBuilder app)
        {
            app.UseCors(AllowAllPolicy);

            return app;
        }
    }
}
