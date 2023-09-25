using cSharp_LibrarySystemEF.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cSharp_LibrarySystemEF
{
    internal class BorrowingBook
    {
        public void BorrowingBookMenu(LibraryDbContext _context)
        {
            HomePage homePage = new HomePage();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Library Management System - Borrowing Book Menu");
                Console.WriteLine("1. Borrow Book");
                Console.WriteLine("2. Return Book");
                Console.WriteLine("3. Patron history");
                Console.WriteLine("4. Display all history");
                Console.WriteLine("5. Go Back");
                Console.Write("Select an option (1-5): ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        BorrowingBookByPatron(_context);
                        break;
                    case "2":
                        ReturnBookById(_context);
                        break;
                    case "3":
                        homePage.mainMenu(_context);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Press any key to continue.");
                        Console.ReadKey();
                        break;
                }
            }
        }
        public void BorrowingBookByPatron(LibraryDbContext _context)
        {
            Console.WriteLine(">> Borrowing Book <<");
            Console.Write("Enter book ID:");
            int bookId = int.Parse(Console.ReadLine());
            Console.Write("Enter patron ID:");
            int patronId = int.Parse(Console.ReadLine());
            CreateBorrowingTransaction(_context, patronId, bookId);

        }

        public void ReturnBookById(LibraryDbContext _context)
        {
            Console.Write("Enter book ID:");
            int returnBookId = int.Parse(Console.ReadLine());
            MarkBookAsReturned(_context, returnBookId);
            return;
        }
        public void CreateBorrowingTransaction(LibraryDbContext _context, int patronId, int bookId)
        {
            // Check if the book is available for borrowing
            var book = _context.Book.FirstOrDefault(b => b.BookId == bookId);
            if (book == null || !book.IsAvailable)
            {
                throw new Exception("The selected book is not available for borrowing.");
            }

            // Check if the patron exists
            var patron = _context.Patron.FirstOrDefault(p => p.PatronId == patronId);
            if (patron == null)
            {
                throw new Exception("Patron not found.");
            }

            var transaction = new BorrowingTransaction
            {
                PatronId = patronId,
                BookId = bookId,
                BorrowDate = DateTime.Now,
                ReturnDate = null
            };
            _context.BorrowingTransaction.Add(transaction);

            book.IsAvailable = false;

            _context.SaveChanges();
            Console.WriteLine("Borrowing successfully");
        }
        public void MarkBookAsReturned(LibraryDbContext _context, int returnBookId)
        {
            var transaction = _context.BorrowingTransaction.FirstOrDefault(bt => bt.BookId == returnBookId);

            if (transaction == null)
            {
                Console.WriteLine("Borrowing transaction not found.");
                return;
            }

            if (transaction.ReturnDate != null)
            {
                Console.WriteLine("The book has already been returned.");
                return;
            }

            transaction.ReturnDate = DateTime.Now;

            var book = _context.Book.FirstOrDefault(b => b.BookId == transaction.BookId);
            if (book != null)
            {
                book.IsAvailable = true;
            }

            _context.SaveChanges();
        }
        //public void 

    }
}
