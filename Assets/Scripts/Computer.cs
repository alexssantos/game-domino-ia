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

        /// <summary>
        /// Recebe o Estado do jogo no turno atual: dados de peças na mão e na mesa.
        /// </summary>
        /// <returns>Retorna a melhor peça a ser jogada</returns>
        public PieceModel ObterMelhorJogada(List<PieceModel> pecasPossiveis, List<PieceModel> pecasNaMesa, int qtddAdversario, int qtddComprar)
        {
            //Peças na minha mão
            //Peças no tabuleiro
            //Qtdd de peças para comprar
            //Qtdd de peças do Adversario

            for (int i = 0; i < pecasPossiveis.Count; i++)
            {

            }

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
