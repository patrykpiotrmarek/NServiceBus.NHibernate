namespace NServiceBus_6.AcceptanceTests.Config
{
    using System;
    using System.Threading.Tasks;
    using AcceptanceTesting;
    using EndpointTemplates;
    using NServiceBus_6.Config;
    using NServiceBus_6.Config.ConfigurationSource;
    using NUnit.Framework;

    public class When_only_abstract_config_override_is_found : NServiceBusAcceptanceTest
    {
        [Test]
        public Task Should_not_invoke_it()
        {
            return Scenario.Define<ScenarioContext>()
                .WithEndpoint<ConfigOverrideEndpoint>().Done(c => c.EndpointsStarted)
                .Run();
        }

        public class ConfigOverrideEndpoint : EndpointConfigurationBuilder
        {
            public ConfigOverrideEndpoint()
            {
                EndpointSetup<DefaultServer>();
            }

            abstract class ConfigErrorQueue : IProvideConfiguration<MessageForwardingInCaseOfFaultConfig>
            {
                public MessageForwardingInCaseOfFaultConfig GetConfiguration()
                {
                    throw new NotImplementedException();
                }
            }
        }
    }
}