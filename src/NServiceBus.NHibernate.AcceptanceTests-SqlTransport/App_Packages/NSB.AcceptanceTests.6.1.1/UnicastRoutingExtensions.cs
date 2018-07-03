namespace NServiceBus_6.AcceptanceTests.Routing
{
    using System;
    using Configuration.AdvanceExtensibility;
    using NServiceBus_6.Routing;

    static class UnicastRoutingExtensions
    {
        public static void RegisterEndpointInstances(this RoutingSettings config, params EndpointInstance[] instances)
        {
            config.GetSettings().GetOrCreate<EndpointInstances>().AddOrReplaceInstances(Guid.NewGuid().ToString(), instances);
        }
    }
}