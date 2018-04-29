using DomainModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDDLab;
using UnitOfWork;

namespace UnitTests
{
    [TestClass]
    public class TransactionTest
    {

        private TransactionOperation transactionOperations;
        IQueryable<TransactionLogs> transaction;
        DbSet<TransactionLogs> mockSetTransaction;
        IQueryable<User> users;
        DbSet<User> mockSetUser;
        IEFDbContext db;

        [TestCleanup]
        public void cleanUpTest()
        {
            transaction = null;
            transactionOperations = null;
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
            mockSetTransaction = Substitute.For<DbSet<TransactionLogs>, IQueryable<TransactionLogs>>();
            ((IQueryable<TransactionLogs>)mockSetTransaction).Provider.Returns(transaction.Provider);
            ((IQueryable<TransactionLogs>)mockSetTransaction).Expression.Returns(transaction.Expression);
            ((IQueryable<TransactionLogs>)mockSetTransaction).ElementType.Returns(transaction.ElementType);

            db = Substitute.For<IEFDbContext>();
            db.transactionLogs.Returns(mockSetTransaction);
            db.users.Returns(mockSetUser);
            transactionOperations = new TransactionOperation(db);
        }

        [TestMethod]
        [Owner("Mateusz Stanik")]
        public void GetTransactionForUser_IsAnyResponse()
        {
            List<TransactionLogs> list = transactionOperations.GetTransactionForUser("Ola");
            Assert.IsNotNull(list);
        }


        [TestMethod]
        [Owner("Mateusz Stanik")]
        public void GetTransactionForUser_IsListWitOneRow()
        {
            List<TransactionLogs> list = transactionOperations.GetTransactionForUser("Ola");
            Assert.AreEqual(1, list.Count);
        }

        [TestMethod]
        [Owner("Mateusz Stanik")]
        public void SaveTransavtionTest_returnTrue()
        {
            var isSaved = transactionOperations.SaveTransaction(1, 30, true);
            Assert.AreEqual(true, isSaved);
        }

    }
}
