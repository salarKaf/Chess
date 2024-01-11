using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public class Result
    {
        public Player winner {  get;  }

        public EndReason reason { get; }    

        public Result(Player winner, EndReason reason)
        {
            this.winner = winner;   
            this.reason = reason;
        }

        public static Result Win(Player winner)
        {
            return new Result(winner, EndReason.Checkmate);

        }

        public static Result Draw(EndReason reason)
        {
            return new Result(Player.None, reason);
        }
    }
}
