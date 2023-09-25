using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cSharp_LibrarySystemEF.Model
{
    public class BorrowingTransaction
    {
        [Key]
        public int BorrowingTransactionId { get; set; }
        [ForeignKey("Patron")]
        public int PatronId { get; set; }
        [ForeignKey("Book")]
        public int BookId { get; set; }
        [Required]
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public Patron Patron { get; set; }
        public Book Book { get; set; }
    }
}
