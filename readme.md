Pokedex
Oversigt:
Pokedex-projektet er en konsolapplikation, der giver dig mulighed for at se din samling af Pokémons. 
Brugeren kan se alle de Pokémons brugeren og søge efter specifikke Pokémons, og hvis man logger ind kan man redigere i pokedexet.

Funktioner:
* Brugerautentifikation: Brugere kan logge ind for at få adgang til redigereing.
* Se alle Pokémons: Viser en liste over alle Pokémoner i Pokedexen.
* Søg Pokémoner: Mulighed for at søge efter Pokémons ved navn.
* Rediger Pokedex: Tilføj, rediger eller slet Pokémon-poster.

Filstruktur:
* Program.cs: Programmets startpunkt, initialiserer menuen og hasher passwords.
* Login.cs: Menuen til at login, checker om brugernavn og passwords er korrekt og hasher det sendte password.
* ViewPokemon.cs: Viser en liste over alle de Pokemons du har, og laver en ny sude for hver femte Pokemon.
* Search.cs: Du kan indstate en Pokemons navn også viser siden dig alle de Pokemons du har, som har det navn du har indtastet.
* EditMenu.cs: En menu hvor du kan vælge at slette en Pokemon, tilføje en Pokemon og redigere i en Pokemon der allerade findes.

Sådan Bruges Programmet:
* Programmet starter med en hovedmenu, hvor du kan vælge mellem at Login, se alle pokemons, søge efter en, gå til redigerings menuen eller afslutte programmet.
* Du skal trykke 1, 2, 3, 4 eller 5 for at vælge hvad du ville.
* Hvis du vælger 1, så skal du logge ind (Brugernavne og adgangskoderne kommer senere i filen). Når du har logget ind, så får du adgang til at redigere i pokedexet.
* Hvis du vælger 2, så får du vist en eller flere lister, over alle dine Pokémons.
* Hvis du trykker på 3, så kan du efter en Pokémon ved hjælp af navnet.
* Hvis du trykker på 4, så bliver der åbnet op til endnu en menu hvor du kan Tilføj, rediger eller slet Pokémons. Du skal værre logget ind for at kunne gøre det.
* Hvis du trykker på 5, så afslutter du programmet.
* Inde i redigerings menuen så får du en ny menu, med andre valgmuligheder.
* 1. Der kan du tilføje en ny pokemon
* 2. Der kan du slette en eksisterende Pokemon.
* 3. Der kan du redigere i en eksisterende Pokemon. Du kan vælge at redigere i navnet, typen eller styrken.
* 4. Du går tilbage til hovedmenuen.

Datafiler:
* userdata.csv: Indeholder brugeroplysninger.
* Pokedex.csv: Indeholder oplysninger om Pokémoner.

Brugernavne og Adgangskoder:
Brugernavn: Asbjorn; Adgangskode: Asbjorn123
Brugernavn: user; Adgangskode; user132
Brugernavn: Admin; Adgangskode; Admin123

Krav
* .NET 9.0 SDK

Opsætning
1. Klon repositoryet.
2. Åbn løsningen i Visual Studio.
3. Byg projektet.
4. Kør applikationen.
