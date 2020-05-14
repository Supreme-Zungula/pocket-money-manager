
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Domain.Services;
using backend_api.Models;
using Data;
using System;

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
    public ActionResult<List<TransactionModel>> Get()
    {
      var transctions = _transactionService.GetTransactions();
      List<TransactionModel> transactionsList = new List<TransactionModel>();

      foreach (var tran in transctions)
      {

        transactionsList.Add(TransactionModel.FromDomain(tran));
      }
      return transactionsList;
    }

    // GET: api/Transaction/5
    [HttpGet("{id:length(24)}", Name = "GetTransaction")]
    public ActionResult<TransactionModel> Get(string id)
    {
      var trans = _transactionService.GetTransaction(id);

      if (trans == null)
      {
        return NotFound();
      }

      return TransactionModel.FromDomain(trans);
    }

    // POST: api/Transaction
    [HttpPost]
    public ActionResult<TransactionModel> Post([FromBody] string requestBody)
    {
        TransactionModel transactionIn = new TransactionModel();
        System.Console.WriteLine("TransactionIN: " + transactionIn.ToString());
        _transactionService.AddTransaction(transactionIn.ToDomain());
        return CreatedAtRoute("GetTransaction", new { id = transactionIn.Id.ToString() }, transactionIn);
    }

    // PUT: api/Transaction/5
    [HttpPut("{id}")]
    public void Put(string id, TransactionData transactionIn)
    {
        var request = Request.Body;
        System.Console.WriteLine(Request.Body.ToString());
        TransactionModel transactionModel = new TransactionModel
        {
            Id = transactionIn.Id,
            Deposit = transactionIn.Deposit,
            Withdrawal = transactionIn.Withdrawal,
            Reference = transactionIn.Reference,
            Date = DateTime.Now
        };

        _transactionService.UpdateTransaction(id, transactionModel.ToDomain());
    }

    // PUT: api/Transaction/
    public void Put()
    {
        var body = Request.Body;
        TransactionModel transactionIn = new TransactionModel();
            
        _transactionService.UpdateTransaction(transactionIn.Id, transactionIn.ToDomain());
    }
    // DELETE: api/ApiWithActions/5
    [HttpDelete("{id}")]
    public void Delete(string id)
    {
      _transactionService.RemoveTransaction(id);
    }

    // DELETE: api/transaction/5
    [HttpDelete]
    public void Delete(TransactionModel transaction)
    {
      _transactionService.RemoveTransaction(transaction.ToDomain());
    }
  }
}
