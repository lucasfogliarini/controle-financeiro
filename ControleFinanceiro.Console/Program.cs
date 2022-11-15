// See https://aka.ms/new-console-template for more information

var serviceProvider = new ServiceCollection()
                            .AddServices()
                            .AddSendGrid()
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
    Console.WriteLine("2) Negative Balances By Date");
    Console.WriteLine("3) Exit");
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
            Console.WriteLine("Negative Balances By Date");
            ConsoleTable
                .From(accountService.GetNegativeBalancesByDate(emailAccount))
                .Write();
            return true;
        case "3":
            return false;
        default:
            return true;
    }
}
