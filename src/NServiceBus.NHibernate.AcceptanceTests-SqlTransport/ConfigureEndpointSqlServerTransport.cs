using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NServiceBus_6;
using NServiceBus_6.AcceptanceTesting.Support;
using NServiceBus_6.AcceptanceTests.ScenarioDescriptors;
using NServiceBus_6.Persistence;

public class ConfigureScenariosForSqlServerTransport : IConfigureSupportedScenariosForTestExecution
{
    public IEnumerable<Type> UnsupportedScenarioDescriptorTypes { get; } = new[]
    {
        typeof(AllTransportsWithCentralizedPubSubSupport)
    };
}
public class ConfigureEndpointSqlServerTransport : EndpointConfigurer
{
    public override Task Configure(string endpointName, EndpointConfiguration configuration, RunSettings settings, PublisherMetadata publisherMetadata)
    {
        configuration.UsePersistence<NHibernatePersistence>()
            .ConnectionString(ConnectionString);

        var transportConfig = configuration.UseTransport<SqlServerTransport>();

        transportConfig.ConnectionString(ConnectionString);

        var routingConfig = transportConfig.Routing();

        foreach (var publisher in publisherMetadata.Publishers)
        {
            foreach (var eventType in publisher.Events)
            {
                routingConfig.RegisterPublisher(eventType, publisher.PublisherName);
            }
        }

        return Task.FromResult(0);
    }
}