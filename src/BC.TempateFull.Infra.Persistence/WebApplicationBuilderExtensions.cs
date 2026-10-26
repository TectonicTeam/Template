using Microsoft.AspNetCore.Builder;

namespace BC.TempateFull.Infra.Persistence;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder RegisterWebHookPersistence(this WebApplicationBuilder app)
    {
        ArgumentNullException.ThrowIfNull(app);

        app.Services.AddPersistence(app.Configuration, app.Environment);

        return app;
    }
}