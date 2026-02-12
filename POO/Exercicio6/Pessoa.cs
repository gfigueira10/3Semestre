using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercicio6
{
    public class Pessoa
    {
         public class Pessoa
    {
        public string Nome="";
        public int Idade;

        public Pessoa(string nome, int idade)
        {
            Nome = nome;
            Idade = idade;
        }

        // Método sem parâmetros
        public void Apresentar()
        {
            Console.WriteLine($"Olá, meu nome é {Nome}");
        }

        // Método com parâmetro sobrenome
        public void Apresentar(string sobrenome)
        {
            Console.WriteLine($"Olá, meu nome é {Nome} {sobrenome}");
        }

        public virtual void ExibirDados()
        {
            Console.WriteLine($"Nome: {Nome}");
            Console.WriteLine($"Idade: {Idade}");
        }
    }
    }
}