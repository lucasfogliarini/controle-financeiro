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


