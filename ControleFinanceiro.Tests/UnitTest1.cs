using ControleFinanceiro.AccountService;

namespace ControleFinanceiro.Tests
{
    public class UnitTest1
    {
        [Theory]
        [InlineData(FinancialReleaseType.Income, 300)]
        public void Test1(FinancialReleaseType releaseType, decimal value)
        {
            //var release1 = new FinancialReleaseInput
            //{
            //    Type = releaseType,
            //    Value = value,
            //    Description = Guid.NewGuid().ToString(),                
            //};

            //new AccountService().

           
        }
    }
}