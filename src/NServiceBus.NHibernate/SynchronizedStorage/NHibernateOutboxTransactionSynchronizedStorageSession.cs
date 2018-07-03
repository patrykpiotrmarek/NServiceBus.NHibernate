namespace NServiceBus_6.Persistence.NHibernate
{
    using System;
    using System.Threading.Tasks;
    using global::NHibernate;
    using Janitor;
    using NServiceBus_6.Outbox.NHibernate;
    using NServiceBus_6.Persistence;

    [SkipWeaving]
    class NHibernateOutboxTransactionSynchronizedStorageSession : CompletableSynchronizedStorageSession, INHibernateSynchronizedStorageSession
    {
        NHibernateOutboxTransaction outboxTransaction;

        public NHibernateOutboxTransactionSynchronizedStorageSession(NHibernateOutboxTransaction outboxTransaction)
        {
            this.outboxTransaction = outboxTransaction;
        }

        public ISession Session => outboxTransaction.Session;
        public void OnSaveChanges(Func<SynchronizedStorageSession, Task> callback)
        {
            outboxTransaction.OnSaveChanges(() => callback(this));
        }

        public ITransaction Transaction => outboxTransaction.Transaction;

        public Task CompleteAsync()
        {
            return Task.FromResult(0);
        }

        public void Dispose()
        {
        }
    }
}