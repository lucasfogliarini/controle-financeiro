// See https://aka.ms/new-console-template for more information

using ControleFinanceiro.Database;
using ControleFinanceiro.Entities;

var serviceProvider = new ServiceCollection()
                            .AddServices()
                            .AddSendGrid()
                            .BuildServiceProvider();

var accountService = serviceProvider.GetService<IAccountService>();

string emailAccount = await Seed();

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
    Console.WriteLine("1) All Releases");
    Console.WriteLine("2) Negative Balances By Date");
    Console.WriteLine("3) Exit");
    Console.Write("\r\nOption: ");

    var option = Console.ReadLine();
    Console.Clear();

    switch (option)
    {
        case "1":
            Console.WriteLine("All Releases");
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

async Task<string> Seed()
{
    var controleFinanceiroDatabase = serviceProvider.GetService<IControleFinanceiroDatabase>();
    var account = new Account 
    {
        Name = "Lucas Fogliarini",
        Email = "lucasfogliarini@gmail.com",
        Balance = 0 
    };
    controleFinanceiroDatabase.Add(account);
    controleFinanceiroDatabase.Commit();

    var financialReleases = Seeder.GetFinancialReleases();
    foreach (var financialRelease in financialReleases)
    {
        var financialReleaseInput = new FinancialReleaseInput
        {
            Email = account.Email,
            Value = financialRelease.Value,
            Description = financialRelease.Description,
            ReleaseAt = financialRelease.ReleaseAt,
        };
        await accountService.ReleaseAsync(financialReleaseInput);
    }

    return account.Email;
}
