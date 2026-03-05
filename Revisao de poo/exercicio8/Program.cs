using System;

class Program
{
    static void Main()
    {
        Usuario user = new Usuario();
        Administrador admin = new Administrador();

        Console.WriteLine(user.Autenticar("1234"));
        Console.WriteLine(admin.Autenticar("admin"));
    }
}
