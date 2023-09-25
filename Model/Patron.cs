using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cSharp_LibrarySystemEF.Model
{
    public class Patron
    {
        [Key]
        public int PatronId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string PhoneNum { get; set; }
        public List<BorrowingTransaction> BorrowingTransactions { get; set; }
    }
}
