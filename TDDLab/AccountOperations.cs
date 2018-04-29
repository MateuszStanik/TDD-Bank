using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork;

namespace TDDLab
{
    public class AccountOperations
    {
        public IEFDbContext _db;
        public AccountOperations(IEFDbContext db)
        {
            _db = db;
        }
        

        public long TransferMoney(long amount, long FromNRB, long ToNRB)
        {
            try
            {
                Account sourceAccount = _db.accounts.Where(x=>x.NRB == FromNRB).FirstOrDefault();
                Account targetAccount = _db.accounts.Where(x => x.NRB == ToNRB).FirstOrDefault();
                sourceAccount.Ammount -= amount;
                targetAccount.Ammount += amount;
                _db.SaveChanges();
                var operation = new TransactionOperation(_db);
                operation.SaveTransaction(sourceAccount.UserId, amount, false);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return amount;
        } 

        public long GetAmountFromAccount(long amount, long NRB)
        {
            try
            {
                Account sourceAccount = _db.accounts.Where(x => x.NRB == NRB).FirstOrDefault();
                sourceAccount.Ammount -= amount;
                _db.SaveChanges();
                var operation = new TransactionOperation(_db);
                operation.SaveTransaction(sourceAccount.UserId, amount, false);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return amount;
        }

        public long AddAmountToAccount(long amount, long NRB)
        {
            try
            {
                Account sourceAccount = _db.accounts.Where(x => x.NRB == NRB).FirstOrDefault();
                sourceAccount.Ammount += amount;
                _db.SaveChanges();
                var operation = new TransactionOperation(_db);
                operation.SaveTransaction(sourceAccount.UserId, amount, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return amount;
        }

        public long CheckBalance(long NRB)
        {
            try
            {
                Account sourceAccount = _db.accounts.Where(x => x.NRB == NRB).FirstOrDefault();
                return sourceAccount.Ammount;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return 0;
            }            
        }

        public List<Account> RaportAccounts()
        {
            try
            {
                List<Account> sourceAccount = _db.accounts.ToList();

                return sourceAccount;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
    }
}
