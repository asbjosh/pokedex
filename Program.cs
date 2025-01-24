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
                        EditMenu editMenu = new();
                        editMenu.ViewEditMenu();
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

class EditMenu
{
    public void ViewEditMenu()
    {
        Console.Clear();
        Console.WriteLine("1. Tilføj en Pokémon");
        Console.WriteLine("2. Slet en Pokémon");
        Console.WriteLine("3. Redigere i en Pokémon");
        Console.Write("4. Gå tilbage til hovedmenuen: ");
        int menuChoice = Convert.ToInt32(Console.ReadLine());

        switch (menuChoice)
        {
            case 1:
                {
                    Add add = new();
                    add.AddPokemon();
                    break;
                }
            case 2:
                {
                    Delete delete = new();
                    delete.DeletePokemon();
                    break;
                }
            case 3:
                {
                    Edit edit = new();
                    edit.EditPokemon();
                    break;
                }
            case 4:
                {
                    break;
                }
            default:
                {
                    break;
                }
        }
    }
}

class Add
{
    public void AddPokemon()
    {
        Console.Clear();
        string filePath = "Pokedex.csv";

        // Find the highest ID in the file
        int nextId = 1;
        var lines = File.ReadLines(filePath).Skip(1).ToArray(); // Skip header
        if (lines.Any())
        {
            // Get the maximum ID
            int maxId = lines
                .Select(line => int.Parse(line.Split(",")[0])) // Get the ID from each line
                .Max(); // Get the max ID value

            nextId = maxId + 1; // Set the next ID to be one greater than the max ID
        }

        Console.Write("Indtast Pokémons Navn: ");
        string name = Console.ReadLine();

        Console.Write("Indtast Pokémons Type: ");
        string type = Console.ReadLine();

        Console.Write("Indtast Pokémons Styrke: ");
        string strength = Console.ReadLine();

        if (int.TryParse(strength, out _))
        {
            string newPokemon = $"{nextId},{name},{type},{strength}";

            // Check if the last line already ends with a newline before appending
            if (File.Exists(filePath) && new FileInfo(filePath).Length > 0)
            {
                // Read the last line to check if it ends with a newline
                var lastLine = File.ReadLines(filePath).Last();
                if (!lastLine.EndsWith(Environment.NewLine))
                {
                    File.AppendAllText(filePath, Environment.NewLine); // Add a newline if necessary
                }
            }

            // Now append the new Pokémon properly
            File.AppendAllText(filePath, newPokemon);

            Console.WriteLine("\nThe Pokémon has been added successfully!");
        }
        else
        {
            Console.WriteLine("\nInvalid Strength. Please try again.");
        }

        Console.ReadKey();
    }
}

class Delete
{
    public void DeletePokemon()
    {
        string filePath = "Pokedex.csv";
        string[] lines = File.ReadAllLines(filePath);

        // Skip the header row and get the Pokémon data
        var pokemons = lines.Skip(1).ToArray();

        // Ask for the Pokémon name
        Console.Write("Søg efter Pokémon at slette: ");
        string input = Console.ReadLine();

        // Find matching Pokémon based on the name
        var matchingPokemons = pokemons
            .Where(pokemon => pokemon.Split(",")[1].Contains(input, StringComparison.OrdinalIgnoreCase))
            .ToArray();

        if (matchingPokemons.Any())
        {
            // Display all matches
            Console.WriteLine("Fundne Pokémons:");
            for (int i = 0; i < matchingPokemons.Length; i++)
            {
                string[] parts = matchingPokemons[i].Split(",");
                Console.WriteLine($"{i + 1}. Navn: {parts[1]}, Type: {parts[2]}, Styrke: {parts[3]}");
            }

            // Ask user to select a Pokémon to delete
            Console.Write("Vælg Pokémon at slette (nummer): ");
            int choice = Convert.ToInt32(Console.ReadLine()) - 1;

            if (choice >= 0 && choice < matchingPokemons.Length)
            {
                string selectedPokemon = matchingPokemons[choice];

                // Remove the selected Pokémon from the list
                pokemons = pokemons.Where(pokemon => pokemon != selectedPokemon).ToArray();

                // Rebuild the file with the remaining Pokémon and update IDs
                var updatedPokemons = new List<string> { lines[0] }; // Keep header

                // Update the IDs and add remaining Pokémon
                int newId = 1;
                foreach (var pokemon in pokemons)
                {
                    var parts = pokemon.Split(",");
                    updatedPokemons.Add($"{newId},{parts[1]},{parts[2]},{parts[3]}");
                    newId++; // Increment the ID
                }

                // Write the updated data back to the file without adding extra newlines
                File.WriteAllText(filePath, string.Join(Environment.NewLine, updatedPokemons));

                Console.WriteLine("Pokémon slettet!");
            }
            else
            {
                Console.WriteLine("Ugyldigt valg.");
            }
        }
        else
        {
            Console.WriteLine("Ingen Pokémon fundet.");
        }

        Console.ReadKey();
    }
}

class Edit
{
    public void EditPokemon()
    {
        string filePath = "Pokedex.csv";
        string[] lines = File.ReadAllLines(filePath);

        // Skip the header row
        var pokemons = lines.Skip(1).ToArray();

        Console.Write("Søg efter Pokémon at redigere: ");
        string input = Console.ReadLine();

        var matchingPokemons = pokemons
            .Where(pokemon => pokemon.Split(",")[1].Contains(input, StringComparison.OrdinalIgnoreCase))
            .ToArray();

        if (matchingPokemons.Any())
        {
            // Display all matches
            Console.WriteLine("Fundne Pokémons:");
            for (int i = 0; i < matchingPokemons.Length; i++)
            {
                string[] parts = matchingPokemons[i].Split(",");
                Console.WriteLine($"{i + 1}. Navn: {parts[1]}, Type: {parts[2]}, Styrke: {parts[3]}");
            }

            // Ask user to select a Pokémon to edit
            Console.Write("Vælg Pokémon at redigere (nummer): ");
            int choice = Convert.ToInt32(Console.ReadLine()) - 1;

            if (choice >= 0 && choice < matchingPokemons.Length)
            {
                string selectedPokemon = matchingPokemons[choice];
                string[] selectedPokemonParts = selectedPokemon.Split(",");

                // Start editing the selected Pokémon
                Console.WriteLine("\nHvilken del af Pokémonen vil du redigere?");
                Console.WriteLine("1. Navn");
                Console.WriteLine("2. Type");
                Console.WriteLine("3. Styrke");
                Console.WriteLine("4. Afslut");
                bool editing = true;

                while (editing)
                {
                    Console.Write("Vælg en mulighed: ");
                    int editChoice = Convert.ToInt32(Console.ReadLine());

                    switch (editChoice)
                    {
                        case 1:
                            Console.Write("Indtast det nye navn: ");
                            selectedPokemonParts[1] = Console.ReadLine();
                            break;

                        case 2:
                            Console.Write("Indtast den nye type: ");
                            selectedPokemonParts[2] = Console.ReadLine();
                            break;

                        case 3:
                            Console.Write("Indtast den nye styrke: ");
                            selectedPokemonParts[3] = Console.ReadLine();
                            break;

                        case 4:
                            editing = false;
                            break;

                        default:
                            Console.WriteLine("Ugyldigt valg, prøv igen.");
                            break;
                    }

                    if (editing)
                    {
                        // Ask if user wants to keep editing
                        Console.WriteLine("\nVil du redigere noget andet?");
                        Console.WriteLine("1. Ja");
                        Console.WriteLine("2. Nej");
                        int continueEditing = Convert.ToInt32(Console.ReadLine());
                        if (continueEditing == 2)
                        {
                            editing = false;
                        }
                    }
                }

                // Update the selected Pokémon with the new details
                string updatedPokemon = string.Join(",", selectedPokemonParts);
                pokemons = pokemons.Select(pokemon => pokemon == selectedPokemon ? updatedPokemon : pokemon).ToArray();

                // Rebuild the file without extra lines at the end
                using (var writer = new StreamWriter(filePath, false))
                {
                    writer.Write(lines[0] + "\n"); // Write the header with explicit newline

                    for (int i = 0; i < pokemons.Length; i++)
                    {
                        if (i == pokemons.Length - 1)
                        {
                            // Avoid adding an extra newline at the end of the file
                            writer.Write(pokemons[i]);
                        }
                        else
                        {
                            writer.WriteLine(pokemons[i]);
                        }
                    }
                }

                Console.WriteLine("\nPokémon er blevet opdateret!");
            }
            else
            {
                Console.WriteLine("Ugyldigt valg.");
            }
        }
        else
        {
            Console.WriteLine("Ingen Pokémon fundet.");
        }

        Console.ReadKey();
    }
}