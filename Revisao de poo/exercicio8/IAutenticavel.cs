interface IAutenticavel
{
    bool Autenticar(string senha);
}

class Usuario : IAutenticavel
{
    public bool Autenticar(string senha)
    {
        return senha == "1234";
    }
}

class Administrador : IAutenticavel
{
    public bool Autenticar(string senha)
    {
        return senha == "admin";
    }
}
