using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public class GameState
    {
        public GameState(List<PieceModel> pecasDoJogador, List<PieceModel> pecasNaMesa, List<PieceModel> pecasParaComprar, List<PieceModel> pecasAdversario)
        {
            this.PecasDoJogador = pecasDoJogador;
            this.PecasNaMesa = pecasNaMesa;
            this.PecasParaComprar = pecasParaComprar;
            this.PecasAdversario = pecasAdversario;
        }

        public int Pontos { get; set; }
        public bool JogoTerminado { get; set; }
        
        public List<PieceModel> PecasDoJogador { get; set; }
        public List<PieceModel> PecasNaMesa { get; set; }
        public List<PieceModel> PecasParaComprar { get; set; }
        public List<PieceModel> PecasAdversario { get; set; }


        public void InverterTurno()
        {
            var aux = PecasDoJogador;
            PecasDoJogador = PecasAdversario;
            PecasAdversario = aux; 
        }
    }
}
