using System;

class Program
{
    static void Main()
    {
        Pessoa p = new Pessoa();
        p.Nome = "Carlos";

        p.Apresentar();
        p.Apresentar("Silva");
    }
}
