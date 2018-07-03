namespace NServiceBus_6.NHibernate.Outbox
{
    using global::NHibernate;
    using NServiceBus_6.Outbox.NHibernate;
    using NServiceBus_6.Persistence.NHibernate;

    interface IOutboxPersisterFactory
    {
        INHibernateOutboxStorage Create(ISessionFactory sessionFactory, string endpointName);
    }

    class OutboxPersisterFactory<T> : IOutboxPersisterFactory
        where T : class, IOutboxRecord, new()
    {
        public INHibernateOutboxStorage Create(ISessionFactory sessionFactory, string endpointName)
        {
            var persister = new OutboxPersister<T>(sessionFactory, endpointName);
            return persister;
        }
    }
}