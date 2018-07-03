namespace NServiceBus_6.Persistence.NHibernate
{
    using System.Threading.Tasks;
    using global::NHibernate;
    using NServiceBus_6.Extensibility;
    using NServiceBus_6.Persistence;

    class NHibernateSynchronizedStorage : ISynchronizedStorage
    {
        ISessionFactory sessionFactory;

        public NHibernateSynchronizedStorage(ISessionFactory sessionFactory)
        {
            this.sessionFactory = sessionFactory;
        }

        public Task<CompletableSynchronizedStorageSession> OpenSession(ContextBag contextBag)
        {
            CompletableSynchronizedStorageSession result = new NHibernateLazyNativeTransactionSynchronizedStorageSession(() => sessionFactory.OpenSession());
            return Task.FromResult(result);
        }
    }
}