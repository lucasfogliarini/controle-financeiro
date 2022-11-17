### 1.
#### a) Estabeleça uma correlação do “BUG” gerado com a alocação de memória ressaltando os tipos de memórias existentes no C#.
Existem 2 tipos em c#: 
- Tipo de Referência que são declarados com as palavras-chaves: class, interface, delegate e record(novo no C# 9)
- Tipo de Valor que é declarado com a palavra-chave: struct

No código de de exemplo é usado um Tipo de Referência declarado por uma classe que armazena a referência dos seus dados (objetos). Ao atribuir p1 em p2, p2 recebe a referência de p1 obtendo duas variáveis com a mesma referência e mesmos dados.

#### b) Dê uma solução para que o desenvolvedor possa copiar os dados de outra pessoa sem ter o problema da sobrescrita de dados.
Uma fácil e possível solução é:
1. Implementar a nova palavra chave record na classe Pessoa:
```csharp
public record class Pessoa
{
...
```

2. Adicionar a palavra chave with pra fazer o clone de p1 em p2
```csharp
var p2 = p1 with { Name = "Maria" };
```

Outras soluções para caso não use o C#9 é criar uma nova instancia pra p2 ou Serializar e Deserializar p1.

##### Referência:
https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/reference-types  
https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/record

### 2. Nos dias atuais, muito se tem falado da arquitetura em micro serviços e seus benefícios, o que ocasionalmente acaba gerando uma tendência de os desenvolvedores aplicarem esse padrãode mercado em todos os projetos em que participam. Explique quando o padrão de micro serviços não se faz necessário e quais são os principais desafios em relação a manutenção desses micros serviços colocando em perspectiva a parte de CI/ CD, indicadores e equipe do projeto.

Microsserviços traz uma série de desafios. Sua utilização requer um alto nível de automação e uma coordenação maior entre as equipes de desenvolvimento, segurança e infraestrutura (DevOps). Abaixo alguns pontos de atenção:

1. Você precisará estar equipado para provisionamento rápido e implantação de aplicativos, providenciando recursos instantaneamente para acompanhar o ritmo necessário para aproveitar ao máximo os microsserviços. Se demorar dias ou meses para conseguir providenciar um servidor, você terá sérios problemas. Da mesma forma, você deve rapidamente implantar novos serviços ou aplicativos.

2. Um monitoramento robusto é obrigatório. Como cada serviço depende do seu próprio idioma, plataforma e API, além de ter equipes trabalhando simultaneamente em diferentes entidades do seu projeto de microsserviços, você precisa de um monitoramento robusto para gerenciar efetivamente toda a infraestrutura, porque se você não fizer isso, pode ficar impossível rastrear problemas, como saber quando um serviço falhar ou uma máquina cair.

3. Você deve abraçar a cultura DevOps. Para trabalhar em equipes multifuncionais, sua empresa deve incorporar práticas e cultura DevOps. Em uma configuração tradicional, os desenvolvedores estão focados em recursos e funcionalidades, e a equipe de operações está no alcance de desafios de produção. Em DevOps, todos são responsáveis pelo provisionamento de serviços.

### 3. No dia a dia do desenvolvimento de software é muito comum existirem configurações específicas para cada tipo de ambiente como: Desenvolvimento, Qualidade e Produção. Sendo assim, se faz necessário estruturas que permitem esta mudança de chave por tipo de ambiente. Explique como isto se dá no C# utilizando um projeto do tipo API ou Worker Service.

A configuração no .NET é realizada usando um ou mais provedores de configuração. Os provedores de configuração leem dados de configuração de pares chave-valor usando várias fontes de configuração.
Entre eles existe o **JsonConfigurationProvider** que herda de FileConfigurationProvider.
Com esse provider é possível adicionar um arquivo de configuração para cada ambiente (appsettings.{Environment}.json) especificando cada configuração em chave valor, exemplo:  
  **Development (appsettings.Development.json)**
  ```json 
  {
    "AccountApi": "https://account-dev.controlefinanceiro.com/api/"
  }

  ```
  **Staging (appsettings.Staging.json)**
  ```json  
  {
    "AccountApi": "https://account-stg.controlefinanceiro.com/api/"
  }
  ```
  **Production (appsettings.Production.json)**
  ```json  
  {
    "AccountApi": "https://account.controlefinanceiro.com/api/"
  }
  ```
  O arquivo específico do ambiente sobreescreve o arquivo principal appsettings.json.
  
  > **Warning**  
  > Configurações sensíveis como api keys, connection strings ou qualquer configuração secreta é recomendado usar o Secrets Manager localmente e 
  Azure Key Vault configuration provider na Azure
  
  
##### Referência:
https://learn.microsoft.com/en-us/dotnet/core/extensions/configuration  
https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-7.0#appsettingsjson  
https://learn.microsoft.com/en-us/azure/key-vault/general/overview  
https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-7.0

### 4. É muito comum no desenvolvimento de software o lançamento de exceções quando uma regra não é atendida no código. Sendo assim, defina quais cuidados devem ser tomados na hora de lançar exceção, estabelecendo uma correlação com o padrão Notification Pattern.

### 5. Crie um sistema de Controle Financeiro Pessoal na plataforma .NET que receba os dados da tabela abaixo e realize um fluxo de caixa detalhando as movimentações diariamente. Após finalização, versionar código no git e enviar o link para o e-mail tiago.rudek@linx.com.br;Jhonas.moises@linx.com.br e Richard.chiavelli@linx.com.br.

##### a) Qual foi o saldo final do fluxo de caixa? Coloque o SQL utilizado para retornar o saldo final de cada dia.  
    Rode a aplicação ControleFinanceiro.Console e escolha a opção 2) Balances By Date
    
##### b) Quais dias em que o caixa teve as despesas maiores que as receitas, ou seja, o saldo ficou negativo?  
    Rode a aplicação ControleFinanceiro.Console e escolha a opção 3) Negative Balances By Date
    
##### c) No ambiente de desenvolvimento de software, é muito comum o desenvolvimento de automações que executam rotinas específicas em um determinado intervalo de tempo definido. Crie um serviço que rode diariamente e notifique o usuário via e-mail quando este estiver com o saldo do fluxo de caixa negativo.  
    Rode o worker do projeto ControleFinanceiro.Worker
    
##### d) A realização de testes para a garantia de qualidade do código é muito importante no ambiente de desenvolvimento de software. Dito isto a plataforma .NET oferece diversas tecnologias para esta funcionalidade, dentre elas: XUnit. Realize um projeto de teste com a cobertura de 100% do código do sistema de Controle 0Financeiro Pessoal.
    Rode os testes do projeto ControleFinanceiro.Tests
