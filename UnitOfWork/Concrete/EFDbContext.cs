using DomainModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWork
{
    public class EFDbContext : DbContext, IEFDbContext
    {
        public EFDbContext() : base("name=TDDProject")
        {

        }

        public DbSet<Account> accounts { get; set; }
        public DbSet<TransactionLogs> transactionLogs { get; set; }
        public DbSet<User> users { get; set; }
        public override int SaveChanges()
        {

            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var exMessage = ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Aggregate(String.Empty, (current, error) => current + String.Format("{0}: {1}\n", error.PropertyName, error.ErrorMessage));
                throw new DbEntityValidationException(exMessage, ex.EntityValidationErrors);
            }

        }
    }
}
