using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cSharp_LibrarySystemEF
{
    public class HomePage
    {
        public void mainMenu(LibraryDbContext _context)
        {
            Books books = new Books();
            Patrons patrons = new Patrons();
            BorrowingBook borrowingBook = new BorrowingBook();
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("1. Books Menu");
                Console.WriteLine("2. Patrons Menu");
                Console.WriteLine("3. Transaction Menu");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("4. Exit");
                Console.ResetColor();
                Console.Write("Select an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        books.BookMenu(_context);
                        break;
                    case "2":
                        patrons.PatronMenu(_context);
                        break;
                    case "3":
                        borrowingBook.BorrowingBookMenu(_context);
                        break;
                    case "4":
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("Are you sure you want to exit? (y/n) "); // Check if the user want to exit the application
                        string ExitInput = Console.ReadLine();
                        ExitInput.ToLower();
                        Console.ResetColor();
                        if (ExitInput.Equals("y", StringComparison.OrdinalIgnoreCase))
                        {
                            Console.Write("Thank You for using our services");
                            Environment.Exit(0);
                        }
                        else
                        {
                            Console.Clear();
                            mainMenu(_context);
                        }
                        break;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
                Console.Clear();
            }
        }
    }
}
