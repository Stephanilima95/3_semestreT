using System;

class Program
{
    static void Main()
    {
        Veiculo carro = new Carro();
        Veiculo bicicleta = new Bicicleta();

        carro.Mover();
        bicicleta.Mover();
    }
}
