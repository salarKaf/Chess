using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public class EnPassant : Move
    {
        public override MoveType Type => MoveType.EnPassant;
        public override Position FromPosition { get; }
        public override Position ToPosition { get; }
        private readonly Position capturePos;
        public EnPassant(Position from, Position to)
        {
            FromPosition = from;
            ToPosition = to;
            capturePos = new Position(from.Row, to.Column);
        }
        public override bool Execute(Board board)
        {
            new NormalMoves(FromPosition, ToPosition).Execute(board);
            board[capturePos] = null;

            return true;
        }
    }
}
