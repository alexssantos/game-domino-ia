﻿using System;
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

        public bool EhTerminal(GameState gameState)
        {
            var playerWinns = gameState.PecasDoJogador.Count == 0;
            var computerWinns = gameState.PecasAdversario.Count == 0;
            var drawGame = gameState.JogoTerminado && gameState.PecasDoJogador.Count > 0 && gameState.PecasAdversario.Count > 0;
            return playerWinns || computerWinns || drawGame;
        }

        /// <summary>
        /// retira uma peça aleatoria da compra e a retorna.
        /// </summary>
        public PieceModel ComprarPeca(GameState gameState)
        {
            if (!gameState.PecasParaComprar.Any())
                return default;

            int index = new Random().Next(0, gameState.PecasNaMesa.Count - 1);
            PieceModel newPiece = gameState.PecasParaComprar[index];
            
            gameState.PecasParaComprar.RemoveAt(index);
            gameState.PecasDoJogador.Add(newPiece);
            return newPiece;
        }
            

        private bool AdicionarPecaNoTabuleiro(PieceModel peca)
        {
            while (!EhPossivelJogar(peca))
            {
                var nova = ComprarPeca(this);
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
            gameState.InverterTurno();
            return gameState;
        }
    }
}
