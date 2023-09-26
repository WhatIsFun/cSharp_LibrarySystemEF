using cSharp_LibrarySystemEF.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cSharp_LibrarySystemEF
{
    public class Patrons
    {
        public void PatronMenu(LibraryDbContext _context)
        {
            HomePage homePage = new HomePage();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Library Management System - Patron Menu");
                Console.WriteLine("1. Add Patron");
                Console.WriteLine("2. View All Patrons");
                Console.WriteLine("3. Search Patrons");
                Console.WriteLine("4. Update Patron");
                Console.WriteLine("5. Delete Patron");
                Console.WriteLine("6. Go Back");
                Console.Write("Select an option (1-6): ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddPatron(_context);
                        break;
                    case "2":
                        ViewAllPatrons(_context);
                        break;
                    case "3":
                        SearchPatrons(_context);
                        break;
                    case "4":
                        UpdatePatron(_context);
                        break;
                    case "5":
                        DeletePatrons(_context);
                        break;
                    case "6":
                        homePage.mainMenu(_context);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Press any key to continue.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void AddPatron(LibraryDbContext _context)
        {
            Console.Clear();
            Console.WriteLine("Add Patron");
            Console.Write("Enter patron name: ");
            var name = Console.ReadLine();
            Console.Write("Enter contact information: ");
            var phone = Console.ReadLine();

            var newPatron = new Patron
            {
                Name = name,
                PhoneNum = phone
            };

            _context.Add(newPatron);
            _context.SaveChanges();

            Console.WriteLine("Patron added successfully. Press any key to continue.");
            Console.ReadKey();
        }

        private void ViewAllPatrons(LibraryDbContext _context)
        {
            Console.Clear();
            Console.WriteLine("View All Patrons");
            List<Patron> patrons = GetAllPatrons(_context);

            foreach (var patron in patrons)
            {
                Console.WriteLine($"ID: {patron.PatronId}\nName: {patron.Name}\nContact: {patron.PhoneNum}\n__________________");
            }

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
            Console.Clear();
        }

        private void SearchPatrons(LibraryDbContext _context)
        {
            Console.Clear();
            Console.WriteLine("Search Patrons");
            Console.Write("Enter search keyword: ");
            var keyword = Console.ReadLine();

            List<Patron> foundPatrons = SearchPatrons(_context, keyword);

            if (foundPatrons.Count == 0)
            {
                Console.WriteLine("No matching patrons found.");
            }
            else
            {
                foreach (var patron in foundPatrons)
                {
                    Console.WriteLine($"ID: {patron.PatronId}, Name: {patron.Name}, Contact: {patron.PhoneNum}");
                }
            }

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        private void UpdatePatron(LibraryDbContext _context)
        {
            Console.Clear();
            Console.WriteLine("Update Patron");
            Console.Write("Enter patron ID to update: ");
            if (int.TryParse(Console.ReadLine(), out int patronId))
            {
                var patronToUpdate = GetPatronById(_context, patronId);

                if (patronToUpdate != null)
                {
                    Console.Write("Enter new name: ");
                    patronToUpdate.Name = Console.ReadLine();
                    Console.Write("Enter new contact information: ");
                    patronToUpdate.PhoneNum = Console.ReadLine();

                    UpdatePatron(_context, patronToUpdate);

                    Console.WriteLine("Patron updated successfully. Press any key to continue.");
                }
                else
                {
                    Console.WriteLine("Patron not found.");
                    return;
                }
            }
            else
            {
                Console.WriteLine("Invalid patron ID.");
                return;
            }

            Console.ReadKey();
        }

        private void DeletePatrons(LibraryDbContext _context)
        {
            Console.Clear();
            Console.WriteLine("Delete Patron");
            Console.Write("Enter patron ID to delete: ");
            if (int.TryParse(Console.ReadLine(), out int patronId))
            {
                var patronToDelete = GetPatronById(_context, patronId);

                if (patronToDelete != null)
                {
                    DeletePatron(_context, patronId);
                    Console.WriteLine("Patron deleted successfully. Press any key to continue.");
                    Console.ReadLine();
                    return;
                }
                else
                {
                    Console.WriteLine("Patron not found.");
                    return;
                }
            }
            else
            {
                Console.WriteLine("Invalid patron ID.");
                return;
            }
        }
        public List<Patron> GetAllPatrons(LibraryDbContext _context)
        {
            return _context.Patron.ToList();
        }
        public Patron GetPatronById(LibraryDbContext _context, int patronId)
        {
            return _context.Patron.FirstOrDefault(p => p.PatronId == patronId);
        }
        public List<Patron> SearchPatrons(LibraryDbContext _context, string searchKeyword)
        {
            return _context.Patron
                .Where(p => p.Name.Contains(searchKeyword) || p.PhoneNum.Contains(searchKeyword))
                .ToList();
        }
        public void UpdatePatron(LibraryDbContext _context, Patron patron)
        {
            var existingPatron = _context.Patron.FirstOrDefault(p => p.PatronId == patron.PatronId);

            if (existingPatron != null)
            {
                existingPatron.Name = patron.Name;
                existingPatron.PhoneNum = patron.PhoneNum;

                _context.SaveChanges();
            }
        }
        public void DeletePatron(LibraryDbContext _context, int patronId)
        {
            var patronToDelete = _context.Patron.FirstOrDefault(p => p.PatronId == patronId);

            if (patronToDelete != null)
            {
                _context.Patron.Remove(patronToDelete);
                _context.SaveChanges();
            }
        }
    }
}
