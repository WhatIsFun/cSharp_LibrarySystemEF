using System.ComponentModel;

namespace cSharp_LibrarySystemEF
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int pass = 110051;
            Console.Write("Enter the password: ");
            int type = int.Parse(Console.ReadLine());
            if (type == pass)
            {
                var _context = new LibraryDbContext();

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("      Welcome To    ");
                Console.WriteLine(" +-+-+-+-+-+-+-+-+-+\r\n |W|h|a|t|I|s|F|u|n|\r\n +-+-+-+-+-+-+-+-+-+");
                Console.WriteLine("Library Management System      \n");
                Console.ResetColor();
                Console.WriteLine("\n\n\n\n\n\nPress any key to start.....");
                Console.ReadLine();
                HomePage homePage = new HomePage();
                homePage.mainMenu(_context);
            }
            else
            {
                Console.WriteLine("Invalid Password. try again :)");
                return;
            }
            
        }
    }
}