using BankStartWeb.Data;
using BankStartWeb.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1.Services
{

    [TestClass]
    public class TransactionServiceTest
    {
        private AccountService _sut;//System Under Test
        private ApplicationDbContext testContext;

        public TransactionServiceTest()
        {
            //Gör ett anrop till temp. databas i ramminne
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Test")
                .Options;
            testContext = new ApplicationDbContext(options);
            _sut = new AccountService( testContext);
        }
        
        [TestMethod]
        public void When_deposit_negative_amount_return_AmountIsNegative()
        {
            var result = _sut.Deposit(1, -3);
            Assert.AreEqual(IAccountService.ErrorCode.AmountIsNegative, result);
        }

        [TestMethod]
        public void When_withdraw_negative_amount_return_AmountIsNegative()
        {
            var result = _sut.Withdraw(1, -3);
            Assert.AreEqual(IAccountService.ErrorCode.AmountIsNegative, result);
        }

        [TestMethod]
        public void When_withdraw_overtrass_return_BalanceIsTooLow()
        {
            
            var acc = new Account { Id = 5, Balance = 200, AccountType = "Personal" };
            testContext.Accounts.Add(acc);
            testContext.SaveChanges();
            var result = _sut.Withdraw(5, 300);
            Assert.AreEqual(IAccountService.ErrorCode.BalanceIsTooLow, result);
        }

        [TestMethod]
        public void When_transfer_negative_amount_return_AmountIsNegative()
        {
            var result = _sut.Transfer(1, 2, -3);
            Assert.AreEqual(IAccountService.ErrorCode.AmountIsNegative, result);
        }

        [TestMethod]

        public void When_transfer_overtrass_return_BalanceIsTooLow()
        {
            var arrange1 = new Account
            {
                Id = 1,
                Balance = 200,
                AccountType = "Personal"
            };
            
            var arrange2 = new Account 
            { 
                Id = 2, 
                Balance = 200, 
                AccountType = "Personal" 
            };

            testContext.Accounts.Add(arrange1);
            testContext.Accounts.Add(arrange2);
            testContext.SaveChanges();
            var result = _sut.Transfer(1, 2, 300);
            Assert.AreEqual(IAccountService.ErrorCode.BalanceIsTooLow, result);
        }

        [TestMethod]
        public void When_deposit_make_new_transaction()
        {
            //Skapar ett fakekonto
            var arrAccount = new Account
            {
                Id = 3,
                Balance = 200,
                AccountType = "Personal"
            };
            //lägger till kontot till fake DB
            testContext.Accounts.Add(arrAccount);
            testContext.SaveChanges();

            //Kör deposit, sätter in 100 på konto 3
            _sut.Deposit(3, 100);

            //Jämför första transactions amount med 100
            Assert.AreEqual(100, arrAccount.Transactions.First().Amount);
        }
    }
}
