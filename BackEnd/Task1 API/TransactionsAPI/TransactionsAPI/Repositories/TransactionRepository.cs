namespace TransactionsAPI.Repositories;

using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.Sqlite;
using TransactionsAPI.Models;
public class TransactionRepository
{
    private readonly string _connectionString;

    public TransactionRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    private IDbConnection Connection => new SqliteConnection(_connectionString);

    public async Task<IEnumerable<Transaction>> GetAllTransactionsAsync()
    {
        using var db = Connection;
        return await db.QueryAsync<Transaction>("SELECT * FROM Transactions");
    }

    public async Task<Transaction> GetTransactionByIdAsync(int id)
    {
        using var db = Connection;
        return await db.QueryFirstOrDefaultAsync<Transaction>(
            "SELECT * FROM Transactions WHERE TransactionID = @Id", new { Id = id });
    }

    public async Task<int> AddTransactionAsync(Transaction transaction)
    {
        using var db = Connection;
        return await db.ExecuteAsync(
            @"INSERT INTO Transactions (AccountID, TransactionAmount, TransactionCurrencyCode, LocalHour, TransactionScenario,
                                        TransactionType, TransactionIPaddress, IpState, IpPostalCode, IpCountry, IsProxyIP, 
                                        BrowserLanguage, PaymentInstrumentType, CardType, PaymentBillingPostalCode, PaymentBillingState, 
                                        PaymentBillingCountryCode, ShippingPostalCode, ShippingState, ShippingCountry, 
                                        CvvVerifyResult, DigitalItemCount, PhysicalItemCount, TransactionDateTime)
              VALUES (@AccountID, @TransactionAmount, @TransactionCurrencyCode, @LocalHour, @TransactionScenario, 
                      @TransactionType, @TransactionIPaddress, @IpState, @IpPostalCode, @IpCountry, @IsProxyIP, 
                      @BrowserLanguage, @PaymentInstrumentType, @CardType, @PaymentBillingPostalCode, @PaymentBillingState, 
                      @PaymentBillingCountryCode, @ShippingPostalCode, @ShippingState, @ShippingCountry, 
                      @CvvVerifyResult, @DigitalItemCount, @PhysicalItemCount, @TransactionDateTime)", transaction);
    }

    public async Task<int> UpdateTransactionAsync(Transaction transaction)
    {
        using var db = Connection;
        return await db.ExecuteAsync(
            @"UPDATE Transactions SET AccountID = @AccountID, TransactionAmount = @TransactionAmount, 
                                     TransactionCurrencyCode = @TransactionCurrencyCode, LocalHour = @LocalHour, 
                                     TransactionScenario = @TransactionScenario, TransactionType = @TransactionType, 
                                     TransactionIPaddress = @TransactionIPaddress, IpState = @IpState, IpPostalCode = @IpPostalCode, 
                                     IpCountry = @IpCountry, IsProxyIP = @IsProxyIP, BrowserLanguage = @BrowserLanguage, 
                                     PaymentInstrumentType = @PaymentInstrumentType, CardType = @CardType, 
                                     PaymentBillingPostalCode = @PaymentBillingPostalCode, PaymentBillingState = @PaymentBillingState, 
                                     PaymentBillingCountryCode = @PaymentBillingCountryCode, ShippingPostalCode = @ShippingPostalCode, 
                                     ShippingState = @ShippingState, ShippingCountry = @ShippingCountry, 
                                     CvvVerifyResult = @CvvVerifyResult, DigitalItemCount = @DigitalItemCount, 
                                     PhysicalItemCount = @PhysicalItemCount, TransactionDateTime = @TransactionDateTime 
              WHERE TransactionID = @TransactionID", transaction);
    }

    public async Task<int> DeleteTransactionAsync(int id)
    {
        using var db = Connection;
        return await db.ExecuteAsync("DELETE FROM Transactions WHERE TransactionID = @Id", new { Id = id });
    }
}