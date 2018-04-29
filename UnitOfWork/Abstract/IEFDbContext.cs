using DomainModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWork
{
    public interface IEFDbContext : IDisposable
    {
        DbSet<Account> accounts { get; set; }
        DbSet<TransactionLogs> transactionLogs { get; set; }
        DbSet<User> users { get; set; }
        int SaveChanges();
    }
}

