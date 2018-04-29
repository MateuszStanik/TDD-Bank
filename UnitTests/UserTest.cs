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
    public class UserTest
    {       
        private UserOperations userOperations;
        IQueryable<User> users;
        DbSet<User> mockSetUser;
        IEFDbContext db;

        [TestCleanup]
        public void cleanUpTest()
        {
            userOperations = null;
            users = null;
            mockSetUser = null;
        }

        [TestInitialize]
        [Owner("Mateusz Stanik")]
        [Description("Creating EF context with basic mocked tables")]
        public void InitUserObject()
        {
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

            db = Substitute.For<IEFDbContext>();
            db.users.Returns(mockSetUser);

            userOperations = new UserOperations(db);
        }

        [TestMethod]
        [Owner("Mateusz Stanik")]
        public void UserLoingFirstUser_IsAnyResponse()
        {
            bool isLogged = userOperations.Login("Ola", "123");
            Assert.IsNotNull(isLogged);
        }

        [TestMethod]
        [Owner("Mateusz Stanik")]
        public void UserLoingFirstUser_IsTrue()
        {
            bool isLogged = userOperations.Login("Ola", "123");
            Assert.IsFalse(!isLogged);
        }

        [TestMethod]
        [Owner("Mateusz Stanik")]
        public void UserLoingSecondUser_IsAnyResponse()
        {
            bool isLogged = userOperations.Login("Krzysiek", "666");
            Assert.IsNotNull(isLogged);
        }

        [TestMethod]
        [Owner("Mateusz Stanik")]
        public void UserLoingSecondUser_IsTrue()
        {
            bool isLogged = userOperations.Login("Krzysiek", "666");
            Assert.IsNotNull(isLogged);
        }
    }
}
