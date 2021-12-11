using Assets.Scripts.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    [Serializable]
    public class GameState
    {
        public int Pontos { get; set; }
        public bool JogoTerminado { get; set; }
        public int PontaEsquerda { get; set; }
        public int PontaDireita { get; set; }

        public List<PiecePairValue> PecasDoJogador { get; set; }
        public List<PiecePairValue> PecasNaMesa { get; set; }
        public List<PiecePairValue> PecasParaComprar { get; set; }
        public List<PiecePairValue> PecasAdversario { get; set; }


        public GameState(
            List<PieceModel> pecasDoJogador, 
            List<PieceModel> pecasNaMesa,
            List<PieceModel> pecasParaComprar, 
            List<PieceModel> pecasAdversario, 
            int pontaEsquerda, 
            int pontaDireita)
        {
            this.PecasDoJogador = pecasDoJogador.Select(p => new PiecePairValue(p)).ToList();
            this.PecasNaMesa = pecasNaMesa.Select(p => new PiecePairValue(p)).ToList();
            this.PecasParaComprar = pecasParaComprar.Select(p => new PiecePairValue(p)).ToList();
            this.PecasAdversario = pecasAdversario.Select(p => new PiecePairValue(p)).ToList();
            PontaEsquerda = pontaEsquerda;
            PontaDireita = pontaDireita;
        }

        public void InverterTurno()
        {
            var aux = PecasDoJogador;
            PecasDoJogador = PecasAdversario;
            PecasAdversario = aux; 
        }

        public bool EhPossivelJogar(PiecePairValue peca)
        {
            if (peca == null) return false;

            if (peca.sideA == PontaDireita || peca.sideA == PontaEsquerda
                || peca.sideB == PontaDireita || peca.sideB == PontaEsquerda)
                return true;

            return false;
        }

        public List<PiecePairValue> ObterPecasPossiveis()
        {
            return PecasDoJogador.Where(peca => EhPossivelJogar(peca)).ToList();
        }

        public bool EhTerminal()
        {
            var playerWinns = PecasDoJogador.Count == 0;
            var computerWinns = PecasAdversario.Count == 0;
            var drawGame = JogoTerminado && PecasDoJogador.Count > 0 && PecasAdversario.Count > 0;
            return playerWinns || computerWinns || drawGame;
        }

        /// <summary>
        /// retira uma peça aleatoria da compra e a retorna.
        /// </summary>
        public PiecePairValue ComprarPeca()
        {
            if (!PecasParaComprar.Any())
                return default;

            int index = new Random().Next(0, PecasNaMesa.Count - 1);
            PiecePairValue newPiece = PecasParaComprar[index];
            
            PecasParaComprar.RemoveAt(index);
            PecasDoJogador.Add(newPiece);
            return newPiece;
        }
            

        private bool AdicionarPecaNoTabuleiro(PiecePairValue peca)
        {
            while (!EhPossivelJogar(peca))
            {
                var nova = ComprarPeca();
                if (peca != default)                
                    peca = nova;
                else
                {
                    //passa a vez
                    InverterTurno();
                    return false;
                }                
            }

            if (peca.sideA == PontaEsquerda)
                PontaEsquerda = peca.sideA;
            else if (peca.sideA == PontaDireita)
                PontaDireita = peca.sideA;
            else if (peca.sideB == PontaEsquerda)
                PontaEsquerda = peca.sideB;
            else if (peca.sideB == PontaDireita)
                PontaDireita = peca.sideB;

            PecasNaMesa.Add(peca);
            return true;
        }

        /// <summary>
        /// Realiza a jogada escolhida no  tabuleiro.
        /// </summary>
        /// <param name="gameState"></param>
        /// <returns>Estado do prox turno com jogador do turno já alternado.</returns>
        public static GameState RealizarJogada(GameState gameState, PiecePairValue peca)
        {
            var state = gameState.Clone();
            if (!state.AdicionarPecaNoTabuleiro(peca))
                return state;

            state.PecasDoJogador.Remove(peca);

            if (state.EhFimDeJogo())
            {
                state.JogoTerminado = true;
                return state;
            }

            state.InverterTurno();
            return state;
        }

        public bool EhFimDeJogo()
        {
            if (PecasDoJogador.Count == 0)
                return true;

            return false;
        }
    }

    [Serializable]
    public class PiecePairValue
    {
        public int sideA { get; private set; }        
        public int sideB { get; private set; }

        public PiecePairValue(PieceModel model)
        {
            this.sideA = model.sideA;
            this.sideB = model.sideB;
        }
    }
}
