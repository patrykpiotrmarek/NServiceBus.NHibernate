namespace NServiceBus_6
{
    using System;
    using System.Threading.Tasks;
    using global::NHibernate;
    using NServiceBus_6.Persistence;

    interface INHibernateSynchronizedStorageSession
    {
        ISession Session { get; }

        void OnSaveChanges(Func<SynchronizedStorageSession, Task> callback);
    }
}