using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    [Table("TransactionHistory")]
    public class TransactionLogs
    {
        [Key]
        public int TransactionLog { get; set; }
        public int UserId { get; set; }
        public string Comment { get; set; }
        public decimal Amount { get; set; }
        public bool IsIncome { get; set; }
    }
}
