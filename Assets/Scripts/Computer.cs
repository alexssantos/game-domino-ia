using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts
{
    public class Computer
    {
        private string _algoritmo { get; set; }
        private const int _depthDefault = 3;
        private IDictionary<string, IDictionary> arvore;

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
            var parValores = Minimax(_depthDefault, gameState, maximizingPlayer);

            if (parValores != default)
                return new PieceModel(parValores.sideA, parValores.sideB);
            else
                return default;
        }

        /// <summary>
        /// Retorna a melhor jogada a partir do algoritmo de minimax.
        /// </summary>
        /// <param name="depth"></param>
        /// <param name="state"></param>
        /// <param name="maximizingPlayer"></param>
        /// <returns></returns>
        private PiecePairValue Minimax(int depth, GameState state, bool maximizingPlayer)
        {
            arvore = new Dictionary<string, IDictionary>();
            arvore["root"] = new Dictionary<string, IDictionary>();

            if (maximizingPlayer)
                return Maxmazing(depth, state, arvore["root"]).Item1;
            else
                return Minimazing(depth, state, arvore["root"]).Item1;
        }

        private (PiecePairValue, double) Minimazing(int depth, GameState state, IDictionary arvore)
        {
            var menorPontuacao = double.MaxValue;
            var pecaRetorno = default(PiecePairValue);

            if (state.EhTerminal())
                return (pecaRetorno, -Utilidade());

            if (depth == 0)
                return (pecaRetorno, -AvaliarEstado(state));



            var pecasPossiveis = state.ObterPecasPossiveis();
            foreach (var pecaIx in pecasPossiveis)
            {
                var indexKey = $"{depth}-{pecaIx}";
                arvore.Add(indexKey, new Dictionary<string, IDictionary>());

                var stateProxTurno = GameState.RealizarJogada(state, pecaIx);
                var (_, pontos) = Maxmazing(depth - 1, stateProxTurno, (IDictionary)arvore[indexKey]);
                if (pontos < menorPontuacao)
                {
                    menorPontuacao = pontos;
                    pecaRetorno = pecaIx;
                    //arvore[$"{indexKey}-pontos"] = pontos;
                }
            }

            return (pecaRetorno, menorPontuacao);
        }

        private (PiecePairValue, double) Maxmazing(int depth, GameState state, IDictionary arvore)
        {
            var maiorPontuacao = double.MinValue;
            var pecaRetorno = default(PiecePairValue);

            //estou em um estado terminal?
            if (state.EhTerminal())
                return (pecaRetorno, Utilidade());

            if (depth == 0)
                return (pecaRetorno, AvaliarEstado(state));
            
            var pecasPossiveis = state.ObterPecasPossiveis();
            foreach (var pecaIx in pecasPossiveis)
            {
                var indexKey = $"{depth}-{pecaIx}";
                arvore.Add(indexKey, new Dictionary<string, IDictionary>());

                var stateProxTurno = GameState.RealizarJogada(state, pecaIx);                              
                var (_, pontos) = Minimazing(depth - 1, stateProxTurno, (IDictionary)arvore[indexKey]);
                if (pontos > maiorPontuacao)
                {
                    maiorPontuacao = pontos;
                    pecaRetorno = pecaIx;
                    //arvore[$"{indexKey}-pontos"] = pontos;
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
        ///     2. B(s) = Qtdd de peças na mão (apos comprar).
        /// </summary>
        /// <returns>
        /// Retorna pontuação do estado atual do jogo.
        /// Nunca retorna uma pontuacao maior que a Utilidade()
        /// </returns>
        private double AvaliarEstado(GameState gameState)
        {
            var max = Utilidade() * 0.8;
            var pontos = 0.0;

            pontos += 3* PontuacaoPorPecasPosiveis(gameState);
            pontos += 2* PontuacaoPorPecasPosiveisOponente(gameState);
            pontos += 1* PontuacaoPorQuantidadeDePecasNaMao(gameState);


            return pontos > max ? max : pontos;
        }

        private double PontuacaoPorPecasPosiveis(GameState gameState)
        {
            var max = 20;
            var pontos = gameState.ObterPecasPossiveis().Count * 2;
            return pontos > max ? max : pontos;
        }

        private double PontuacaoPorPecasPosiveisOponente(GameState gameState)
        {
            var max = 20;
            var pecasQtdd = gameState.ObterPecasPossiveisAdversario().Count;
            if (pecasQtdd == 0)
                return max;
            
            return (max/pecasQtdd);
        }

        private double PontuacaoPorQuantidadeDePecasNaMao(GameState gameState)
        {
            var max = 20;
            var pecasQtdd = gameState.PecasDoJogador.Count;
            if (pecasQtdd == 0)
                return max;

            return (max / pecasQtdd);
        }

        /// <summary>
        /// Pontuacao maxima aos estados terminais. 
        /// </summary>
        /// <returns></returns>
        private int Utilidade()
        {
            return 200;
        }
    }
}
