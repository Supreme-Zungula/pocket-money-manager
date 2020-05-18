using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Domain.Services;
using backend_api.Models;
using Data;
using Domain.DefinitionObjects;
namespace backend_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankAccountController : ControllerBase
    {
        private readonly BankAccountService  _bankAccountService;
        public BankAccountController()
        {
            _bankAccountService = new BankAccountService();
        }
        // GET: api/BankAccount
        [HttpGet]
        public IEnumerable<BankAccountData> GetAccounts()
        {
            return _bankAccountService.GetAccounts();
        }

        // GET: api/BankAccount/5
        [HttpGet("{id}", Name = "Get")]
        public BankAccountModel Get(Guid id)
        {
            BankAccount bankAccount = _bankAccountService.GetAccount(id);
            return BankAccountModel.FromDomain(bankAccount);
        }

        // POST: api/BankAccount
        [HttpPost]
        public BankAccountData Post(BankAccountModel accountModel)
        {
           BankAccount newAccount =  _bankAccountService.CreateAccount(accountModel.ToDomain());
            return new BankAccountData
            {
                Id = newAccount.Id,
                AccountNo = newAccount.AccountNo.ToString(),
                Balance = newAccount.Balance,
                UserReference = newAccount.CustomerRef
            };
        }

        // PUT: api/BankAccount/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
