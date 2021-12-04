using System;

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
        public PieceModel ObterMelhorJogada(GameState gameState)
        {
            //Peças na minha mão
            //Peças no tabuleiro
            //Qtdd de peças para comprar
            //Qtdd de peças do Adversario

            //var pecasPossiveis = gameState.PecasDoJogador

            for (int i = 0; i < gameState.PecasDoJogador.Count; i++)
            {

            }

            return null;
        }

        private void Minimax(int depth, GameState state, bool maximizingPlayer)
        {

        }

        private void Minimazing()
        {

        }

        private void Maxmazing()
        {

        }

        /// <summary>
        /// Realiza a jogada e obtem o proximo estado ao 
        /// </summary>
        //public RealizarJogada()
        //{

        //}


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
        /// Dá valor numérico aos estados terminais. 
        /// </summary>
        /// <returns></returns>
        private int Utilidade()
        {
            return 10;
        }

        private bool EhTerminal(GameState gameState)
        {
            var playerWinns = gameState.PecasDoJogador.Count == 0;
            var computerWinns = gameState.PecasAdversario.Count == 0;
            var drawGame = gameState.JogoTerminado && gameState.PecasDoJogador.Count > 0 && gameState.PecasAdversario.Count > 0;
            return playerWinns || computerWinns || drawGame;
        }

        #region Acoes



        ///// <summary>
        ///// retira uma peça aleatoria da compra e retorna.
        ///// </summary>
        //public PieceModel ComprarPeca(GameState gameState)
        //{
        //    int index = new Random().Next(0, gameState.PecasNaMesa.Count - 1);
        //    PieceModel newPiece = piecesInGame[index];
        //    piecesInGame.RemoveAt(index);
        //}

        #endregion
    }
}
