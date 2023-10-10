using MySql.Data.MySqlClient;
using System;

namespace product_search.api.Config
{
    public class ConnectionStringBuilder
    {
        public static string GetConnectionString()
        {
            var UserID = Environment.GetEnvironmentVariable("DB_USER");
            var Password = Environment.GetEnvironmentVariable("DB_PASS");
            var Database = Environment.GetEnvironmentVariable("DB_NAME");
            var ServerIp = Environment.GetEnvironmentVariable("DB_SERVER");
            if (Environment.GetEnvironmentVariable("UseIpSqlConnection") == "true")
            {
                var template = "Server={0};Database={1};Uid={2};Password={3}";
                return string.Format(template, ServerIp, Database, UserID, Password);
            }
            else
                return GetUnixConnectionString();
        }

        private static string GetUnixConnectionString()
        {
            // Equivalent connection string:
            // "Server=<dbSocketDir>/<INSTANCE_CONNECTION_NAME>;Uid=<DB_USER>;Pwd=<DB_PASS>;Database=<DB_NAME>;Protocol=unix"
            String dbSocketDir = Environment.GetEnvironmentVariable("DB_SOCKET_PATH") ?? "/cloudsql";
            String instanceConnectionName = Environment.GetEnvironmentVariable("INSTANCE_CONNECTION_NAME");
            var connectionString = new MySqlConnectionStringBuilder()
            {
                // The Cloud SQL proxy provides encryption between the proxy and instance.
                SslMode = MySqlSslMode.None,
                // Remember - storing secrets in plaintext is potentially unsafe. Consider using
                // something like https://cloud.google.com/secret-manager/docs/overview to help keep
                // secrets secret.
                Server = String.Format("{0}/{1}", dbSocketDir, instanceConnectionName),
                UserID = Environment.GetEnvironmentVariable("DB_USER"),   // e.g. 'my-db-user
                Password = Environment.GetEnvironmentVariable("DB_PASS"), // e.g. 'my-db-password'
                Database = Environment.GetEnvironmentVariable("DB_NAME"), // e.g. 'my-database'
                ConnectionProtocol = MySqlConnectionProtocol.UnixSocket
            };
            connectionString.Pooling = true;
            // Specify additional properties here.
            // ...

            return connectionString.ConnectionString;
        }
    }
}
