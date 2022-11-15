// See https://aka.ms/new-console-template for more information

var serviceProvider = new ServiceCollection()
                            .AddServices()
                            .BuildServiceProvider();

var accountService = serviceProvider.GetService<IAccountService>();

string emailAccount = await serviceProvider.GetService<SeedService>().Seed();

bool showMenu = true;
while (showMenu)
{
    showMenu = Program();
    if (showMenu)
    {
        Console.WriteLine("\n Press any key to return to Menu");
        Console.ReadLine();
    }
}

bool Program()
{
    Console.Clear();
    Console.WriteLine("Choose an option:");
    Console.WriteLine("1) All Financial Releases");
    Console.WriteLine("2) Balances By Date");
    Console.WriteLine("3) Negative Balances By Date");
    Console.WriteLine("4) Exit");
    Console.Write("\r\nOption: ");

    var option = Console.ReadLine();
    Console.Clear();

    switch (option)
    {
        case "1":
            Console.WriteLine("All Financial Releases");
            ConsoleTable
                .From(accountService.GetFinancialReleases(emailAccount))
                .Write();
            return true;
        case "2":
            Console.WriteLine("Balances By Date");
            ConsoleTable
                .From(accountService.GetBalancesByDate(emailAccount))
                .Write();
            Console.WriteLine("\n Query:");
            Console.WriteLine("SELECT [ReleaseAt] Date, sum([Value]) Balance");
            Console.WriteLine("FROM [FinancialRelease] r");
            Console.WriteLine("group by ReleaseAt");
            return true;
        case "3":
            Console.WriteLine("Negative Balances By Date");
            ConsoleTable
                .From(accountService.GetBalancesByDate(emailAccount).Where(e => e.Balance < 0))
                .Write();
            return true;
        case "4":
            return false;
        default:
            return true;
    }
}
