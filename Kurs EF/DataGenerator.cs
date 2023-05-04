using Bogus;
using Kurs_EF.Entities;

namespace Kurs_EF
{
    public class DataGenerator
    {
        public static void Seed(MyBoardsContext context)
        {
            var locale = "pl";  //dod. opcja jezeli chcemy dane z konkretnego kraju

            Randomizer.Seed = new Random(911);  //dod. opcja jeżeli chcemy aby dane były generowane za każdym razem tak samo

            var adressGenerator = new Faker<Adress>(locale)
                //.StrictMode(true)             //dod. opcja jeśli chcemy mieć pewność że wygenerowano wszystkie dane dla encji
                .RuleFor(a => a.City, f => f.Address.City())
                .RuleFor(a => a.Country, f => f.Address.Country())
                .RuleFor(a => a.PostalCode, f => f.Address.CountryCode())
                .RuleFor(a => a.Street, f => f.Address.StreetName());

            // Adress adress = adressGenerator.Generate();

            var userGenerator = new Faker<User>()
                .RuleFor(u => u.FullName, f => f.Person.FullName)
                .RuleFor(u => u.Email, f => f.Person.Email)
            //  .RuleFor(u => u.Adress, adress);
                .RuleFor(u => u.Adress, f => adressGenerator.Generate());

            var users = userGenerator.Generate(100);

            context.AddRange(users);
            context.SaveChanges();
        }
    }
}
