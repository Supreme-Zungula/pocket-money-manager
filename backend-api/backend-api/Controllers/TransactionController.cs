
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Domain.Services;
using backend_api.Models;
using Data;
using Domain.DefinitionObjects;

namespace backend_api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class TransactionController : ControllerBase
  {
    private readonly TransactionService _transactionService;

    public TransactionController()
    {
      this._transactionService = new TransactionService();
    }

    // GET: api/Transaction
    [HttpGet]
    public ActionResult<List<Transaction>> Get()
    {
      return _transactionService.GetTransactions();
    }

    // GET: api/Transaction/5
    [HttpGet("{id:length(24)}", Name = "GetTransaction")]
    public ActionResult<Transaction> Get(string id)
    {
      var trans = _transactionService.GetTransaction(id);

      if (trans == null)
      {
        return NotFound();
      }

      return trans;
    }

    // POST: api/Transaction
    [HttpPost]
    public ActionResult<TransactionModel> Post(Transaction transactionIn)
    {

      _transactionService.AddTransaction(transactionIn);
      return CreatedAtRoute("GetTransaction", new { id = transactionIn.Id.ToString() }, transactionIn);
    }

    // PUT: api/Transaction/5
    [HttpPut("{id}")]
    public IActionResult Put(string id, Transaction transactionIn)
    {
      var updatedTransaction = _transactionService.UpdateTransaction(id, transactionIn);
      if (updatedTransaction == null)
      {
        return NotFound();
      }
      return NoContent();
    }

    // DELETE: api/ApiWithActions/5
    [HttpDelete("{id}")]
    public IActionResult Delete(string id)
    {
      var deletedTransactionId = _transactionService.RemoveTransaction(id);
      if (deletedTransactionId == null)
      {
        return NotFound();
      }

      return NoContent();
    }

    // DELETE: api/transaction/
    [HttpDelete]
    public IActionResult Delete(Transaction transactionIn)
    {
      var deletedTrans = _transactionService.RemoveTransaction(transactionIn);
      if (deletedTrans == null)
      {
        return NotFound();
      }

      return NoContent();
    }
  }
}
