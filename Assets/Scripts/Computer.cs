using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public class Computer
    {
        private string _algoritmo { get; set; }

        public Computer(string algoritmo = "MINIMAX")
        {
            _algoritmo = algoritmo.ToUpper();
        }

        public PieceModel Jogar()
        {
            return null;
        }

        private void Minimax()
        {

        }

        private void Minimazing()
        {

        }

        private void Naxmazing()
        {

        }

        /// <summary>
        /// Função linear com pesos: F= w.A + w.B ...
        /// chamada para dar nota para estados. Probabilidade de vencer a partir deste estado.
        /// </summary>
        /// <returns></returns>
        private int FuncaoAvaliacao()
        {

            return 10;
        }

        /// <summary>
        /// Dá valor numérico aos estados terminais. 
        /// </summary>
        /// <returns></returns>
        private int FuncaoUtilidade()
        {
            return 10;
        }
    }
}
