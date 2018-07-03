namespace NServiceBus_6.NHibernate.Tests.Outbox
{
    using System.Data;
    using NServiceBus_6.Outbox;

    class FakeDbConnectionProvider : IDbConnectionProvider
    {
        readonly IDbConnection dbConnection;

        public FakeDbConnectionProvider(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        public bool TryGetConnection(out IDbConnection connection, string connectionString)
        {
            connection = dbConnection;

            return connection != null;
        }
    }
}