public class TransactionService
{
    private readonly List<Transaction> Transactions;

    // É mais interessante inicializar essa lista apenas em casos de teste ou mudança. Inicializei ela através de um construtor
    public TransactionService()
    {
        Transactions = new List<Transaction>();
    }

    public List<Transaction> GetTransactionByIdList(List<string> idList)
    {
        // Verifica se a lista de transações é nula antes de tentar acessar
        if (Transactions == null || idList == null || idList.Count == 0)
            return new List<Transaction>(); // Retorna uma lista vazia se for nula ou a idList estiver vazia

        // O código faz uma busca usando 2 loops alinhados o que pode prejudicar a performance e aumenta a complexidade do código. Preferi uma forma mais eficiente como é feita com o HashSet
        var idSet = new HashSet<string>(idList);
        return Transactions.Where(t => idSet.Contains(t.Id)).ToList();
    }

    public Transaction GetTransactionById(string id)
    {
        // Verifica se a lista de transações é nula antes de buscar
        if (Transactions == null || string.IsNullOrEmpty(id))
            return null; // Retorna null se a lista ou o id forem nulos

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
        // Verifica se a transação é nula
        if (transaction == null)
            throw new ArgumentNullException(nameof(transaction), "Transaction cannot be null");

        Transactions.Add(transaction);
    }

    public void RemoveTransaction(string id)
    {
        // Verifica se a lista de transações é nula antes de tentar remover
        if (Transactions == null || string.IsNullOrEmpty(id))
            return; // Não faz nada se a lista ou o id forem nulos

        // Esse método chama o GetTransactionById e isso pode resultar em uma busca redundante. Usar o RemoveAll da lista é mais eficiente e mais direto
        Transactions.RemoveAll(t => t.Id == id);
    }
}
