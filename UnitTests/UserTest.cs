using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDDLab;

namespace UnitTests
{
    class UserTest
    {       
        private UserOperations userOperations;


        [TestInitialize]
        public void InitAccountObject()
        {
            userOperations = new UserOperations();
        }

        [TestMethod]
        public void AccountLoingIsTrue()
        {
            var isLogged = userOperations.Login("Syntaxerror", "Maticzek");

            Assert.IsFalse(isLogged);
        }
    }
}
