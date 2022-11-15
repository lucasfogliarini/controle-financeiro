// See https://aka.ms/new-console-template for more information

using ControleFinanceiro.Database;
using ControleFinanceiro.Entities;

var serviceProvider = new ServiceCollection()
                            .AddServices()
                            .AddSendGrid()
                            .BuildServiceProvider();

var accountService = serviceProvider.GetService<IAccountService>();

string emailAccount = await Seed();

ConsoleTable
    .From(accountService.GetFinancialReleases(emailAccount))
    .Write();

Console.ReadKey();

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
        await accountService.Release(financialReleaseInput);
    }

    return account.Email;
}
