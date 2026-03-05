class Pessoa
{
    public string Nome = "";

    public void Apresentar()
    {
        Console.WriteLine($"Olá, meu nome é {Nome}");
    }

    public void Apresentar(string sobrenome)
    {
        Console.WriteLine($"Olá, meu nome é {Nome} {sobrenome}");
    }
}
