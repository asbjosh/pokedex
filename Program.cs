using System.Runtime.CompilerServices;

namespace pokedex;

class Program
{
    static void Main(string[] args)
    {
        ShowMainMenu();
    }

    internal static void ShowMainMenu()
    {
        do
        {
            Console.Clear();
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Se alle Pokémons");
            Console.WriteLine("3. Søg efter en Pokémon");
            Console.WriteLine("4. Rediger i Pokédexet");
            Console.Write("5. Afslut programmet: ");
            int menuChoice = Convert.ToInt32(Console.ReadLine());

            switch (menuChoice)
            {
                case 1:
                    {
                        Login login = new();
                        login.LoginMenu();
                        break;
                    }
                case 2:
                    {
                        ViewPokémons viewpokémons = new();
                        viewpokémons.DisplayPokémons();
                        break;
                    }
                case 3:
                    {
                        Search search = new();
                        search.Searching();
                        break;
                    }
                case 4:
                    {
                        break;
                    }
                case 5:
                    {
                        Console.Clear();
                        Console.WriteLine("Programmet afsluttes!");
                        Thread.Sleep(3000);
                        Environment.Exit(0);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        } while (true);
    }
}

class Login
{
    internal void LoginMenu()
    {
        User user = new();

        do
        {
            Console.Clear();
            Console.Write("Indtast brugernavn: ");
            string username = Console.ReadLine();

            Console.Clear();
            Console.Write("Indtast kodeord: ");
            string password = Console.ReadLine();

            user.LoggedIn = LoginCheck(username, password);

            if (user.LoggedIn == false)
            {
                Console.Clear();
                Console.WriteLine("Brugernavn eller adgangskode er forkert!");
                Console.ReadKey();
            }
        } while (!user.LoggedIn);
    }

    internal bool LoginCheck(string username, string password)
    {
        string[] file = File.ReadAllLines("userdata.csv");
        string[] x;
        file = file.Skip(1).ToArray();

        foreach (var item in file)
        {
            x = item.Split(",");

            if (x[0].ToLower() == username.ToLower() && x[1] == password)
            {
                return true;
            }
        }

        return false;
    }
}

class User
{
    public bool LoggedIn { get; set; }
}

class ViewPokémons
{
    internal void DisplayPokémons()
    {
        Console.Clear();

        string filePath = "Pokedex.csv";
        string[] displayPokémons = File.ReadAllLines(filePath).Skip(1).ToArray();

        int currentPage = 0;
        int pokesPerPage = 5;
        int totalPages = (int)Math.Ceiling(displayPokémons.Length / (double)pokesPerPage);

        bool loop = true;

        while (loop)
        {
            Console.WriteLine($"Page {currentPage + 1} of {totalPages}");

            var pokémonsOnPage = displayPokémons
                .Skip(currentPage * pokesPerPage)
                .Take(pokesPerPage);

            foreach (var item in pokémonsOnPage)
            {
                string[] x = item.Split(",");
                Console.WriteLine($"Navn: {x[1]} Type: {x[2]} Styrke: {x[3]}");
            }

            Console.WriteLine("\n N for at gå til næste side, P for at gå til den sidste side, Q for at gå tilbage til hovedmenuen.");

            var key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.N && currentPage < totalPages - 1)
                currentPage++;
            else if (key == ConsoleKey.P && currentPage > 0)
                currentPage--;
            else if (key == ConsoleKey.Q)
                loop = false;
        }
    }
}

class Search
{
    public void Searching()
    {
        Console.Clear();
        string filePath = "Pokedex.csv";
        string[] searchPokémons = File.ReadAllLines(filePath).Skip(1).ToArray();
        int results = 0;

        Console.Write("Søg efter en Pokémons navn ellers den type: ");
        string input = Console.ReadLine();


        foreach (var item in searchPokémons)
        {
            string[] x = item.Split(",");

            if (x[1].Contains(input) || x[2].Contains(input))
            {
                Console.WriteLine($"Navn: {x[1]} Type: {x[2]} Styrke: {x[3]}");
                results++;
            }
        }
        if (results == 0)
        {
            Console.WriteLine("Der blev ikke fundet nogle Pokémons");
        }
        Console.ReadKey();
    }
}