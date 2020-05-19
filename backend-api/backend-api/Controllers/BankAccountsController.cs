using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Domain.Services;
using Domain.DefinitionObjects;
using backend_api.Models;

namespace backend_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankAccountsController : ControllerBase
    {
        private readonly BankAccountService _bankAccountService = new BankAccountService();
        private readonly TransactionService _transactionService = new TransactionService();

        // GET: api/BankAccounts
        [HttpGet]
        public IEnumerable<BankAccount> GetAccounts()
        {
            return _bankAccountService.GetAccounts();
        }

        // GET: api/BankAccounts/{:accountId}
        [HttpGet("{accountId}", Name = "GetBankAccount")]
        public ActionResult<BankAccountModel> GetBankAccount(string accountId)
        {
            var account = _bankAccountService.GetAccount(accountId);
            if (account == null)
            {
                return NotFound();
            }
            return BankAccountModel.FromDomain(account);
        }

        // POST: api/BankAccounts
        [HttpPost]
        public ActionResult<BankAccountModel> Post(BankAccountModel accountIn)
        {
            var newAccount = _bankAccountService.CreateAccount(accountIn.ToDomain());
            return BankAccountModel.FromDomain(newAccount);
            //return CreatedAtRoute("BankAccounts", new { id = accountIn.Id.ToString() }, accountIn);
        }

        // PUT: api/BankAccounts/{:accountId}
        [HttpPut("{accountId}")]
        public ActionResult<BankAccountModel> Put(string accountId, BankAccountModel accountIn)
        {
            Transaction transaction = new Transaction
            {
                AccountNo = accountIn.AccountNo,
                Deposit = accountIn.Balance,
                Withdrawal = 0m,
                Reference = "Account update",
                Date = DateTime.Now
            };

            BankAccount account = new BankAccount(accountIn.AccountNo, accountIn.Balance, accountIn.CustomerRef, transaction);
            var updatedAccount = _bankAccountService.UpdateAccount(accountId, account);
            
            if (updatedAccount == null)
            {
                return NotFound();
            }
            return BankAccountModel.FromDomain(updatedAccount);
        }

        // DELETE: api/ApiWithActions/{:accountId}
        [HttpDelete("{accountId}")]
        public ActionResult<BankAccountModel> DeleteAccount(string accountId)
        {
            var deletedAccount = _bankAccountService.DeleteAccount(accountId);

            if (deletedAccount == null)
            {
                return NotFound();
            }
            return BankAccountModel.FromDomain(deletedAccount);
        }
    }
}
