using cSharp_LibrarySystemEF.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cSharp_LibrarySystemEF
{
    public class Books
    {
        public void BookMenu(LibraryDbContext _context) 
        {
            HomePage homePage = new HomePage();
            Console.Clear();
            Console.WriteLine("Library Management System - Book Menu");

            while (true)
            {
                Console.WriteLine("1. Add Book");
                Console.WriteLine("2. View All Books");
                Console.WriteLine("3. Search for Books");
                Console.WriteLine("4. Update Book");
                Console.WriteLine("5. Delete Book");
                Console.WriteLine("6. Go Back");
                Console.Write("Select an option (1-6): ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddBook(_context);
                        break;
                    case "2":
                        ViewAllBooks(_context);
                        break;
                    case "3":
                        SearchBooks(_context);
                        break;
                    case "4":
                        UpdateBook(_context);
                        break;
                    case "5":
                        DeleteBook(_context);
                        break;
                    case "6":
                        Console.Clear();
                        homePage.mainMenu(_context);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Press any key to continue.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void AddBook(LibraryDbContext _context)
        {
            Console.Clear();
            Console.WriteLine("Add Book");
            Console.Write("Enter book title: ");
            var title = Console.ReadLine();
            Console.Write("Enter author: ");
            var author = Console.ReadLine();
            Console.Write("Enter publication year: ");
            if (int.TryParse(Console.ReadLine(), out int publicationYear))
            {
                var newBook = new Book
                {
                    Title = title,
                    Author = author,
                    PublicationYear = publicationYear,
                    IsAvailable = true // Assuming the book is available when added
                };

                addBook(_context, newBook);

                Console.WriteLine("Book added successfully. Press any key to continue.");
            }
            else
            {
                Console.WriteLine("Invalid publication year. Press any key to continue.");
            }

            Console.ReadKey();
        }

        private void ViewAllBooks(LibraryDbContext _context)
        {
            Console.Clear();
            Console.WriteLine("View All Books");
            List<Book> books = getAllBooks(_context);

            foreach (var book in books)
            {
                Console.WriteLine($"ID: {book.BookId}\nTitle: {book.Title}\nAuthor: {book.Author}\n" +
                                  $"Publication Year: {book.PublicationYear}\nAvailable: {book.IsAvailable}\n________________");
            }

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
            Console.Clear();

        }

        private void SearchBooks(LibraryDbContext _context)
        {
            Console.Clear();
            Console.WriteLine("Search Books");
            Console.Write("Enter search book ID: ");
            int searchBookId = int.Parse(Console.ReadLine());

            var foundBooks = getBookById(_context, searchBookId);

            if (foundBooks != null)
            {
               
                    Console.WriteLine($"ID: {foundBooks.BookId}\nTitle: {foundBooks.Title}\nAuthor: {foundBooks.Author} " +
                                      $"\nPublication Year: {foundBooks.PublicationYear}\nAvailable: {foundBooks.IsAvailable}");
            }
            else
            {
                Console.WriteLine("No matching books found.");
            }

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
            return;
        }

        private void UpdateBook(LibraryDbContext _context)
        {
            Console.Clear();
            Console.WriteLine("Update Book");
            Console.Write("Enter book ID to update: ");
            if (int.TryParse(Console.ReadLine(), out int bookId))
            {
                var bookToUpdate = getBookById(_context, bookId);

                if (bookToUpdate != null)
                {
                    Console.Write("Enter new title: ");
                    bookToUpdate.Title = Console.ReadLine();
                    Console.Write("Enter new author: ");
                    bookToUpdate.Author = Console.ReadLine();
                    Console.Write("Enter new publication year: ");
                    if (int.TryParse(Console.ReadLine(), out int newPublicationYear))
                    {
                        bookToUpdate.PublicationYear = newPublicationYear;
                        updateBook(_context, bookToUpdate);
                        Console.WriteLine("Book updated successfully. Press any key to continue.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid publication year. Press any key to continue.");
                    }
                }
                else
                {
                    Console.WriteLine("Book not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid book ID.");
            }

            Console.ReadKey();
        }

        private void DeleteBook(LibraryDbContext _context)
        {
            Console.Clear();
            Console.WriteLine("Delete Book");
            Console.Write("Enter book ID to delete: ");
            if (int.TryParse(Console.ReadLine(), out int bookId))
            {
                var bookToDelete = getBookById(_context, bookId);

                if (bookToDelete != null)
                {
                    deleteBook(_context, bookId);
                    Console.WriteLine("Book deleted successfully. Press any key to continue.");
                }
                else
                {
                    Console.WriteLine("Book not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid book ID.");
            }

            Console.ReadKey();
        }
        public void addBook(LibraryDbContext _context, Book book)
        {
            _context.Book.Add(book);
            _context.SaveChanges();
        }
        public List<Book> getAllBooks(LibraryDbContext _context)
        {
            return _context.Book.ToList();
        }
        public Book getBookById(LibraryDbContext _context, int searchBookId)
        {
            return _context.Book.FirstOrDefault(b => b.BookId == searchBookId);
        }

        public void updateBook(LibraryDbContext _context, Book book)
        {
            _context.Entry(book).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void deleteBook(LibraryDbContext _context, int bookId)
        {
            var book = _context.Book.FirstOrDefault(b => b.BookId == bookId);
            if (book != null)
            {
                _context.Book.Remove(book);
                _context.SaveChanges();
            }
        }

    }
}
