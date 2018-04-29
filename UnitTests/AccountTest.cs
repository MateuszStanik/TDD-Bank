using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TDDLab;
using DomainModel;
using System.Collections.Generic;
using NSubstitute;
using System.Linq;
using System.Data.Entity;
using UnitOfWork;

namespace UnitTests
{
    [TestClass]
    public class AccountTest
    {
        private AccountOperations accountOperations;
        IQueryable<TransactionLogs> transaction;
        DbSet<TransactionLogs> mockSetTransaction;
        IQueryable<Account> account;
        DbSet<Account> mockSetAccount;
        IQueryable<User> users;
        DbSet<User> mockSetUser;
        IEFDbContext db;

        [TestCleanup]
        public void cleanUpTest()
        {
            transaction = null;
            accountOperations = null;
            mockSetTransaction = null;
        }

        [TestInitialize]
        [Owner("Mateusz Stanik")]
        [Description("Creating EF context with basic mocked tables")]
        public void InitUserObject()
        {
            transaction = new List<TransactionLogs>() {
                new TransactionLogs() {
                    Amount = 200,
                    IsIncome = true,
                    UserId = 1
                },
                new TransactionLogs() {
                    Amount = 100,
                    IsIncome = false,
                    UserId = 2
                }
            }.AsQueryable();
            account = new List<Account>() {
                new Account() {
                    NRB = 909392000000018909,                    
                    Ammount = 100,
                    AccountId = 1,
                    UserId = 1
                },
                new Account() {
                    NRB = 890989000000029834,
                    Ammount = 10,
                    AccountId = 1,
                    UserId = 2
                }
            }.AsQueryable();
            users = new List<User>() {
                new User() {
                    Login = "Ola",
                    Password = "123",
                    UserId = 1
                },
                new User() {
                    Login = "Krzysiek",
                    Password = "666",
                    UserId = 2
                }
            }.AsQueryable();
            mockSetUser = Substitute.For<DbSet<User>, IQueryable<User>>();
            ((IQueryable<User>)mockSetUser).Provider.Returns(users.Provider);
            ((IQueryable<User>)mockSetUser).Expression.Returns(users.Expression);
            ((IQueryable<User>)mockSetUser).ElementType.Returns(users.ElementType);
            ((IQueryable<User>)mockSetUser).GetEnumerator().Returns(users.GetEnumerator());
            mockSetAccount = Substitute.For<DbSet<Account>, IQueryable<Account>>();
            ((IQueryable<Account>)mockSetAccount).Provider.Returns(account.Provider);
            ((IQueryable<Account>)mockSetAccount).Expression.Returns(account.Expression);
            ((IQueryable<Account>)mockSetAccount).ElementType.Returns(account.ElementType);
            ((IQueryable<Account>)mockSetAccount).GetEnumerator().Returns(account.GetEnumerator());
            mockSetTransaction = Substitute.For<DbSet<TransactionLogs>, IQueryable<TransactionLogs>>();
            ((IQueryable<TransactionLogs>)mockSetTransaction).Provider.Returns(transaction.Provider);
            ((IQueryable<TransactionLogs>)mockSetTransaction).Expression.Returns(transaction.Expression);
            ((IQueryable<TransactionLogs>)mockSetTransaction).ElementType.Returns(transaction.ElementType);

            db = Substitute.For<IEFDbContext>();
            db.accounts.Returns(mockSetAccount);
            db.transactionLogs.Returns(mockSetTransaction);
            db.users.Returns(mockSetUser);
            accountOperations = new AccountOperations(db);
        }


        [TestMethod]
        public void AddAmountValeToAccount_IsCorrectType()
        {
            var amount = accountOperations.AddAmountToAccount(23, 909392000000018909);

            Assert.IsInstanceOfType(amount, typeof(long));

        }

        [TestMethod]
        public void AddAmountValeToAccount_IsCorrectValue()
        {
            var amount = accountOperations.AddAmountToAccount(23, 909392000000018909);

            Assert.AreEqual(23,amount);

        }
        [TestMethod]
        public void RemoveAmountFromAccount_IsCorrectType()
        {
            var amount = accountOperations.GetAmountFromAccount(23, 909392000000018909);

            Assert.IsInstanceOfType(amount, typeof(long));

        }

        [TestMethod]
        public void RemoveAmountFromAccount_IsCorrectValue()
        {
            var amount = accountOperations.GetAmountFromAccount(23, 909392000000018909);

            Assert.AreEqual(23, amount);

        }
        [TestMethod]
        public void TransferMoney_IsCorrectType()
        {
            var amount = accountOperations.TransferMoney(20, 909392000000018909, 890989000000029834);

            Assert.IsInstanceOfType(amount, typeof(long));

        }

        [TestMethod]
        public void TransferMoney_IsCorrectValue()
        {
            var amount = accountOperations.TransferMoney(20, 909392000000018909, 890989000000029834);

            Assert.AreEqual(20, amount);

        }
        [TestMethod]
        public void TransferMoney_IsNotCorrectValue()
        {
            var amount = accountOperations.TransferMoney(20, 909392000000018909, 890989000000029834);

            Assert.AreNotEqual(2, amount);

        }
        [TestMethod]
        public void CheckBalance_IsNotNull()
        {
            var resp = accountOperations.CheckBalance(909392000000018909);

            Assert.IsNotNull(resp);

        }
        [TestMethod]
        public void CheckBalance_RepIsEqual()
        {
            long resp = accountOperations.CheckBalance(909392000000018909);

            Assert.AreEqual(100, resp);

        }

        [TestMethod]
        public void RaportAccounts_IsNotNull()
        {
            List<Account> resp = accountOperations.RaportAccounts();

            Assert.IsInstanceOfType(resp, typeof(List<Account>));

        }
        [TestMethod]
        public void RaportAccounts_IsCorrectAmountOfAccounts()
        {
            List<Account> resp = accountOperations.RaportAccounts();

            Assert.AreEqual(2, resp.Count);

        }
    }
}
