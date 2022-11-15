### 1.
#### a) Estabeleça uma correlação do “BUG” gerado com a alocação de memória ressaltando os tipos de memórias existentes no C#.
	Existem 2 tipos em c#: 
	- Tipo de Referência que são declarados com as palavras-chaves: class, interface, delegate e record(novo no C# 9)
	- Tipo de Valor que é declarado com a palavra-chave: struct

	b) Uma fácil e possível solução é:
	1. Implementar a nova palavra chave record na classe Pessoa:
	record class Pessoa
	{
	...

	2. Adicionar a palavra chave with pra fazer o clone de p1 em p2
	var p2 = p1 with { Name = "M" }; 

	Outras soluções para caso não use o C#9 é criar uma nova instancia pra p2 ou Serializar e Deserializar p1.



Referência:
https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/reference-types
https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/record