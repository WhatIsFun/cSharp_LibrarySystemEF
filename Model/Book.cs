using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cSharp_LibrarySystemEF.Model
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public int PublicationYear { get; set; }
        [Required]
        public bool IsAvailable { get; set; }

        public List<BorrowingTransaction> BorrowingTransactions { get; set; }

    }
}
