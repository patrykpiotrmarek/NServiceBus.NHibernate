﻿namespace NServiceBus_6.Persistence
{
    using System;
    using Configuration.AdvanceExtensibility;
    using global::NHibernate;
    using global::NHibernate.Cfg;

    /// <summary>
    /// Shared configuration extensions.
    /// </summary>
    public static class SharedConfig
    {
        /// <summary>
        /// Sets the connection string to use for all storages
        /// </summary>
        /// <param name="persistenceConfiguration"></param>
        /// <param name="connectionString">The connection string to use.</param>
        public static PersistenceExtensions<NHibernatePersistence> ConnectionString(this PersistenceExtensions<NHibernatePersistence> persistenceConfiguration, string connectionString)
        {
            persistenceConfiguration.GetSettings().Set("NHibernate.Common.ConnectionString", connectionString);
            return persistenceConfiguration;
        }

        /// <summary>
        /// Disables automatic schema update.
        /// </summary>
        /// <param name="persistenceConfiguration"></param>
        public static PersistenceExtensions<NHibernatePersistence> DisableSchemaUpdate(this PersistenceExtensions<NHibernatePersistence> persistenceConfiguration)
        {
            persistenceConfiguration.GetSettings().Set("NHibernate.Common.AutoUpdateSchema", false);
            return persistenceConfiguration;
        }

        /// <summary>
        /// Configures Subscription Storage to use the <paramref name="configuration"/>.
        /// </summary>
        /// <param name="persistenceConfiguration"></param>
        /// <param name="configuration">The <see cref="Configuration"/> object.</param>
        public static PersistenceExtensions<NHibernatePersistence> UseConfiguration(this PersistenceExtensions<NHibernatePersistence> persistenceConfiguration, Configuration configuration)
        {
            persistenceConfiguration.GetSettings().Set("StorageConfiguration", configuration);
            return persistenceConfiguration;
        }

        /// <summary>
        /// Instructs the NHibernate persistence to register the managed session available via NHibernateStorageSession in the container.
        /// </summary>
        /// <param name="persistenceConfiguration"></param>
        /// <returns></returns>
        [ObsoleteEx(
            RemoveInVersion = "8",
            TreatAsErrorFromVersion = "7",
            ReplacementTypeOrMember = "IMessageHandlerContext.SynchronizedStorageSession.Session")]
        public static PersistenceExtensions<NHibernatePersistence> RegisterManagedSessionInTheContainer(this PersistenceExtensions<NHibernatePersistence> persistenceConfiguration)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Instructs the NHibernate persistence to use a custom session creation method. The provided method takes the ISessionFactory and the connection string and returns a session.
        /// </summary>
        /// <param name="persistenceConfiguration"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        [ObsoleteEx(
            RemoveInVersion = "8",
            TreatAsErrorFromVersion = "7",
            Message = "Custom session creation is no longer supported. Entity mapping can be done through providing custom NHibernate Configuration object on endpoint initialization.")]
        public static PersistenceExtensions<NHibernatePersistence> UseCustomSessionCreationMethod(this PersistenceExtensions<NHibernatePersistence> persistenceConfiguration, Func<ISessionFactory, string, ISession> callback)
        {
            throw new NotImplementedException();
        }
    }
}
