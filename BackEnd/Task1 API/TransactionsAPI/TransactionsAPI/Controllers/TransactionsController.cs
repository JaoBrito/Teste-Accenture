using TransactionsAPI.Models;
using TransactionsAPI.Repositories;

namespace TransactionsAPI.Controllers;

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class TransactionsController : ControllerBase
{
    private readonly TransactionRepository _repository;

    public TransactionsController(TransactionRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Transaction>>> GetAll()
    {
        return Ok(await _repository.GetAllTransactionsAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Transaction>> Get(int id)
    {
        var transaction = await _repository.GetTransactionByIdAsync(id);
        if (transaction == null) return NotFound();
        return Ok(transaction);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] Transaction transaction)
    {
        await _repository.AddTransactionAsync(transaction);
        return CreatedAtAction(nameof(Get), new { id = transaction.TransactionID }, transaction);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(string id, [FromBody] Transaction transaction)
    {
        transaction.TransactionID = id;
        await _repository.UpdateTransactionAsync(transaction);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _repository.DeleteTransactionAsync(id);
        return NoContent();
    }
}