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
            var jogada = Minimax(_depthDefault, gameState, maximizingPlayer);
            return jogada;
        }

        /// <summary>
        /// Retorna a melhor jogada a partir do algoritmo de minimax.
        /// </summary>
        /// <param name="depth"></param>
        /// <param name="state"></param>
        /// <param name="maximizingPlayer"></param>
        /// <returns></returns>
        private PieceModel Minimax(int depth, GameState state, bool maximizingPlayer)
        {
            if (maximizingPlayer)
                return Maxmazing(depth, state).Item1;
            else
                return Minimazing(depth, state).Item1;
        }

        private (PieceModel, int) Minimazing(int depth, GameState state)
        {
            var menorPontuacao = int.MaxValue;
            var pecaRetorno = default(PieceModel);

            if (state.EhTerminal())
                return (pecaRetorno, -Utilidade());

            if (depth == 0)
                return (pecaRetorno, -AvaliarEstado(state));

            var pecasPossiveis = state.ObterPecasPossiveis();
            foreach (var pecaIx in pecasPossiveis)
            {
                var stateProxTurno = GameState.RealizarJogada(state, pecaIx);
                var (_, pontos) = Maxmazing(depth - 1, stateProxTurno);
                if (pontos < menorPontuacao)
                {
                    menorPontuacao = pontos;
                    pecaRetorno = pecaIx;
                }
            }

            return (pecaRetorno, menorPontuacao);
        }

        private (PieceModel, int) Maxmazing(int depth, GameState state)
        {
            var maiorPontuacao = int.MinValue;
            var pecaRetorno = default(PieceModel);

            //estou em um estado terminal?
            if (state.EhTerminal())
                return (pecaRetorno, Utilidade());

            if (depth == 0)
                return (pecaRetorno, AvaliarEstado(state));

            var pecasPossiveis = state.ObterPecasPossiveis();
            foreach (var pecaIx in pecasPossiveis)
            {
                var stateProxTurno = GameState.RealizarJogada(state, pecaIx);
                var (_, pontos) = Minimazing(depth - 1, stateProxTurno);
                if (pontos > maiorPontuacao)
                {
                    maiorPontuacao = pontos;
                    pecaRetorno = pecaIx;
                }
            }

            return (pecaRetorno, maiorPontuacao);
        }
        

        /// <summary>
        /// Função linear com pesos: Fav(s) = w.A(s) + w.B(s) ...
        /// Chamada para dar nota para estados. Probabilidade de vencer a partir deste estado.
        /// PONTOS:
        ///     1. A(s) = Qtdd de peças possiveis.
        ///         - Nao deixar o oponente jogar (inverso, ponto negativo)
        ///     2. B(s) = Qtdd de peças na mão (apos comprar).        ///  
        /// </summary>
        /// <returns>
        /// Retorna pontuação do estado atual do jogo.
        /// Nunca retorna uma pontuacao maior que a Utilidade()
        /// </returns>
        private int AvaliarEstado(GameState gameState)
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
