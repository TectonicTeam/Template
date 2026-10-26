using Dekacore.Messaging.Bus.Cap;
using Dekacore.Messaging.Bus.Cap.SqlServer;
using Dekacore.Web.Installer;
using BC.TempateFull.Application.Command;
using BC.TempateFull.Infra;
using BC.TempateFull.Infra.Persistence;

namespace BC.TempateFull.Api.Installers;
internal class MessagingServiceInstaller : IServiceInstaller
{
    public void Install(IHostApplicationBuilder builder)
    {

        builder.Services.AddCapMessaging(options =>
        {
            options.RegisterHandlersFrom(ApplicationCommandServiceCollectionExtensions.Assembly);
            options.UseEntityFramework<ServiceDbContext>(option => { option.Schema = Constants.Schema; });
            options.UseSqlServerTransaction();
            //options.UseInMemoryMessageQueue();
            options.UseKafka(kafka =>
            {

                if (builder.Environment.IsDevelopment())
                {
                    kafka.Servers = builder.Configuration["Kafka:Servers"]!;
                    kafka.MainConfig.Add("sasl.username", !string.IsNullOrEmpty(builder.Configuration?["Kafka:sasl.username"]) ? builder.Configuration!["Kafka:sasl.username"]! : Environment.GetEnvironmentVariable("SASL.USERNAME")!);
                    kafka.MainConfig.Add("sasl.password", !string.IsNullOrEmpty(builder.Configuration?["Kafka:sasl.password"]) ? builder.Configuration!["Kafka:sasl.password"]! : Environment.GetEnvironmentVariable("SASL.PASSWORD")!);
                }
                else
                {

                    kafka.Servers = "c0main.kf-deka.entekhabgroup.com:10084,c1main.kf-deka.entekhabgroup.com:10085,c2main.kf-deka.entekhabgroup.com:10086";
                    kafka.MainConfig.Add("sasl.username", "user1");
                    kafka.MainConfig.Add("sasl.password", "34jZ6x6Mlo");

                    //kafka.Servers = "c0prod.kf-deka.entekhabgroup.com:10074,c0prod.kf-deka.entekhabgroup.com:10075,c0prod.kf-deka.entekhabgroup.com:10076";
                    //kafka.MainConfig.Add("sasl.username", "user1");
                    //kafka.MainConfig.Add("sasl.password", "XjiVWDlFBG");

                }
                kafka.MainConfig.Add("sasl.mechanism", string.IsNullOrEmpty(builder.Configuration?["Kafka:sasl.mechanism"]) ? builder.Configuration!["sasl.mechanism"]! : "PLAIN");
                kafka.MainConfig.Add("security.protocol", !string.IsNullOrEmpty(builder.Configuration?["Kafka:security.protocol"]) ? builder.Configuration!["Kafka:security.protocol"]! : "SASL_PLAINTEXT");


            });
            options.UseDashboard();
        });
    }
}