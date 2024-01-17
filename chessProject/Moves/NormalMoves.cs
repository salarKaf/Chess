using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public class NormalMoves : Move
    {
        public override MoveType Type => MoveType.Normal;

        public override Position FromPosition { get; }

        public override Position ToPosition { get; }

        public NormalMoves(Position from, Position to)
        {
            FromPosition = from;
            ToPosition = to;

        }


        public override bool Execute(Board board)
        {
            Piece piece = board[FromPosition];
            bool capture = !board.IsEmpty(ToPosition);
            board[ToPosition] = piece;
            board[FromPosition] = null;
            piece.IsPlaying = true;

            return capture || piece.Type == TypePieces.Pawn;
        }
    }
}
