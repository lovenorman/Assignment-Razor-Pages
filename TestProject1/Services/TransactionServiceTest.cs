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
            var acc = new Account { Id = 1, Balance = 200, AccountType = "Personal" };
            testContext.Accounts.Add(acc);
            testContext.SaveChanges();
            var result = _sut.Withdraw(1, 300);
            Assert.AreEqual(IAccountService.ErrorCode.BalanceIsTooLow, result);
        }

        [TestMethod]
        public void When_transfer_negative_amount_return__AmountIsNegative()
        {
            var result = _sut.Transfer(1, 2, -3);
            Assert.AreEqual(IAccountService.ErrorCode.AmountIsNegative, result);
        }

    }
}
