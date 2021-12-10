using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public class GameState
    {
        public int Pontos { get; set; }
        public bool JogoTerminado { get; set; }
        public int PontaEsquerda { get; set; }
        public int PontaDireita { get; set; }

        public List<PieceModel> PecasDoJogador { get; set; }
        public List<PieceModel> PecasNaMesa { get; set; }
        public List<PieceModel> PecasParaComprar { get; set; }
        public List<PieceModel> PecasAdversario { get; set; }


        public GameState(List<PieceModel> pecasDoJogador, List<PieceModel> pecasNaMesa, List<PieceModel> pecasParaComprar, List<PieceModel> pecasAdversario)
        {
            this.PecasDoJogador = pecasDoJogador;
            this.PecasNaMesa = pecasNaMesa;
            this.PecasParaComprar = pecasParaComprar;
            this.PecasAdversario = pecasAdversario;
        }
              
        public void InverterTurno()
        {
            var aux = PecasDoJogador;
            PecasDoJogador = PecasAdversario;
            PecasAdversario = aux; 
        }

        public bool EhPossivelJogar(PieceModel peca)
        {
            if (peca == null) return false;

            if (peca.sideA == PontaDireita || peca.sideA == PontaEsquerda
                || peca.sideB == PontaDireita || peca.sideB == PontaEsquerda)
                return true;

            return false;
        }

        public List<PieceModel> ObterPecasPossiveis()
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
        public PieceModel ComprarPeca()
        {
            if (!PecasParaComprar.Any())
                return default;

            int index = new Random().Next(0, PecasNaMesa.Count - 1);
            PieceModel newPiece = PecasParaComprar[index];
            
            PecasParaComprar.RemoveAt(index);
            PecasDoJogador.Add(newPiece);
            return newPiece;
        }
            

        private bool AdicionarPecaNoTabuleiro(PieceModel peca)
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
        public static GameState RealizarJogada(GameState gameState, PieceModel peca)
        {
            if(!gameState.AdicionarPecaNoTabuleiro(peca))
                return gameState;

            gameState.PecasDoJogador.Remove(peca);

            if (gameState.EhFimDeJogo())
            {
                gameState.JogoTerminado = true;
                return gameState;
            }        

            gameState.InverterTurno();
            return gameState;
        }

        public bool EhFimDeJogo()
        {
            if (PecasDoJogador.Count == 0)
                return true;

            return false;
        }
    }
}
