namespace NServiceBus.TimeoutPersisters.NHibernate.Tests
{
    using System;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Security.Principal;
    using global::NHibernate;
    using NServiceBus.Config.ConfigurationSource;
    using NServiceBus.NHibernate.Internal;
    using NUnit.Framework;
    using Persistence.NHibernate;
    using Pipeline;

    public abstract class InMemoryDBFixture
    {
        protected TimeoutStorage persister;
        protected ISessionFactory sessionFactory;

        private readonly string connectionString = String.Format(@"Data Source={0};Version=3;New=True;", Path.GetTempFileName());
        private const string dialect = "NHibernate.Dialect.SQLiteDialect";

        [SetUp]
        public void Setup()
        {
            Configure.ConfigurationSource = new DefaultConfigurationSource();

            NHibernateSettingRetriever.AppSettings = () => new NameValueCollection
                                                               {
                                                                   {"NServiceBus/Persistence/NHibernate/dialect", dialect}
                                                               };

            NHibernateSettingRetriever.ConnectionStrings = () => new ConnectionStringSettingsCollection
                                                                     {
                                                                         new ConnectionStringSettings("NServiceBus/Persistence/NHibernate/Timeout", connectionString)
                                                                     };

            ConfigureNHibernate.Init();

            Configure.With(Enumerable.Empty<Type>())
                .DefineEndpointName("Foo")
                .DefaultBuilder()
                .UseNHibernateTimeoutPersister();

            persister = Configure.Instance.Builder.Build<TimeoutStorage>();
            persister.PipelineExecutor = new PipelineExecutor(Configure.Instance.Builder, new PipelineBuilder(Configure.Instance.Builder));
            sessionFactory = persister.SessionFactory;

            new Installer.Installer().Install(WindowsIdentity.GetCurrent().Name);
        }
    }
}
