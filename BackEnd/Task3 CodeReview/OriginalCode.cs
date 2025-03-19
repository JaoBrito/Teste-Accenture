public class TransactionService
{
    private readonly List<Transaction> Transactions = new List<Transaction>();

    public List<Transaction> GetTransactionByIdList(List<string> idList)
    {
        foreach (var transaction in Transactions)
        {
            foreach (var id in idList)
            {
                if (transaction.Id == id)
                {
                    return transaction;
                }
            }
        }
        return null;
    }

    public Transaction GetTransactionById(string id)
    {
        foreach (var transaction in Transactions)
        {
            if (transaction.Id == id)
            {
                return transaction;
            }
        }
        return null;
    }

    public void AddTransaction(Transaction transaction)
    {
        Transactions.Add(transaction);
    }

    public void RemoveTransaction(string id)
    {
        var transaction = GetTransactionById(id);
        if (transaction != null)
        {
            Transactions.Remove(transaction);
        }
    }
}