using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public class Rook: Piece
    {
        public override TypePieces Type => TypePieces.Rook;

        public override Player Color { get; }
        public static readonly Direction[] dirs = new Direction[]
        {
            Direction.North,
            Direction.South,
            Direction.East,
            Direction.West,
        };
        public Rook(Player color)
        {
            Color = color;
        }

        public override Piece copy()
        {
            Rook RookCopy = new Rook(Color);
            RookCopy.IsPlaying = IsPlaying;
            return RookCopy;

        }

        public override IEnumerable<Move> GetMoves(Position from, Board board)
        {
            return MovePositionsInDir(from, board, dirs).Select(to => new NormalMoves(from, to));
        }


    }
}
