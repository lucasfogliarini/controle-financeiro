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

A configuração no .NET é realizada usando um ou mais provedores de configuração. Os provedores de configuração leem dados de configuração de pares chave-valor usando várias fontes de configuração, entre eles:
- JsonConfigurationProvider que herda de FileConfigurationProvider como appsettings.json
- EnvironmentVariablesConfigurationProvider

##### Referência:
https://learn.microsoft.com/en-us/dotnet/core/extensions/configuration
