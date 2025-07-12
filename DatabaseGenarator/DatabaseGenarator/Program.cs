using DatabaseGenarator;

class Program
{
    static void Main(string[] args)
    {
        using (var context = new HubDbContext())
        {
            context.Database.EnsureCreated();
            DataSeeder.SeedData(context);
        }

        Console.WriteLine("Data seeding completed.");
    }
}