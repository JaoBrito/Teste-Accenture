using TransactionsAPI.Models;
namespace TransactionsAPI.Data;

using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.Sqlite;

public static class CsvImporter
{
    public static async Task ImportTransactionsFromCsv(string filePath, string connectionString)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine("Arquivo Sales.txt não encontrado.");
            return;
        }

        using var connection = new SqliteConnection(connectionString);
        connection.Open();

        var transactions = new List<Transaction>();

        using (var reader = new StreamReader(filePath))
        {
            string headerLine = reader.ReadLine(); // Ignora o cabeçalho
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');

                var transaction = new Transaction
                {
                    AccountID = values[0], // Pode ser string
                    TransactionID = values[1],
                    TransactionAmount = decimal.TryParse(values[2], out decimal amount) ? amount : 0,
                    TransactionCurrencyCode = values[3], // Mantenha como string
                    TransactionScenario = values[4],
                    TransactionType = values[5],
                    TransactionIPaddress = values[6],
                    IpState = values[7],
                    IpPostalCode = values[8],
                    IpCountry = values[9], // Se for string, manter assim
                    IsProxyIP = values[10],
                    BrowserLanguage = values[11],
                    PaymentInstrumentType = values[12],
                    CardType = values[13],
                    PaymentBillingPostalCode = values[14],
                    PaymentBillingState = values[15],
                    PaymentBillingCountryCode = values[16], // Se for string, manter assim
                    ShippingPostalCode = values[17],
                    ShippingState = values[18],
                    ShippingCountry = values[19],
                    CvvVerifyResult = values[20],
                    DigitalItemCount = int.TryParse(values[21], out int digitalItems) ? digitalItems : 0, // Se erro estiver aqui, mantém 0
                    PhysicalItemCount = int.TryParse(values[22], out int physicalItems) ? physicalItems : 0, // Se erro estiver aqui, mantém 0
                    TransactionDateTime = DateTime.TryParse(values[23], out DateTime date) ? date : DateTime.MinValue // Se erro, mantém data mínima
                };

                transactions.Add(transaction);
            }
        }

        string insertQuery = @"
            INSERT INTO Transactions (AccountID, TransactionAmount, TransactionCurrencyCode, LocalHour, TransactionScenario,
                                      TransactionType, TransactionIPaddress, IpState, IpPostalCode, IpCountry, IsProxyIP, 
                                      BrowserLanguage, PaymentInstrumentType, CardType, PaymentBillingPostalCode, PaymentBillingState, 
                                      PaymentBillingCountryCode, ShippingPostalCode, ShippingState, ShippingCountry, 
                                      CvvVerifyResult, DigitalItemCount, PhysicalItemCount, TransactionDateTime)
            VALUES (@AccountID, @TransactionAmount, @TransactionCurrencyCode, @LocalHour, @TransactionScenario, 
                    @TransactionType, @TransactionIPaddress, @IpState, @IpPostalCode, @IpCountry, @IsProxyIP, 
                    @BrowserLanguage, @PaymentInstrumentType, @CardType, @PaymentBillingPostalCode, @PaymentBillingState, 
                    @PaymentBillingCountryCode, @ShippingPostalCode, @ShippingState, @ShippingCountry, 
                    @CvvVerifyResult, @DigitalItemCount, @PhysicalItemCount, @TransactionDateTime)";

        await connection.ExecuteAsync(insertQuery, transactions);
        Console.WriteLine("Importação de transações concluída.");
    }
}
