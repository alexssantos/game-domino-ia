using System;

namespace Assets.Scripts
{
    public class Computer
    {
        private string _algoritmo { get; set; }
        private const int _depthDefault = 3;

        public Computer(string algoritmo = "MINIMAX")
        {
            _algoritmo = algoritmo.ToUpper();
        }

        /// <summary>
        /// Recebe o Estado do jogo no turno atual: dados de peças na mão e na mesa.
        /// </summary>
        /// <returns>Retorna a melhor peça a ser jogada</returns>
        public PieceModel ObterMelhorJogada(GameState gameState, bool maximizingPlayer=true )
        {
            //Peças na minha mão
            //Peças no tabuleiro
            //Qtdd de peças para comprar
            //Qtdd de peças do Adversario

            //var pecasPossiveis = gameState.PecasDoJogador

            for (int i = 0; i < gameState.PecasDoJogador.Count; i++)
            {


                var jogada = Minimax(_depthDefault, gameState, maximizingPlayer);
            }

            return null;
        }

        private PieceModel Minimax(int depth, GameState state, bool maximizingPlayer)
        {
            if (maximizingPlayer)
                return Maxmazing(depth, state);
            else
                return Minimazing(depth, state);
        }

        private PieceModel Minimazing(int depth, GameState state)
        {
            
        }

        private PieceModel Maxmazing(int depth, GameState state)
        {

        }
        

        /// <summary>
        /// Função linear com pesos: Fav(s) = w.A(s) + w.B(s) ...
        /// Chamada para dar nota para estados. Probabilidade de vencer a partir deste estado.
        /// PONTOS:
        ///     1. A(s) = Qtdd de peças possiveis.
        ///         - Nao deixar o oponente jogar (inverso, ponto negativo)
        ///     2. B(s) = Qtdd de peças na mão (apos comprar).
        /// </summary>
        /// <returns></returns>
        private int FuncaoAvaliacao(GameState gameState)
        {


            return 10;
        }

        /// <summary>
        /// Pontuacao maxima aos estados terminais. 
        /// </summary>
        /// <returns></returns>
        private int Utilidade()
        {
            return 100;
        }
    }
}
