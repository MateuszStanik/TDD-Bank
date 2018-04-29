using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork;

namespace TDDLab
{
    public class UserOperations
    {
        IEFDbContext _db;
        public UserOperations(IEFDbContext db)
        {
            _db = db;
        }

        public bool Login(string login, string password) {
            
            bool isLogged = false;

            try
            {
                var user = _db.users.Where(x => x.Login == login && x.Password == password).FirstOrDefault();
                if(user.Login != null && user.Password != null)
                {
                    isLogged = true;
                    Console.WriteLine("Zalogowano");
                }                
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return isLogged;
        }


    }
}
