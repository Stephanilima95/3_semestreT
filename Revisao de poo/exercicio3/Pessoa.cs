using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace exercicio3
{
    public class Pessoa
    {
       public string Nome = "";

       public int Idade;

       public void ExibirDados()
        {
            System.Console.WriteLine($"Nome: {Nome}");
            System.Console.WriteLine($"Idade: {Idade}");
        }

        public Pessoa(string n, int i)
        {
            if(i > 0)
            {
                Nome = n;
                Idade = i;
            }
            else
            {
                System.Console.WriteLine($"A idade não pode ser negativa. A idade de {n} foi definida como 0.");
                Nome = n;
                Idade = 0;
            }
        }
      

    }
}