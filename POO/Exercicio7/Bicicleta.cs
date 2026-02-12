using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercicio7
{
    public class Bicicleta
    {
        public class Bicicleta : Veiculo
    {
        public override void Mover()
        {
            Console.WriteLine("A bicicleta está sendo pedalada.");
        }
    }
    }
}