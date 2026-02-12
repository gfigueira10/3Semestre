using Exercicio3;

Pessoa joao = new Pessoa();

joao.Nome = "João Silva";

Console.Write("Digite Idade: ");
joao.DefinirIdade(int.Parse(Console.ReadLine()));

Console.WriteLine($"{joao.Nome} tem {joao.ObterIdade()} anos");
