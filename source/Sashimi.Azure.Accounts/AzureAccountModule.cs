using Autofac;
using Octopus.Server.Extensibility.Extensions.Mappings;
using Sashimi.Server.Contracts.Accounts;

namespace Sashimi.Azure.Accounts
{
    public class AzureAccountModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AzureServicePrincipalAccountTypeProvider>().As<IServiceMessageHandler>().As<IAccountTypeProvider>().As<IContributeMappings>().SingleInstance();
        }
    }
}