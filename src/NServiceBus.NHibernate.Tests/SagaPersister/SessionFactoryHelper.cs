namespace NServiceBus_6.SagaPersisters.NHibernate.Tests
{
    using System;
    using Features;
    using global::NHibernate.Cfg;
    using global::NHibernate.Impl;
    using NServiceBus_6.Sagas;
    using Settings;

    public static class SessionFactoryHelper
    {
        public static SessionFactoryImpl Build(Type[] types)
        {
            var builder = new NHibernateSagaStorage();
            var properties = SQLiteConfiguration.InMemory();

            var configuration = new Configuration().AddProperties(properties);
            var settings = new SettingsHolder();

            var metaModel = new SagaMetadataCollection();
            metaModel.Initialize(types);
            settings.Set<SagaMetadataCollection>(metaModel);

            settings.Set("TypesToScan", types);
            builder.ApplyMappings(settings, configuration);
            return configuration.BuildSessionFactory() as SessionFactoryImpl;
        }
    }
}