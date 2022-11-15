### 1.
#### a) Estabele�a uma correla��o do �BUG� gerado com a aloca��o de mem�ria ressaltando os tipos de mem�rias existentes no C#.
	Existem 2 tipos em c#: 
	- Tipo de Refer�ncia que s�o declarados com as palavras-chaves: class, interface, delegate e record(novo no C# 9)
	- Tipo de Valor que � declarado com a palavra-chave: struct

	b) Uma f�cil e poss�vel solu��o �:
	1. Implementar a nova palavra chave record na classe Pessoa:
	record class Pessoa
	{
	...

	2. Adicionar a palavra chave with pra fazer o clone de p1 em p2
	var p2 = p1 with { Name = "M" }; 

	Outras solu��es para caso n�o use o C#9 � criar uma nova instancia pra p2 ou Serializar e Deserializar p1.



Refer�ncia:
https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/reference-types
https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/record