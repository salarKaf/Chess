using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public class DoublePawn : Move
    {
        public override MoveType Type => MoveType.DoublePawn;
        public override Position FromPosition { get; }
        public override Position ToPosition { get; }
        private readonly Position skippedPos;
        public DoublePawn(Position from,Position to)
        {
            FromPosition = from;
            ToPosition = to;
            skippedPos = new Position((from.Row + to.Row) / 2, from.Column);
        }
        public override bool Execute(Board board)
        {
            Player player = board[FromPosition].Color;
            board.SetPawnSkipPosition(player, skippedPos);
            new NormalMoves(FromPosition,ToPosition).Execute(board);

            return true;

        }

    }
}
