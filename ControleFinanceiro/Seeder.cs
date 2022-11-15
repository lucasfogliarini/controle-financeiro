namespace ControleFinanceiro
{
    public static class Seeder
    {
        public static IEnumerable<FinancialRelease> Seed()
        {
            var financialReleases = new List<FinancialRelease>()
            {
                new FinancialRelease 
                { ReleaseAt = new DateTime(2022, 8, 29), Description = "Cartão de Crédito", Value = -825.82m },
                new FinancialRelease
                { ReleaseAt = new DateTime(2022, 8, 29), Description = "Curso C#", Value = -200 },
                new FinancialRelease
                { ReleaseAt = new DateTime(2022, 8, 31), Description = "Salário", Value = 7000 },
                new FinancialRelease
                { ReleaseAt = new DateTime(2022, 9, 1), Description = "Mercado", Value = -3000 },
                new FinancialRelease
                { ReleaseAt = new DateTime(2022, 9, 1), Description = "Farmácia", Value = -300 },
                new FinancialRelease
                { ReleaseAt = new DateTime(2022, 9, 1), Description = "Combustível", Value = -800.25m },
                new FinancialRelease
                { ReleaseAt = new DateTime(2022, 9, 15), Description = "Financiamento Carro", Value = -900 },
                new FinancialRelease
                { ReleaseAt = new DateTime(2022, 9, 22), Description = "Financiamento Casa", Value = -1200 },
                new FinancialRelease
                { ReleaseAt = new DateTime(2022, 9, 25), Description = "Freelance Projeto XPTO", Value = 2500 },
            };

            return financialReleases;
        }
    }
}
