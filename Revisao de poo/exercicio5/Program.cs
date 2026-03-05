using System;

class Program
{
    static void Main()
    {
        Funcionario f = new Funcionario();
        f.Nome = "Ana";
        f.Idade = 25;
        f.Salario = 2500;

        Console.WriteLine("Funcionário");
        Console.WriteLine($"Nome: {f.Nome}");
        Console.WriteLine($"Idade: {f.Idade}");
        Console.WriteLine($"Salário: R$ {f.Salario}");
    }
}
