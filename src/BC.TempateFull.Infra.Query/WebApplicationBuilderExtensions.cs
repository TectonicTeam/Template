using Microsoft.AspNetCore.Builder;

namespace BC.TempateFull.Infra.Query;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder RegisterClubQuery(this WebApplicationBuilder app)
    {
        ArgumentNullException.ThrowIfNull(app);

        app.Services.AddQuery(app.Configuration, app.Environment);

        return app;
    }
}