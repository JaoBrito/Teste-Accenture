namespace TransactionsAPI.Data;

using System.Data;
using Dapper;
using Microsoft.Data.Sqlite;

public static class DatabaseInitializer
{
    public static void InitializeDatabase(string connectionString)
    {
        using var connection = new SqliteConnection(connectionString);
        connection.Open();

        string createTableQuery = @"
            CREATE TABLE IF NOT EXISTS Transactions (
                TransactionID INTEGER PRIMARY KEY AUTOINCREMENT,
                AccountID INTEGER NOT NULL,
                TransactionAmount REAL NOT NULL,
                TransactionCurrencyCode TEXT NOT NULL,
                LocalHour TEXT NOT NULL,
                TransactionScenario TEXT NOT NULL,
                TransactionType TEXT NOT NULL,
                TransactionIPaddress TEXT NOT NULL,
                IpState TEXT NOT NULL,
                IpPostalCode TEXT NOT NULL,
                IpCountry TEXT NOT NULL,
                IsProxyIP INTEGER NOT NULL,
                BrowserLanguage TEXT NOT NULL,
                PaymentInstrumentType TEXT NOT NULL,
                CardType TEXT NOT NULL,
                PaymentBillingPostalCode TEXT NOT NULL,
                PaymentBillingState TEXT NOT NULL,
                PaymentBillingCountryCode TEXT NOT NULL,
                ShippingPostalCode TEXT NOT NULL,
                ShippingState TEXT NOT NULL,
                ShippingCountry TEXT NOT NULL,
                CvvVerifyResult TEXT NOT NULL,
                DigitalItemCount INTEGER NOT NULL,
                PhysicalItemCount INTEGER NOT NULL,
                TransactionDateTime TEXT NOT NULL
            );";

        connection.Execute(createTableQuery);
    }
}
