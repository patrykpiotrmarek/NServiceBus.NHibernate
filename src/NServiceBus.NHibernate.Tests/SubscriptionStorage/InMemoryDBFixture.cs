namespace NServiceBus.Unicast.Subscriptions.NHibernate.Tests
{
    using System.Threading.Tasks;
    using global::NHibernate;
    using global::NHibernate.Cfg;
    using global::NHibernate.Dialect;
    using global::NHibernate.Mapping.ByCode;
    using global::NHibernate.Tool.hbm2ddl;
    using NServiceBus.NHibernate.Tests;
    using NUnit.Framework;

    class InMemoryDBFixture
    {
        protected SubscriptionPersister storage;
        protected ISessionFactory SessionFactory;
        SchemaExport schema;

        [SetUp]
        public async Task SetupContext()
        {
            var cfg = new Configuration()
                .DataBaseIntegration(x =>
                {
                    x.Dialect<MsSql2012Dialect>();
                    x.ConnectionString = Consts.SqlConnectionString;
                });

            var mapper = new ModelMapper();
            mapper.AddMapping<Config.SubscriptionMap>();

            cfg.AddMapping(mapper.CompileMappingForAllExplicitlyAddedEntities());

            schema = new SchemaExport(cfg);
            await schema.CreateAsync(false, true);

            SessionFactory = cfg.BuildSessionFactory();
            storage = new SubscriptionPersister(SessionFactory);
        }

        [TearDown]
        public Task TearDown() => schema.DropAsync(false, true);
    }
}