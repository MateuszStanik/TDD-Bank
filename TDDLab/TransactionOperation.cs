using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork;

namespace TDDLab
{
    public class TransactionOperation
    {
        IEFDbContext _db;
        public TransactionOperation(IEFDbContext db)
        {
            _db = db;
        }

        public List<TransactionLogs> GetTransactionForUser(string login)
        {            
            try
            {
                var user = _db.users.Where(x => x.Login == login).FirstOrDefault();
                List<TransactionLogs> transactionList = _db.transactionLogs.Where(x => x.UserId == user.UserId).ToList();
                return transactionList;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
            
        }

        public bool SaveTransaction(int userId, long amount, bool isIncome) {
            bool isSavedTransaction = false;
            try
            {
                TransactionLogs transaction = new TransactionLogs() {
                    UserId = userId,
                    Amount = amount,
                    IsIncome = isIncome                
                };
               
                var user = _db.transactionLogs.Add(transaction);
                _db.SaveChanges();
                isSavedTransaction = true;

                return isSavedTransaction;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }

        }
    }
}
