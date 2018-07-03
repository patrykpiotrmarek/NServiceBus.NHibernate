using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NServiceBus_6;
using NServiceBus_6.AcceptanceTesting.Support;
using NServiceBus_6.Persistence;

public class ConfigureScenariosForNHibernatePersistence : IConfigureSupportedScenariosForTestExecution
{
    public IEnumerable<Type> UnsupportedScenarioDescriptorTypes { get; } = new List<Type>();
}

public class ConfigureEndpointNHibernatePersistence : EndpointConfigurer
{
    public override Task Configure(string endpointName, EndpointConfiguration configuration, RunSettings settings, PublisherMetadata publisherMetadata)
    {
        configuration.UsePersistence<NHibernatePersistence>()
            .ConnectionString(ConnectionString);

        return Task.FromResult(0);
    }    
}