using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    [Table("Accounts")]
    public class Account
    {
        [Key]
        public int AccountId { get; set; }        
        public int UserId { get; set; }
        public long NRB { get; set; }
        public long Ammount { get; set; }
        public virtual User user {get;set;}
    }
}
