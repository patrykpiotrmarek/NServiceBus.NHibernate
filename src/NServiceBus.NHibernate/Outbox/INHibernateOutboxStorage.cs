namespace NServiceBus_6
{
    using System;
    using NServiceBus_6.Outbox;

    interface INHibernateOutboxStorage : IOutboxStorage
    {
        void RemoveEntriesOlderThan(DateTime dateTime);
    }
}